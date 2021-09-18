using UnityEngine;
using UnityEngine.Events;

namespace ObjectTemplate.Events{
    public delegate void Vector2Handler(Vector2 value);
    public class Vector2Event : UnityEvent<Vector2> {}
}