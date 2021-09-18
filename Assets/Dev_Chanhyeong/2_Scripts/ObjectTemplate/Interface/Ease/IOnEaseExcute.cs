namespace ObjectTemplate.Interface{
    /// <summary>
    /// 이 인터페이스는 ObjectTemplate.ScraptableObject에 사용하기 위한 함수를 정의한 인터페이스 입니다.
    /// </summary>
    public interface IOnEaseExcute
    {
        /// <summary>
        /// Ease가 실행 중일때 실행됩니다.
        /// </summary>
        /// <param name="t">t는 0부터 1값만 들어갈 수 있습니다. t값은 Ease곡선의 t지점의 값이 들어갑니다.</param>
        void OnEaseExcute(float t);
    }
}