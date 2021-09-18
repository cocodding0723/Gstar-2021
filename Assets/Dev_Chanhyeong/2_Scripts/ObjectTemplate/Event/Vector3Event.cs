using UnityEngine;
using UnityEngine.Events;

namespace ObjectTemplate.Events{
    public delegate void Vector3Handler(Vector3 value);
    public class Vector3Event : UnityEvent<Vector3> {}
}