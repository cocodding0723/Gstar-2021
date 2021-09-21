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
        /// 시전자
        /// </summary>
        /// <value></value>
        public Character caster { get => _caster; set => _caster = value; }
        protected Character _caster = null;

        /// <summary>
        /// 캐릭터에 데미지를 가하는 함수
        /// </summary>
        /// <param name="character">데미지를 가할 캐릭터</param>
        public virtual void OnInflict(Character character)
        {
            if (character != caster){           // 시전자와 캐릭터가 다르다면 데미지를 입힘
                character.HP -= _damage;
            }
        }
    }
}