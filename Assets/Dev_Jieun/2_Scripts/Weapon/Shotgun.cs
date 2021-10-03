using System.Collections;
using UnityEngine;
using ObjectTemplate.Pattern;

namespace Weapon
{
    using Character;

    public class Shotgun : Gun
    {

        protected override void WeaponAction(){
            // 샷건에서 총알이 발사되는 스크립트 작성
            // 랜덤하게 탄퍼짐
            // 총알이 여러개 나감
            if (_currentAmmo > 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    float X = Mathf.Atan2(y: 30, x: 100) * Mathf.Rad2Deg; //각도계산

                    Quaternion rot = _gunMuzzle.rotation * Quaternion.Euler(new Vector3(Random.Range(-X, X), Random.Range(-X, X), 0)); //계산한 각도 안에서의 랜덤값 설정

                    Bullet bullet = SimplePool.Spawn(bulletPrefab, _gunMuzzle.position, rot).GetComponent<Bullet>();

                    bullet.caster = _owner;

                    _currentAmmo--;

                    bullet.onDisable.AddListener(() => SimplePool.Despawn(bullet.gameObject));
                    bullet.onDisable.AddListener(() => bullet.GetComponent<TrailRenderer>().Clear());
                }
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