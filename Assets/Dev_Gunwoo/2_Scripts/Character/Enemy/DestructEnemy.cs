using System.Collections;
using UnityEngine;

namespace Character
{
    /// <summary>
    /// 플레이어에게서 달려와 자폭하는 적
    /// </summary>
    public class DestructEnemy : Enemy
    {
        protected override IEnumerator Attack()
        {
            throw new System.NotImplementedException();
        }

        protected override IEnumerator Idle()
        {
            throw new System.NotImplementedException();
        }

        protected override IEnumerator Patrol()
        {
            throw new System.NotImplementedException();
        }
    }
}