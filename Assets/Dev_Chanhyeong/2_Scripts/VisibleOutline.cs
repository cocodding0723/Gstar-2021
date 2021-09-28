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
        _outline.OutlineWidth = Mathf.Lerp(_outline.OutlineWidth, _targetWidth, Time.deltaTime);
    }

    public void OnInvisivle()
    {
        _targetWidth = 0f;
        isVisible = false;
    }

    public void OnVisible()
    {
        _targetWidth = _widthMax * Mathf.Sin(Time.time * _blinkMultiplier);
        isVisible = true;
    }
}