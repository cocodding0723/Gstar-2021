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
        }

        /// <summary>
        /// 재장전 기능
        /// </summary>
        public override void SpecialAction()
        {
            
        }
    }
}