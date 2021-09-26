using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SlowMotionEffect : MonoBehaviour {
    private Volume volume;

    [SerializeField]
    private float modifier = 5f;
    [SerializeField]
    private float slowMotion_timeScale = 0.25f;

    private void Start() {
        volume = this.GetComponent<Volume>();
    }

    private void Update() {
        if (Input.GetMouseButton(1)){
            volume.weight = Mathf.Lerp(volume.weight, 1f, modifier * Time.deltaTime);
            Time.timeScale = Mathf.Lerp(Time.timeScale, slowMotion_timeScale, modifier * Time.deltaTime);
            if (volume.weight > 0.95f) volume.weight = 1f;
        }
        else{
            volume.weight = Mathf.Lerp(volume.weight, 0.001f, modifier * Time.deltaTime);
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, modifier * Time.deltaTime);
        }

    }
}