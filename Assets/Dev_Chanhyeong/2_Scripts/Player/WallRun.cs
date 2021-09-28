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

    private Vector3 _lastWallPosition;
    private Vector3 _lastWallNormal;
    private float _elapsedTimeSinceJump = 0;
    private float _elapsedTimeSinceWallAttach = 0;
    private float _elapsedTimeSinceWallDetatch = 0;
    private bool _jumping;
    private float _noiseAmplitude;
    private Vector3[] _directions;
    private RaycastHit[] _hits;

    private PlayerMovement _playerMovement;
    [SerializeField] private LayerMask whatIsWall;

    private void Start() {
        _playerMovement = this.GetComponent<PlayerMovement>();

        _directions = new Vector3[]{ 
            Vector3.right, 
            Vector3.right + Vector3.forward,
            Vector3.forward, 
            Vector3.left + Vector3.forward, 
            Vector3.left
        };
    }

    private bool CanWallRun()
    {
        float verticalAxis = Input.GetAxisRaw("Vertical");
        bool isSprinting = Input.GetKeyDown(KeyCode.LeftShift);
        isSprinting = !useSprint || isSprinting;
        
        return !_playerMovement.IsGrounded && verticalAxis > 0 && VerticalCheck() && isSprinting;
    }

    private bool VerticalCheck()
    {
        return !Physics.Raycast(orientation.position, Vector3.down, minimumHeight, whatIsWall);
    }

    public void LateUpdate()
    {  
        isWallRunning = false;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            _jumping = true;
        }

        if(CanAttach())
        {
            _hits = new RaycastHit[_directions.Length];

            for(int i=0; i<_directions.Length; i++)
            {
                Vector3 dir = orientation.TransformDirection(_directions[i]);
                Physics.Raycast(orientation.position, dir, out _hits[i], wallMaxDistance, whatIsWall);
                if(_hits[i].collider != null)
                {
                    Debug.DrawRay(orientation.position, dir * _hits[i].distance, Color.green);
                }
                else
                {
                    Debug.DrawRay(orientation.position, dir * wallMaxDistance, Color.red);
                }
            }

            if(CanWallRun())
            {   
                _hits = _hits.ToList().Where(h => h.collider != null).OrderBy(h => h.distance).ToArray();
                if(_hits.Length > 0)
                {
                    OnWall(_hits[0]);
                    _lastWallPosition = _hits[0].point;
                    _lastWallNormal = _hits[0].normal;
                }
            }
        }

        if(isWallRunning)
        {
            _elapsedTimeSinceWallDetatch = 0;
            _elapsedTimeSinceWallAttach += Time.deltaTime;
            rigidbody.velocity += Vector3.down * wallGravityDownForce * Time.deltaTime;
            // rigidbody.useGravity = false;
        }
        else
        {   
            _elapsedTimeSinceWallAttach = 0;
            _elapsedTimeSinceWallDetatch += Time.deltaTime;
            // rigidbody.useGravity = true;
        }
    }

    private bool CanAttach()
    {
        
        if(_jumping)
        {
            _elapsedTimeSinceJump += Time.deltaTime;
            if(_elapsedTimeSinceJump > jumpDuration)
            {
                _elapsedTimeSinceJump = 0;
                _jumping = false;
            }
            return false;
        }
        
        return true;
    }

    private void OnWall(RaycastHit hit){
        float d = Vector3.Dot(hit.normal, Vector3.up);
        if (!(d >= -normalizedAngleThreshold) || !(d <= normalizedAngleThreshold)) return;
        // Vector3 alongWall = Vector3.Cross(hit.normal, Vector3.up);
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 alongWall = orientation.TransformDirection(Vector3.forward);

        var position = orientation.position;
        Debug.DrawRay(position, alongWall.normalized * 10, Color.green);
        Debug.DrawRay(position, _lastWallNormal * 10, Color.magenta);

        rigidbody.velocity = alongWall * vertical * wallSpeedMultiplier;
        isWallRunning = true;

    }

    private float CalculateSide()
    {
        if (!isWallRunning) return 0;
        Vector3 heading = _lastWallPosition - orientation.position;
        Vector3 perp = Vector3.Cross(orientation.forward, heading);
        float dir = Vector3.Dot(perp, orientation.up);
        return dir;
    }

    public float GetCameraRoll(float cameraAngle)
    {
        float dir = CalculateSide();
        float targetAngle = 0;
        if(dir != 0)
        {
            targetAngle = Mathf.Sign(dir) * maxAngleRoll;
        }
        return Mathf.LerpAngle(cameraAngle, targetAngle, Mathf.Max(_elapsedTimeSinceWallAttach, _elapsedTimeSinceWallDetatch) / cameraTransitionDuration);
    } 

    public Vector3 GetWallJumpDirection()
    {
        if(isWallRunning)
        {
            return _lastWallNormal * wallBouncing + Vector3.up;
        }
        return Vector3.zero;
    } 
}