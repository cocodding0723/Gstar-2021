using System.Collections.Generic;
using UnityEngine;
using ObjectTemplate;

namespace Weapon
{
    public class WeaponSystem : OptimizeBehaviour
    {
        [SerializeField]
        private List<Weapon> weapons;                   // 무기들
        private Weapon currentWeapon = null;                   // 현재 무기

        private void Start() {
            for (int i = 0; i < weapons.Count; i++){
                weapons[i].gameObject.SetActive(false);
            }

            currentWeapon = weapons[0];
            currentWeapon.gameObject.SetActive(true);
        }

        private void Update() {
            if (Input.GetMouseButton(0)){
                currentWeapon.Execute();
            }
        }
    }
}