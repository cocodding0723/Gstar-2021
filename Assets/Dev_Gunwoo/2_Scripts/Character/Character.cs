using UnityEngine;
using ObjectTemplate;

// TODO : 캐릭터관련 스크립트 시 Character 네임스페이스를 사용해주세요.
namespace Character
{
    /// <summary>
    /// 캐릭터에 관한 기본적인 기능, 정보가 들어있는 스크립트
    /// 1. 캐릭터의 체력
    /// 2. 캐릭터의 이벤트 제어
    /// </summary>
    public abstract class Character : OptimizeBehaviour, IHP
    {
        /// <summary>
        /// 캐릭터가 가질 수 있는 최대 체력입니다.
        /// </summary>
        /// <value>캐릭터의 최대 체력량입니다.</value>
        public float Max { get => _Max; protected set => _Max = value; }
        private float _Max = 100f;

        /// <summary>
        /// 캐릭터의 현재 체력입니다.
        /// </summary>
        /// <value>캐릭터의 현재 체력 량입니다.</value>
        public float HP { get => _HP; set => _HP = Mathf.Clamp(value, 0f, _Max); }
        private float _HP = 100f;

        /// <summary>
        /// 캐릭터의 사망 여부입니다.
        /// true : 살아 있음
        /// false : 사망
        /// </summary>
        /// <value></value>
        public bool isDie { get => _HP <= 0f; }

        protected virtual void Start(){
            _HP = _Max;
            Debug.Log(this.gameObject.name + " 캐릭터에 체력 미지정 Default 100 으로 설정됨.");
        }

        protected virtual void Update()
        {
            if (isDie)
            {
                Die();
            }
        }

        /// <summary>
        /// 캐릭터가 사망시 실행할 함수입니다.
        /// </summary>
        protected abstract void Die();
    }
}