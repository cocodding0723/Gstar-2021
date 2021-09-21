using System.Collections;
using UnityEngine;
using ObjectTemplate.Pattern;

namespace Weapon
{
    using Character;

    public class Gun : Weapon
    {
        [SerializeField]
        /// <summary>
        /// 총을 들고있는 캐릭터
        /// </summary>
        protected Character owner = null;

        [SerializeField]
        /// <summary>
        /// 총구 위치
        /// </summary>k
        protected Transform gunMuzzle = null;        

        [SerializeField]
        /// <summary>
        /// 총알 프리팹
        /// </summary>
        protected GameObject bulletPrefab = null;          

        protected virtual void Awake() {
            SimplePool.Preload(bulletPrefab, 10);   // 오브젝트 풀에 총알 오브젝트 캐싱
        }

        protected override void WeaponAction(){
            Bullet bullet = SimplePool.Spawn(bulletPrefab, gunMuzzle.position, gunMuzzle.rotation).GetComponent<Bullet>();

            bullet.caster = owner;                          // 총알을 발사한 사람

            bullet.onDisable.AddListener(() => SimplePool.Despawn(bullet.gameObject));       // 총알이 부딪혔을때 다시 풀에 돌아가게
            bullet.onDisable.AddListener(() => bullet.GetComponent<TrailRenderer>().Clear());
        }
    }
}