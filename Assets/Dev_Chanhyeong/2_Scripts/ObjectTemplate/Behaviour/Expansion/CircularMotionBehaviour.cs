using UnityEngine;
using ObjectTemplate.ScraptableObject;

namespace ObjectTemplate.Behaviour.Expansion
{
    public abstract class CircularMotionBehaviour : ActiveBehaviour {
        [SerializeField]
        protected Ease circularEase = null;

        // TODO : MagicCircle과 ItemInventory에서 쓰인 것들 합치기
    }

    public abstract class CircularMotionBehaviour<T> : CircularMotionBehaviour where T : OptimizeBehaviour {
        // TODO : 위의 CircularMotionBehaviour을 상속받아 캐싱해서 사용하는 Behaviour 만들기
    }
}