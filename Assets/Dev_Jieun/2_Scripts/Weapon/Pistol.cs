using System.Collections;
using UnityEngine;
using ObjectTemplate.Pattern;

namespace Weapon
{
    using Character;

    public class Pistol : Gun
    {
        protected override void WeaponAction(){
            // 권총에서 총알이 발사되는 스크립트 작성
            // 탄 퍼짐 x

            if(_currentAmmo > 0)
            {
                Bullet bullet = SimplePool.Spawn(bulletPrefab, _gunMuzzle.position, _gunMuzzle.rotation).GetComponent<Bullet>();

                bullet.caster = _owner;

                _currentAmmo--;

                bullet.onDisable.AddListener(() => SimplePool.Despawn(bullet.gameObject));
                bullet.onDisable.AddListener(() => bullet.GetComponent<TrailRenderer>().Clear());
            }

        }

        public override void SpecialAction()
        {
            base.SpecialAction();

            // 장전 애니메이션 
        }
    }
}