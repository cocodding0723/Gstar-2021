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
        protected Character _owner = null;

        [SerializeField]
        /// <summary>
        /// 총구 위치
        /// </summary>
        protected Transform _gunMuzzle = null;        

        [SerializeField]
        /// <summary>
        /// 총알 프리팹
        /// </summary>
        protected GameObject bulletPrefab = null;

        [SerializeField]
        /// <summary>
        /// 남아있는 탄창 갯수
        /// </summary>
        protected int _remainAmmo = 100;

        [SerializeField]
        /// <summary>
        /// 총의 최대 탄약 갯수
        /// </summary>
        protected int _maxAmmo = 30;

        /// <summary>
        /// 현재 탄약 갯수
        /// </summary>
        protected int _currentAmmo = 30;

        protected virtual void Awake() {
            _currentAmmo = _maxAmmo;
            _specialActionKey = KeyCode.R;
            SimplePool.Preload(bulletPrefab, 10);   // 오브젝트 풀에 총알 오브젝트 캐싱
        }

        protected override void WeaponAction(){
            if (_currentAmmo > 0){
                Bullet bullet = SimplePool.Spawn(bulletPrefab, _gunMuzzle.position, _gunMuzzle.rotation).GetComponent<Bullet>();

                bullet.caster = _owner;                          // 총알을 발사한 사람

                _currentAmmo--;

                bullet.onDisable.AddListener(() => SimplePool.Despawn(bullet.gameObject));       // 총알이 부딪혔을때 다시 풀에 돌아가게
                bullet.onDisable.AddListener(() => bullet.GetComponent<TrailRenderer>().Clear());
            }
        }

        /// <summary>
        /// 재장전 기능
        /// </summary>
        public override void SpecialAction()
        {
            if (_maxAmmo > _currentAmmo){
                if (_maxAmmo <= _remainAmmo){
                    _remainAmmo -= _maxAmmo - _currentAmmo;
                    _currentAmmo = _maxAmmo;
                }
                else{
                    _currentAmmo += _remainAmmo;
                    _remainAmmo = 0;
                }
            }
        }
    }
}