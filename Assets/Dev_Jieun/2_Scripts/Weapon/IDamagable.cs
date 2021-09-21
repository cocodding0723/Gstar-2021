namespace Weapon{
    using Character;

    /// <summary>
    /// 피해를 입힐수 있는 오브젝트
    /// </summary>
    public interface IDamagable{
        /// <summary>
        /// 오브젝트의 데미지
        /// </summary>
        /// <value>데미지</value>
        float damage { get; }

        /// <summary>
        /// 데미지 오브젝트를 사용한 캐릭터, 피해를 입지 않음
        /// </summary>
        /// <value></value>
        Character caster { get; set; }

        /// <summary>
        /// 캐릭터에게 데미지를 가하는 함수
        /// </summary>
        /// <param name="character">데미지를 받을 캐릭터</param>
        void OnInflict(Character character);
    }
}