using System;
using UnityEngine;
using UnityEngine.Events;

namespace Character
{
    public class Player : Character
    {
        // TODO : 플레이어 사망시 발생하는 이벤트
        // 1. 쓰러지는 카메라 워크
        // 2. 키 입력시 다음 스테이지로 이동 : Fade In / Out 효과
        // 3. 적들 공격 중지

        /// <summary>
        /// 플레이어 사망시 발생하는 이벤트
        /// </summary>
        public static event Action onPlayerDie;

        /// <summary>
        /// 플레이어 사망시 발생하는 이벤트, 인스펙터 창에서 설정 가능한 이벤트
        /// </summary>
        public UnityEvent _onPlayerDie;

        protected override void Die()
        {
            onPlayerDie();
            _onPlayerDie.Invoke();
        }
    }
}