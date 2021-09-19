namespace Weapon{
    using Character;

    /// <summary>
    /// 피해를 입힐수 있는 오브젝트
    /// </summary>
    public interface IDamagable{
        float damage { get; }
        void OnInflict(Character character);
    }
}