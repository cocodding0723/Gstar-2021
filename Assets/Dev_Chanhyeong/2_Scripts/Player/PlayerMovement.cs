using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectTemplate;

public class PlayerMovement : OptimizeBehaviour
{
    private readonly float _playerHeight = 2f;

    [SerializeField] private Transform orientation;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float airMultiplier = 0.4f;
    private readonly float _movementMultiplier = 10f;

    [Header("Sprinting")]
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float acceleration = 10f;

    [Header("Jumping")]
    public float jumpForce = 5f;
    public float wallJumpForce = 2.5f;

    [Header("Keybindings")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Drag")]
    [SerializeField] private float groundDrag = 6f;
    [SerializeField] private float airDrag = 2f;
    [SerializeField] private float slideDrag = 4f;
    [SerializeField] private float grapplingDrag = 0f;

    private float _horizontalMovement;
    private float _verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField]
    public bool IsGrounded { get; private set; }

    private Vector3 _moveDirection;
    private Vector3 _slopeMoveDirection;
    public float slideForce = 400;
    public float slideCounterMovement = 0.2f;
    private Vector3 _playerScale;
    private readonly Vector3 _crouchScale = new Vector3(1, 0.5f, 1);
    private bool _crouching = false;
    private bool _isSlope = false;
    private bool _afterGrappling = false;

    private RaycastHit _slopeHit;
    [SerializeField] private GrapplingGun grapplingGun = null;
    private WallRun _wallRun;

    private void Start()
    {
        rigidbody.freezeRotation = true;
        _playerScale = transform.localScale;
        _wallRun = this.GetComponent<WallRun>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        SlopeCheck();
    }

    private void Update()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        MyInput();
        ControlDrag();
        ControlSpeed();

        if (Input.GetKeyDown(jumpKey))
        {
            Jump();
        }

        _slopeMoveDirection = Vector3.ProjectOnPlane(_moveDirection, _slopeHit.normal);
    }

    private void MyInput()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal");
        _verticalMovement = Input.GetAxisRaw("Vertical");

        _crouching = Input.GetKey(crouchKey);
        if (Input.GetKeyDown(crouchKey))
            StartCrouch();
        if (Input.GetKeyUp(crouchKey))
            StopCrouch();

        _moveDirection = orientation.forward * _verticalMovement + orientation.right * _horizontalMovement;
    }

    private void Jump()
    {
        if (IsGrounded)
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
            rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        if (_wallRun.isWallRunning){
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
            rigidbody.AddForce((_wallRun.GetWallJumpDirection() + transform.up) * wallJumpForce, ForceMode.Impulse);
        }
    }

    private void ControlSpeed()
    {
        if (_crouching)
        {
            rigidbody.AddForce(moveSpeed * Time.deltaTime * -rigidbody.velocity.normalized * slideCounterMovement);
            return;
        }
        if (Input.GetKey(sprintKey) && IsGrounded)
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
        transform.localScale = _crouchScale;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        if (rigidbody.velocity.magnitude > 0.5f)
        {
            if (IsGrounded)
            {
                rigidbody.AddForce(orientation.transform.forward * slideForce);
            }
        }
    }

    private void StopCrouch()
    {
        transform.localScale = _playerScale;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    private void ControlDrag()
    {
        if (_crouching)
        {
            rigidbody.drag = slideDrag;
        }
        if (IsGrounded && !_crouching)
        {
            rigidbody.drag = groundDrag;
            _afterGrappling = false;
        }
        if (!IsGrounded && !grapplingGun.IsGrappling() && rigidbody.useGravity && !_afterGrappling)
        {
            rigidbody.drag = airDrag;
        }
        if (!IsGrounded && grapplingGun.IsGrappling())
        {
            rigidbody.drag = grapplingDrag;
            _afterGrappling = true;
        }
    }

    private void MovePlayer()
    {
        if (!_wallRun.isWallRunning)
        {
            if (_crouching && IsGrounded)
            {
                rigidbody.AddForce(Vector3.down * Time.deltaTime * 0.1f);
                return;
            }

            if (IsGrounded && !_isSlope)
            {
                rigidbody.AddForce(_moveDirection.normalized * moveSpeed * _movementMultiplier, ForceMode.Acceleration);
            }
            else if (IsGrounded && _isSlope)
            {
                rigidbody.AddForce(_slopeMoveDirection.normalized * moveSpeed * _movementMultiplier, ForceMode.Acceleration);
            }
            else if (!IsGrounded)
            {
                rigidbody.AddForce(_moveDirection.normalized * moveSpeed * _movementMultiplier * airMultiplier, ForceMode.Acceleration);
            }
        }
    }

    private void SlopeCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit, _playerHeight / 2 + 0.5f))
        {
            if (_slopeHit.normal != Vector3.up)
            {
                _isSlope = true;
            }
            else
            {
                _isSlope = false;
            }
        }
    }
}