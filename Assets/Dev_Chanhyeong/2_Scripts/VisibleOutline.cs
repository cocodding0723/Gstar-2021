using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Outline))]
public class VisibleOutline : MonoBehaviour, IVisible {
    private Outline _outline = null;
    [FormerlySerializedAs("_widthMax")] [SerializeField]
    private float widthMax = 2f;
    [FormerlySerializedAs("_blinkMultiplier")] [SerializeField]
    private float blinkMultiplier = 1f;

    private float _targetWidth = 0f;
    private float _elapseTime = 0f;

    private void Awake() {
        _outline = this.GetComponent<Outline>();
    }

    private void Update()
    {
        _elapseTime += Time.deltaTime;
        _outline.OutlineWidth = Mathf.Lerp(_outline.OutlineWidth, _targetWidth, Time.deltaTime);

        if (_elapseTime > 1f)
        {
            OnInvisivle();
        }
    }

    public void OnInvisivle()
    {
        _targetWidth = 0f;
    }

    public void OnVisible()
    {
        _elapseTime = 0f;
        _targetWidth = widthMax * Mathf.Sin(Time.time * blinkMultiplier) + widthMax;
    }
}