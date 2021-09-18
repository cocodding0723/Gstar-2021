using UnityEngine.Events;

namespace ObjectTemplate.Events{
    public delegate void IntHandler(int value);
    public class IntEvent : UnityEvent<int> {}
}