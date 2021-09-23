using System.Collections;
using UnityEngine;
using ObjectTemplate.Pattern;

namespace Weapon
{
    using Character;

    public class Pistol : Gun
    {

        protected override void WeaponAction(){
            // 권총에서 총알이 발사되는 스크립트 작성
            // 탄 퍼짐 x
        }

        public override void SpecialAction()
        {
            
        }
    }
}