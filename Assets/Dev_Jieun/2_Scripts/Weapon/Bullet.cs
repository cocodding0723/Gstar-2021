using System;
using System.Collections;
using UnityEngine;
using ObjectTemplate.Pattern;

namespace Weapon
{
    using Character;

    /// <summary>
    /// 총에서 발사되는 총알
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : DamagableObject
    {
        public event Action onTriggerEnter;

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

        protected virtual void Start() {
            collider.isTrigger = true;          // OnTriggerEnter를 사용하기 위해 true
            rigidbody.useGravity = false;       // 총알이 아래로 휘지 않게 중력을 끔
        }

        protected void OnEnable() {
            rigidbody.AddForce(this.transform.forward * _speed);         // 오브젝트가 켜지면 앞방향으로 힘을 가함

            StartCoroutine(DelayDisable());
        }

        protected virtual void OnTriggerEnter(Collider other) {
            Character character = other.GetComponent<Character>();      // 충돌한 대상에게서 캐릭터 컴포넌트를 가져옴

            if (character != null){                                     // 만약 충돌한 대상에게서 캐릭터 컴포넌트가 존재한다면
                OnInflict(character);                                   // 해당 캐릭터에게 피해를 줌
            }

            onTriggerEnter();                                           // 이벤트 함수 실행
            ClearTriggerEvent();                                        // 해당 이벤트 초기화
        }

        private void ClearTriggerEvent(){
            foreach(Delegate d in onTriggerEnter.GetInvocationList()){
                onTriggerEnter -= (Action)d;
            }
        }

        protected virtual IEnumerator DelayDisable(){
            yield return new WaitForSeconds(_disableTime);

            if (gameObject.activeSelf){
                onTriggerEnter();
                ClearTriggerEvent(); 
            }
        }
    }
}