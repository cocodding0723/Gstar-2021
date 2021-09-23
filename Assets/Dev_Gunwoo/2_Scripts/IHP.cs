
/// <summary>
/// 체력을 설정하는 인터페이스입니다.
/// </summary>
public interface IHP
{
    /// <summary>
    /// 최대 체력입니다.
    /// </summary>
    /// <value>최대 체력량입니다.</value>
    float Max { get; }

    /// <summary>
    /// 현재 체력입니다.
    /// </summary>
    /// <value>현재 체력 량입니다.</value>
    float HP { get; set; }
}
