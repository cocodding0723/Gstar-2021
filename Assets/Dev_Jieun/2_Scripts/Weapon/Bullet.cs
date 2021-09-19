using UnityEngine;

namespace Weapon
{
    using Character;

    /// <summary>
    /// 총에서 발사되는 총알
    /// </summary>
    public class Bullet : DamagableObject
    {
        [SerializeField]
        protected float _speed = 50f;

        protected virtual void Start() {
            collider.isTrigger = true;          // OnTriggerEnter를 사용하기 위해 true
            rigidbody.useGravity = false;       // 총알이 아래로 휘지 않게 중력을 끔
        }

        protected void OnEnable() {
            rigidbody.AddForce(this.transform.forward * _speed);         // 오브젝트가 켜지면 앞방향으로 힘을 가함
        }

        protected virtual void OnTriggerEnter(Collider other) {
            Character character = other.GetComponent<Character>();      // 충돌한 대상에게서 캐릭터 컴포넌트를 가져옴

            if (character != null){                                     // 만약 충돌한 대상에게서 캐릭터 컴포넌트가 존재한다면
                OnInflict(character);                                   // 해당 캐릭터에게 피해를 줌
            }

            this.gameObject.SetActive(false);                           // 충돌한 총알 비활성화
        }
    }
}