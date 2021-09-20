using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Character
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class Enemy : Character
    {
        /// <summary>
        /// 행동 상태
        /// </summary>
        public enum State{
            Idle,       // Idle 상태
            Patrol,     // 순찰 상태
            Attack,     // 공격 상태
            Trace       // 추격 상태
        }

        [SerializeField]
        protected FieldOfView _fieldOfView = null;          // 시야각 내에 있는지 검사

        [SerializeField]
        protected float attackRange = 5f;                   // 공격 범위
        [SerializeField]
        protected float moveSpeed = 5f;

        protected State prevState;                          // 이전 상태
        protected State currentState;                       // 현재 상태

        protected Transform _target;                        // Player
        protected NavMeshAgent _navMeshAgent;

        protected virtual void Start(){
            _fieldOfView = this.GetComponentInChildren<FieldOfView>();      // 자식오브젝트 내에 있는 field of view 캐싱
            _navMeshAgent = this.GetComponent<NavMeshAgent>();              // navmeshagent 캐싱
            _target = FindObjectOfType<PlayerCharacter>().transform;        // 플레이어 캐싱

            prevState = State.Idle;
            currentState = State.Patrol;
        }
        protected override void Update(){
            base.Update();

            if (_fieldOfView.visibleTargets.Count > 0){                                             // 시야 내에 플레이어가 들어 온다면
                if (Vector3.Distance(_target.position, this.transform.position) > attackRange){     // 플레이어와 사이가 공격 범위보다 크다면
                    currentState = State.Trace;                                                     // 추격 상태
                }
                else{                                                                               // 공격 범위 내에 들어온다면
                    currentState = State.Attack;                                                    // 공격 상태
                }
            }

            if (currentState != prevState){                             // 현재 상태와 이전 상태가 다르면 실행
                prevState = currentState;                               // 이전 상태 저장
                StartCoroutine(currentState.ToString());                // 열거형을 string으로 변환시켜 해당 코루틴 실행
            }
        }

        protected override void Die(){
            
        }

        #region 상속 후 정의해야하는 부분
        protected abstract IEnumerator Idle();
        protected abstract IEnumerator Patrol();
        protected abstract IEnumerator Trace();
        protected abstract IEnumerator Attack();
        #endregion

        protected virtual void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, attackRange);
        }
    }
}