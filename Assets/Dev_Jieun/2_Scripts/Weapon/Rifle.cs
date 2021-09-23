using System.Collections;
using UnityEngine;
using ObjectTemplate.Pattern;

namespace Weapon
{
    using Character;

    public class Rifle : Gun
    {

        protected override void WeaponAction(){
            // 라이플에서 총알이 발사되는 스크립트 작성
            // 탄 퍼짐 o
        }

        /// <summary>
        /// 재장전 기능
        /// </summary>
        public override void SpecialAction()
        {
            
        }
    }
}