using UnityEngine;

public class GrapplingGun : MonoBehaviour {
    
    public LayerMask whatIsGrappleable;
    public Transform gunTip, _camera, player;
    [SerializeField] private KeyCode grappleKey = KeyCode.E;
    private Vector3 grapplePoint;
    private float maxDistance = 100f;
    private SpringJoint joint = null;

    [SerializeField] private float spring = 4f;
    [SerializeField] private float damper = 7f;
    [SerializeField] private float massSalce = 4.5f;


    void Update() {
        if (Input.GetKeyDown(grappleKey)) {
            StartGrapple();
        }
        else if (Input.GetKeyUp(grappleKey)) {
            StopGrapple();
        }
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple() {
        RaycastHit hit;
        if (Physics.Raycast(_camera.position, _camera.forward, out hit, maxDistance, whatIsGrappleable)) {
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



    public bool IsGrappling() {
        return joint != null;
    }

    public Vector3 GetGrapplePoint() {
        return grapplePoint;
    }
}
