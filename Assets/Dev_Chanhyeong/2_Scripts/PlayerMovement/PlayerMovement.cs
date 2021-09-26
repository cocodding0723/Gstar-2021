using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectTemplate;

public class PlayerMovement : OptimizeBehaviour
{
    float playerHeight = 2f;

    [SerializeField] private Transform orientation;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float airMultiplier = 0.4f;
    float movementMultiplier = 10f;

    [Header("Sprinting")]
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float acceleration = 10f;

    [Header("Jumping")]
    public float jumpForce = 5f;
    public float wallJumpForce = 2.5f;

    [Header("Keybinds")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Drag")]
    [SerializeField] private float groundDrag = 6f;
    [SerializeField] private float airDrag = 2f;
    [SerializeField] private float slideDrag = 4f;
    [SerializeField] private float grapplingDrag = 0f;

    float horizontalMovement;
    float verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField]
    public bool isGrounded { get; private set; }

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;
    public float slideForce = 400;
    public float slideCounterMovement = 0.2f;
    private Vector3 playerScale;
    private Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    private bool crouching = false;
    private bool isSlope = false;
    private bool afterGrappling = false;

    private RaycastHit slopeHit;
    [SerializeField] private GrapplingGun grapplingGun = null;
    private WallRun wallRun;

    private void Start()
    {
        rigidbody.freezeRotation = true;
        playerScale = transform.localScale;
        wallRun = this.GetComponent<WallRun>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        SlopeCheck();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        MyInput();
        ControlDrag();
        ControlSpeed();

        if (Input.GetKeyDown(jumpKey))
        {
            Jump();
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        crouching = Input.GetKey(crouchKey);
        if (Input.GetKeyDown(crouchKey))
            StartCrouch();
        if (Input.GetKeyUp(crouchKey))
            StopCrouch();

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    void Jump()
    {
        if (isGrounded)
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
            rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        if (wallRun.isWallRunning){
            Debug.Log("벽점프");
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
            rigidbody.AddForce((wallRun.GetWallJumpDirection() + transform.up) * wallJumpForce, ForceMode.Impulse);
        }
    }

    void ControlSpeed()
    {
        if (crouching)
        {
            rigidbody.AddForce(moveSpeed * Time.deltaTime * -rigidbody.velocity.normalized * slideCounterMovement);
            return;
        }
        if (Input.GetKey(sprintKey) && isGrounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    private void StartCrouch()
    {
        transform.localScale = crouchScale;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        if (rigidbody.velocity.magnitude > 0.5f)
        {
            if (isGrounded)
            {
                rigidbody.AddForce(orientation.transform.forward * slideForce);
            }
        }
    }

    private void StopCrouch()
    {
        transform.localScale = playerScale;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    void ControlDrag()
    {
        if (crouching)
        {
            rigidbody.drag = slideDrag;
        }
        if (isGrounded && !crouching)
        {
            rigidbody.drag = groundDrag;
            afterGrappling = false;
        }
        if (!isGrounded && !grapplingGun.IsGrappling() && rigidbody.useGravity && !afterGrappling)
        {
            rigidbody.drag = airDrag;
        }
        if (!isGrounded && grapplingGun.IsGrappling())
        {
            rigidbody.drag = grapplingDrag;
            afterGrappling = true;
        }
    }

    void MovePlayer()
    {
        if (!wallRun.isWallRunning)
        {
            if (crouching && isGrounded)
            {
                rigidbody.AddForce(Vector3.down * Time.deltaTime * 0.1f);
                return;
            }

            if (isGrounded && !isSlope)
            {
                rigidbody.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
            }
            else if (isGrounded && isSlope)
            {
                rigidbody.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
            }
            else if (!isGrounded)
            {
                rigidbody.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
            }
        }
    }

    private void SlopeCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                isSlope = true;
            }
            else
            {
                isSlope = false;
            }
        }
    }
}