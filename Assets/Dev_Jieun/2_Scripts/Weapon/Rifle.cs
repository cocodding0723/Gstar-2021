using System.Collections;
using UnityEngine;
using ObjectTemplate.Pattern;

namespace Weapon
{
    using Character;

    public class Rifle : Gun
    {

        protected override void WeaponAction(){
            // 라이플에서 총알이 발사되는 스크립트 작성
            // 탄 퍼짐 o

            if(_currentAmmo > 0)
            {
                Bullet bullet = SimplePool.Spawn(bulletPrefab, _gunMuzzle.position, _gunMuzzle.rotation).GetComponent<Bullet>();

                bullet.caster = _owner;

                _currentAmmo--;

                bullet.onDisable.AddListener(() => SimplePool.Despawn(bullet.gameObject));
                bullet.onDisable.AddListener(() => bullet.GetComponent<TrailRenderer>().Clear());
            }
        }

        /// <summary>
        /// 재장전 기능
        /// </summary>
        public override void SpecialAction()
        {
            base.SpecialAction();

            // 장전 애니메이션 
        }
    }
}