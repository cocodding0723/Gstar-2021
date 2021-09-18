using UnityEngine.Events;

namespace ObjectTemplate.Events{
    public delegate void FloatHander(float value);
    public class FloatEvent : UnityEvent<float> {}
}