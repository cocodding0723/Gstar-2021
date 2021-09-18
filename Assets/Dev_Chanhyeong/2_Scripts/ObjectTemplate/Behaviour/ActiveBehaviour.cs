using UnityEngine;
using ObjectTemplate.Interface;

namespace ObjectTemplate.Behaviour{
    public abstract class ActiveBehaviour : OptimizeBehaviour, IActive
    {

        public virtual void SetActive(bool activeState)
        {
            if (activeState) 
            {
                Active();
            }
            else{
                InActive();
            }
        }
        public abstract void Active();

        public abstract void InActive();
    }
}