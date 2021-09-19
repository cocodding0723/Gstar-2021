using UnityEngine;
using ObjectTemplate;

namespace Weapon
{
    using Character;

    public abstract class DamagableObject : OptimizeBehaviour, IDamagable
    {
        /// <summary>
        /// 피해량
        /// </summary>
        public float damage => _damage;
        [SerializeField]
        protected float _damage = 30f;

        /// <summary>
        /// 캐릭터에 데미지를 가하는 함수
        /// </summary>
        /// <param name="character">데미지를 가할 캐릭터</param>
        public virtual void OnInflict(Character character)
        {
            character.HP -= _damage;
        }
    }
}