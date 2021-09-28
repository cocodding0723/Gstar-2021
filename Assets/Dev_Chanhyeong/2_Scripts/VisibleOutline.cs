using UnityEngine;

[RequireComponent(typeof(Outline))]
public class VisibleOutline : MonoBehaviour, IVisible {
    private Outline _outline = null;
    [SerializeField]
    private float _widthMax = 2f;
    [SerializeField]
    private float _blinkMultiplier = 1f;

    private float _targetWidth = 0f;

    private float _elapsedTime = 0f;

    private bool isVisible = false;

    private void Awake() {
        _outline = this.GetComponent<Outline>();
    }

    private void Update() {
        if (isVisible){
            _outline.OutlineWidth = Mathf.Lerp(_outline.OutlineWidth, _targetWidth, Time.deltaTime);
        }
        else{

        }
    }

    public void OnInvisivle()
    {
        _targetWidth = 0f;
        Debug.Log("눈에 안보임");
        isVisible = false;
    }

    public void OnVisible()
    {
        _targetWidth = _widthMax * Mathf.Sin(Time.time * _blinkMultiplier);
        Debug.Log("눈에 보임");
        isVisible = true;
    }
}