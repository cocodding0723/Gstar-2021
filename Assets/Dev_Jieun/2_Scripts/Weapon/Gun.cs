using System.Collections;
using UnityEngine;
using ObjectTemplate.Pattern;

namespace Weapon
{
    public class Gun : Weapon
    {
        [SerializeField]
        protected Transform gunMuzzle;              // 총구 위치

        [SerializeField]
        protected GameObject bulletPrefab;          // 총알 프리팹

        [SerializeField]
        protected float bulletRemoveTime = 3f;

        protected virtual void Awake() {
            SimplePool.Preload(bulletPrefab, 20);   // 오브젝트 풀에 총알 오브젝트 캐싱
        }

        public override void Excute(){
            if (_prevAttackTime + _attackDelay > Time.time){           // 공격 가능한 시간이 아니면
                return;                                                 // 함수 종료
            }
            
            _prevAttackTime = Time.time;
            // 무기 공격에 관한 정의를 상속해서 사용

            Bullet bullet = SimplePool.Spawn(bulletPrefab, gunMuzzle.position, gunMuzzle.rotation).GetComponent<Bullet>();      // 오브젝트 풀에서 오브젝트 호출

            // 여기에서 총알이 부딪혔을때 함수
            bullet.onTriggerEnter += () => SimplePool.Despawn(bullet.gameObject);
        }
    }
}