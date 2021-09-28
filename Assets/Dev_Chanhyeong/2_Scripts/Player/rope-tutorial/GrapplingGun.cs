using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class GrapplingGun : MonoBehaviour {
    
    public LayerMask whatIsGrappleable;
    public Transform gunTip, _camera, player;
    [SerializeField] private KeyCode grappleKey = KeyCode.E;
    [SerializeField] private KeyCode grappleJumpKey = KeyCode.Q;
    private Vector3 _grapplePoint;
    
    [SerializeField] private float maxDistance = 75f;
    private SpringJoint _joint = null;

    [SerializeField] private float spring = 4f;
    [SerializeField] private float damper = 7f;
    [SerializeField] private float massSalce = 4.5f;
    [SerializeField] private float grapleJumpPower = 50f;
    [SerializeField] private FieldOfView fieldOfView;

    private Vector3 _direction;


    void Update() {
        if (fieldOfView.visibleTargets.Count > 0){
            fieldOfView.visibleTargets = fieldOfView.visibleTargets.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
            _direction = (fieldOfView.visibleTargets[0].position - _camera.transform.position).normalized;

            Debug.DrawRay(_camera.transform.position, _direction, Color.magenta);
        }

        if (Input.GetKeyDown(grappleKey) && !IsGrappling()) {
            StartGrapple();
        }
        else if (Input.GetKeyUp(grappleKey)) {
            StopGrapple();
        }

        if (Input.GetKeyDown(grappleJumpKey) && !IsGrappling()){
            StartGrappleJump();
        }
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    private void StartGrapple() {
        if (!Physics.Raycast(_camera.position, _direction, out var hit, maxDistance, whatIsGrappleable)) return;
        
        _grapplePoint = hit.point;
        _joint = player.gameObject.AddComponent<SpringJoint>();
        _joint.autoConfigureConnectedAnchor = false;
        _joint.connectedAnchor = _grapplePoint;

        float distanceFromPoint = Vector3.Distance(player.position, _grapplePoint);

        //The distance grapple will try to keep from grapple point. 
        _joint.maxDistance = distanceFromPoint * 0.8f;
        _joint.minDistance = distanceFromPoint * 0.25f;

        //Adjust these values to fit your game.
        _joint.spring = spring;
        _joint.damper = damper; 
        _joint.massScale = massSalce;
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    private void StopGrapple()
    {
        Destroy(_joint);
    }

    private void StartGrappleJump() {
        if (!Physics.Raycast(_camera.position, _direction, out var hit, maxDistance, whatIsGrappleable)) return;
        _grapplePoint = hit.point;
        _joint = player.gameObject.AddComponent<SpringJoint>();
        _joint.autoConfigureConnectedAnchor = false;
        _joint.connectedAnchor = _grapplePoint;

        Vector3 direction = hit.point - player.transform.position;

        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().AddForce(direction * grapleJumpPower, ForceMode.VelocityChange);

        float distanceFromPoint = Vector3.Distance(player.position, _grapplePoint);

        //The distance grapple will try to keep from grapple point. 
        _joint.maxDistance = distanceFromPoint * 0.8f;
        _joint.minDistance = distanceFromPoint * 0.25f;

        //Adjust these values to fit your game.
        _joint.spring = spring;
        _joint.damper = damper; 
        _joint.massScale = massSalce;

        Destroy(_joint, .1f);
    }


    public bool IsGrappling() {
        return _joint != null;
    }

    public Vector3 GetGrapplePoint() {
        return _grapplePoint;
    }
}
