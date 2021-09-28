using System.Linq;
using UnityEngine;

public class GrapplingGun : MonoBehaviour {
    
    public LayerMask whatIsGrappleable;
    public Transform gunTip, _camera, player;
    [SerializeField] private KeyCode grappleKey = KeyCode.E;
    [SerializeField] private KeyCode grappleJumpKey = KeyCode.Q;
    private Vector3 grapplePoint;
    
    [SerializeField] private float maxDistance = 75f;
    private SpringJoint joint = null;

    [SerializeField] private float spring = 4f;
    [SerializeField] private float damper = 7f;
    [SerializeField] private float massSalce = 4.5f;
    [SerializeField] private float grapleJumpPower = 50f;
    [SerializeField] private FieldOfView _fieldOfView;

    private Vector3 direction;


    void Update() {
        if (_fieldOfView.visibleTargets.Count > 0){
            _fieldOfView.visibleTargets = _fieldOfView.visibleTargets.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
            direction = (_fieldOfView.visibleTargets[0].position - _camera.transform.position).normalized;

            Debug.DrawRay(_camera.transform.position, direction, Color.magenta);
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
    void StartGrapple() {
        RaycastHit hit;
        if (Physics.Raycast(_camera.position, direction, out hit, maxDistance, whatIsGrappleable)) {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //Adjust these values to fit your game.
            joint.spring = spring;
            joint.damper = damper; 
            joint.massScale = massSalce;
        }
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple() {
        Destroy(joint);
    }

    void StartGrappleJump() {
        RaycastHit hit;
        if (Physics.Raycast(_camera.position, direction, out hit, maxDistance, whatIsGrappleable)) {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            Vector3 direction = hit.point - player.transform.position;

            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.GetComponent<Rigidbody>().AddForce(direction * grapleJumpPower, ForceMode.VelocityChange);

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //Adjust these values to fit your game.
            joint.spring = spring;
            joint.damper = damper; 
            joint.massScale = massSalce;

            Destroy(joint, .1f);
        }
    }


    public bool IsGrappling() {
        return joint != null;
    }

    public Vector3 GetGrapplePoint() {
        return grapplePoint;
    }
}
