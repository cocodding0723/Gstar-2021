using System;
using UnityEngine;
using UnityEngine.AI;
using ObjectTemplate;

namespace ObjectTemplate.Behaviour{
    public abstract class CharacterBehviour : OptimizeBehaviour {
        [HideInInspector, NonSerialized]
        private Animator _animator;

        /// <summary>
        /// Gets the Aniamtor attached to the obejct
        /// </summary>
        public Animator animator { get { return _animator ? _animator : (_animator = GetComponent<Animator>()); } }

        [HideInInspector, NonSerialized]
        private NavMeshAgent _navMeshAgent;

        /// <summary>
        /// Gets the NavMeshAgent attached to the obejct
        /// </summary>
        public NavMeshAgent navMeshAgent { get { return _navMeshAgent ? _navMeshAgent : (_navMeshAgent = GetComponent<NavMeshAgent>()); } }
    }
}