using UnityEngine;
using ObjectTemplate;

namespace Weapon
{
    public abstract class Weapon : OptimizeBehaviour
    {
        /// <summary>
        /// 특수 행동 키코드
        /// </summary>
        public KeyCode specialActionKey =>_specialActionKey;
        [SerializeField]
        /// <summary>
        /// 특수 행동 키
        /// </summary>
        protected KeyCode _specialActionKey;

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

        [SerializeField]
        /// <summary>
        /// 특수 행동 딜레이
        /// </summary>
        protected float _specialActionDelay = 2f;

        /// <summary>
        /// 이전에 특수 행동을 사용한 시간
        /// </summary>
        protected float _prevSpecialActionTime = 0f;

        protected virtual void Start() {
            _prevAttackTime = _attackDelay;                             // 즉시 공격할수 있도록 설정
        }

        /// <summary>
        /// 무기 공격 함수
        /// </summary>
        public virtual void Excute()
        {
            if (_prevAttackTime + _attackDelay > Time.time)
            {            // 공격 가능한 시간이 아니면
                return;                                                 // 함수 종료
            }

            _prevAttackTime = Time.time;                                // 이전에 무기를 사용한 시간 저장

            WeaponAction();
            // 무기의 기능 사용

        }

        /// <summary>
        /// 무기의 기능
        /// ex ) 총 : 총알 발사, 검 : 검 휘두르기
        /// </summary>
        protected abstract void WeaponAction();

        public virtual void ExcuteSpecial(){
            if (_prevSpecialActionTime + _specialActionDelay > Time.time){           // 특수 기능을 사용가능한 시간이 아니면
                return;                                                              // 함수 종료
            }
            
            _prevSpecialActionTime = Time.time;                                       // 특수 기능을 사용한 시간 저장

            SpecialAction();                                                          // 특수 기능 사용
        }

        /// <summary>
        /// 무기의 특수 기능 있어도 되고 없어도됨
        /// </summary>
        public abstract void SpecialAction();
    }
}