using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class SlowMotionEffect : MonoBehaviour {
    private Volume _volume;

    [SerializeField]
    private float modifier = 5f;
    [FormerlySerializedAs("slowMotion_timeScale")] [SerializeField]
    private float slowMotionTimeScale = 0.25f;

    private void Start() {
        _volume = this.GetComponent<Volume>();
    }

    private void Update() {
        if (Input.GetMouseButton(1)){
            _volume.weight = Mathf.Lerp(_volume.weight, 1f, modifier * Time.deltaTime);
            Time.timeScale = Mathf.Lerp(Time.timeScale, slowMotionTimeScale, modifier * Time.deltaTime);
            if (_volume.weight > 0.95f) _volume.weight = 1f;
        }
        else{
            _volume.weight = Mathf.Lerp(_volume.weight, 0.001f, modifier * Time.deltaTime);
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, modifier * Time.deltaTime);
        }

    }
}