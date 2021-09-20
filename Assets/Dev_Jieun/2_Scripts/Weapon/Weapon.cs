using UnityEngine;
using ObjectTemplate;

namespace Weapon
{
    public abstract class Weapon : OptimizeBehaviour
    {
        /// <summary>
        /// 공격 딜레이 프로퍼티
        /// </summary>
        public float attackDelay => _attackDelay;

        [SerializeField]
        /// <summary>
        /// 공격 딜레이
        /// </summary>
        protected float _attackDelay = 2f;

        /// <summary>
        /// 이전에 공격한 시간
        /// </summary>
        protected float _prevAttackTime = 0f;

        /// <summary>
        /// 무기 공격 함수
        /// </summary>
        public virtual void Excute(){
            if (_prevAttackTime + _attackDelay > Time.time){           // 공격 가능한 시간이 아니면
                return;                                                 // 함수 종료
            }
            
            _prevAttackTime = Time.time;
            // 무기 공격에 관한 정의를 상속해서 사용
        }
    }
}