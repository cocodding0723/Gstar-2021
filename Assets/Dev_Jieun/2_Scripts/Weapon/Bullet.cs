using UnityEngine;
using UnityEngine.Events;

namespace Weapon
{
    using Character;

    /// <summary>
    /// 총에서 발사되는 총알
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : DamagableObject
    {
        public UnityEvent onTriggerEnter;
        public UnityEvent onDisable;

        [SerializeField]
        /// <summary>
        /// 총알 속도
        /// </summary>
        protected float _speed = 50f;

        [SerializeField]
        /// <summary>
        /// 총알이 생성된 후 종료되는 시간
        /// </summary>
        protected float _disableTime = 3f;

        [SerializeField]
        /// <summary>
        /// 총알의 사거리
        /// </summary>
        protected float _maxDistance = 100f;

        protected Vector3 startPosition;

        protected virtual void Start()
        {
            collider.isTrigger = true;          // OnTriggerEnter를 사용하기 위해 true
            rigidbody.useGravity = false;       // 총알이 아래로 휘지 않게 중력을 끔
            rigidbody.interpolation = RigidbodyInterpolation.Interpolate;       // 리지드바디 설정
        }

        protected virtual void OnEnable()
        {
            rigidbody.AddForce(this.transform.forward * _speed);         // 오브젝트가 켜지면 앞방향으로 힘을 가함
        }

        protected virtual void OnDisable()
        {
            rigidbody.velocity = Vector3.zero;                          // 해당 총알의 속도를 초기화

            onDisable.Invoke();
            onDisable.RemoveAllListeners();
        }

        protected virtual void Update()
        {
            if (Vector3.Distance(startPosition, this.transform.position) > _maxDistance)
            {
                if (this.gameObject.activeSelf)                             // 게임 오브젝트가 켜져있을때
                {
                    this.gameObject.SetActive(false);                       // 오브젝트 끄기
                }
            }
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            Character character = other.GetComponent<Character>();      // 충돌한 대상에게서 캐릭터 컴포넌트를 가져옴

            if (character != null)                                      // 만약 충돌한 대상에게서 캐릭터 컴포넌트가 존재한다면
            {
                OnInflict(character);                                   // 해당 캐릭터에게 피해를 줌
            }

            if (this.gameObject.activeSelf)                             // 게임 오브젝트가 켜져있을때
            {
                onTriggerEnter.Invoke();
                onTriggerEnter.RemoveAllListeners();
                this.gameObject.SetActive(false);                       // 오브젝트 끄기
            }
        }
    }
}