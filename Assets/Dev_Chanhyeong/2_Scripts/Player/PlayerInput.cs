using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerInput : MonoBehaviour
{
    public float vertical { get; private set; }
    public float horizontal { get; private set; }
    public bool isJump {get; private set;}
    public bool isSprint { get; private set; }
    public bool isCrouch { get; private set; }
    public bool isGrappling { get; private set; }
    public bool isGrapplingJump { get; private set; }

    private const string _verticalInput = "Vertical";
    private const string _horizontalInput = "Horizontal";

    [SerializeField]
    private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField]
    private KeyCode _sprintKey = KeyCode.LeftShift;
    [SerializeField]
    private KeyCode _crouchKey = KeyCode.LeftControl;
    [SerializeField]
    private KeyCode _grapplingKey = KeyCode.E;
    [SerializeField]
    private KeyCode _grpplingJumpKey = KeyCode.Q;

    private void Update() {
        vertical = Input.GetAxisRaw(_verticalInput);
        horizontal = Input.GetAxisRaw(_horizontalInput);

        isJump = Input.GetKeyDown(_jumpKey);
        isSprint = Input.GetKey(_sprintKey);
        isCrouch = Input.GetKey(_crouchKey);
        isGrappling = Input.GetKey(_grapplingKey);
        isGrapplingJump = Input.GetKey(_grpplingJumpKey);
    }
}