using System.Linq;
using UnityEngine;
using ObjectTemplate;

public class WallRun : OptimizeBehaviour {

    public float wallMaxDistance = 1;
    public float wallSpeedMultiplier = 1.2f;
    public float minimumHeight = 1.2f;
    public float maxAngleRoll = 20;
    [Range(0.0f, 1.0f)]
    public float normalizedAngleThreshold = 0.1f;

    public Transform orientation;
    
    public float jumpDuration = 1;
    public float wallBouncing = 3;
    public float cameraTransitionDuration = 1;

    public float wallGravityDownForce = 20f;

    public bool useSprint;

    [HideInInspector]
    public bool isWallRunning = false;
    Vector3 lastWallPosition;
    Vector3 lastWallNormal;
    float elapsedTimeSinceJump = 0;
    float elapsedTimeSinceWallAttach = 0;
    float elapsedTimeSinceWallDetatch = 0;
    bool jumping;
    float lastVolumeValue = 0;
    float noiseAmplitude;
    private Vector3[] directions;
    private RaycastHit[] hits;

    private PlayerMovement playerMovement;
    [SerializeField] private LayerMask whatIsWall;

    private void Start() {
        playerMovement = this.GetComponent<PlayerMovement>();

        directions = new Vector3[]{ 
            Vector3.right, 
            Vector3.right + Vector3.forward,
            Vector3.forward, 
            Vector3.left + Vector3.forward, 
            Vector3.left
        };
    }

    bool CanWallRun()
    {
        float verticalAxis = Input.GetAxisRaw("Vertical");
        bool isSprinting = Input.GetKeyDown(KeyCode.LeftShift);
        isSprinting = !useSprint ? true : isSprinting;
        
        return !playerMovement.isGrounded && verticalAxis > 0 && VerticalCheck() && isSprinting;
    }

    bool VerticalCheck()
    {
        return !Physics.Raycast(orientation.position, Vector3.down, minimumHeight, whatIsWall);
    }

    public void LateUpdate()
    {  
        isWallRunning = false;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumping = true;
        }

        if(CanAttach())
        {
            hits = new RaycastHit[directions.Length];

            for(int i=0; i<directions.Length; i++)
            {
                Vector3 dir = orientation.TransformDirection(directions[i]);
                Physics.Raycast(orientation.position, dir, out hits[i], wallMaxDistance, whatIsWall);
                if(hits[i].collider != null)
                {
                    Debug.DrawRay(orientation.position, dir * hits[i].distance, Color.green);
                }
                else
                {
                    Debug.DrawRay(orientation.position, dir * wallMaxDistance, Color.red);
                }
            }

            if(CanWallRun())
            {   
                hits = hits.ToList().Where(h => h.collider != null).OrderBy(h => h.distance).ToArray();
                if(hits.Length > 0)
                {
                    OnWall(hits[0]);
                    lastWallPosition = hits[0].point;
                    lastWallNormal = hits[0].normal;
                }
            }
        }

        if(isWallRunning)
        {
            elapsedTimeSinceWallDetatch = 0;
            elapsedTimeSinceWallAttach += Time.deltaTime;
            rigidbody.velocity += Vector3.down * wallGravityDownForce * Time.deltaTime;
            // rigidbody.useGravity = false;
        }
        else
        {   
            elapsedTimeSinceWallAttach = 0;
            elapsedTimeSinceWallDetatch += Time.deltaTime;
            // rigidbody.useGravity = true;
        }
    }

    bool CanAttach()
    {
        
        if(jumping)
        {
            elapsedTimeSinceJump += Time.deltaTime;
            if(elapsedTimeSinceJump > jumpDuration)
            {
                elapsedTimeSinceJump = 0;
                jumping = false;
            }
            return false;
        }
        
        return true;
    }

    void OnWall(RaycastHit hit){
        float d = Vector3.Dot(hit.normal, Vector3.up);
        if(d >= -normalizedAngleThreshold && d <= normalizedAngleThreshold)
        {
            // Vector3 alongWall = Vector3.Cross(hit.normal, Vector3.up);
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 alongWall = orientation.TransformDirection(Vector3.forward);

            Debug.DrawRay(orientation.position, alongWall.normalized * 10, Color.green);
            Debug.DrawRay(orientation.position, lastWallNormal * 10, Color.magenta);

            
            rigidbody.velocity = alongWall * vertical * wallSpeedMultiplier;
            isWallRunning = true;
        }

    }

    float CalculateSide()
    {
        if(isWallRunning)
        {
            Vector3 heading = lastWallPosition - orientation.position;
            Vector3 perp = Vector3.Cross(orientation.forward, heading);
            float dir = Vector3.Dot(perp, orientation.up);
            return dir;
        }
        return 0;
    }

    // public float GetCameraRoll()
    // {
    //     float dir = CalculateSide();
    //     float cameraAngle = m_PlayerCharacterController.playerCamera.transform.eulerAngles.z;
    //     float targetAngle = 0;
    //     if(dir != 0)
    //     {
    //         targetAngle = Mathf.Sign(dir) * maxAngleRoll;
    //     }
    //     return Mathf.LerpAngle(cameraAngle, targetAngle, Mathf.Max(elapsedTimeSinceWallAttach, elapsedTimeSinceWallDetatch) / cameraTransitionDuration);
    // } 

    public Vector3 GetWallJumpDirection()
    {
        if(isWallRunning)
        {
            return lastWallNormal * wallBouncing + Vector3.up;
        }
        return Vector3.zero;
    } 
}