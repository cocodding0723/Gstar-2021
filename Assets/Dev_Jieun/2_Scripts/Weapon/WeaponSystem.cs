using System;
using System.Collections.Generic;
using UnityEngine;
using ObjectTemplate;

namespace Weapon
{
    public class WeaponSystem : OptimizeBehaviour
    {
        [SerializeField]
        private List<Weapon> weapons;                   // 무기들
        private Weapon currentWeapon = null;            // 현재 무기

        private int prevWeaponIndex = 0;                // 이전에 들고있었던 무기 번호
        private int currentWeaponIndex = 0;             // 현재 들고있는 무기 번호

        private void Start()
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                weapons[i].gameObject.SetActive(false);
            }

            if (weapons.Count > 0)
            {
                prevWeaponIndex = 0;
                currentWeaponIndex = 0;
                currentWeapon = weapons[0];
                currentWeapon.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError("현재 보유중인 무기 없음");
            }
        }

        private void FixedUpdate()
        {
            if (Input.GetMouseButton(0) && currentWeapon != null)       // 현재 무기가 적용되어 있는 경우, 마우스 좌클릭 시 무기 사용
            {
                currentWeapon.Excute();
            }

            if (currentWeapon != null && Input.GetKeyDown(currentWeapon.specialActionKey)){
                currentWeapon.ExcuteSpecial();
            }

            ChangeIndexToScroll();
            ChangeIndexToKeboard();

            // 범위 제한
            if (weapons.Count > 1)
            {
                if (currentWeaponIndex >= weapons.Count)
                {
                    currentWeaponIndex = 0;
                }
                if (currentWeaponIndex < 0)
                {
                    currentWeaponIndex = weapons.Count - 1;
                }
            }
            else
            {
                currentWeaponIndex = 0;
            }

            if (currentWeaponIndex != prevWeaponIndex)                      // 현재 무기가 이전에 사용한 무기와 다를때
            {                     
                weapons[prevWeaponIndex].gameObject.SetActive(false);       // 이전 무기 끄기
                weapons[currentWeaponIndex].gameObject.SetActive(true);     // 현재 무기 키기
                currentWeapon = weapons[currentWeaponIndex];                // 현재 무기 설정
                prevWeaponIndex = currentWeaponIndex;                       // 이전 인덱스를 현재 인덱스로 설정
            }
        }

        /// <summary>
        /// 마우스 스크롤로 현재 무기 인덱스를 변경하는 함수
        /// </summary>
        private void ChangeIndexToScroll()
        {
            float mouseScroll = Input.GetAxisRaw("Mouse ScrollWheel");      // 마우스 스크롤 입력을 받아옴

            if (mouseScroll > 0f)                                           // 마우스 스크롤을 위로 굴렸을때
            {                                         
                currentWeaponIndex--;                                       // 현재 무기를 이전 무기로
            }
            if (mouseScroll < 0f)                                           // 마우스 스크롤을 아래로 굴렸을때
            {                                          
                currentWeaponIndex++;                                       // 현재 무기를 다음 무기로
            }
        }

        /// <summary>
        /// 키보드로 현재 무기 인덱스를 변경하는 함수
        /// </summary>
        private void ChangeIndexToKeboard()
        {
            for (int i = 0; i < 10; i++)
            {
                if (Input.GetKeyDown(GetIndexToAlphaKeyCode(i)))            // 현재 i값을 키코드로 변환후 검사
                {           
                    currentWeaponIndex = i - 1;                             // 1번이 0번무기여야 하므로 -1을 해줌
                }
            }
        }

        /// <summary>
        /// 매개변수로 넘어온 정수를 키보드의 KeyCode로 바꾸는 함수
        /// /// </summary>
        /// <param name="index">키보드 숫자</param>
        /// <returns></returns>
        private KeyCode GetIndexToAlphaKeyCode(int index)
        {
            // 참고 : https://magicmon.tistory.com/102
            // 문자열을 Enum형으로 바꾸는 기능
            return (KeyCode)Enum.Parse(typeof(KeyCode), "Alpha" + index);
        }
    }
}