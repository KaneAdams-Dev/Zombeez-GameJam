using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombeezGameJam.Entities.Enemies
{
    public enum ZombieStates
    {
        Idle,
        Patrol,
        Chase,
        Attack,
    }

    public class ZombiesScript : BaseEntity
    {
        [Header("Script References")]
        [SerializeField] internal ZombieCombatScript combatScript;
        [SerializeField] internal ZombieAnimationScript animationScript;
        [SerializeField] internal ZombieHitbox hitboxScript;

        [SerializeField] private GameObject _weaponDrop;

        [SerializeField] internal float _attackRange;
        [SerializeField] internal float _chaseRange;

        internal ZombieStates currentState;

        public override void Start()
        {
            base.Start();
            UpdateZombieState(ZombieStates.Idle);
        }

        internal void UpdateZombieState(ZombieStates a_newState)
        {
            if (currentState == a_newState)
            {
                return;
            }

            currentState = a_newState;
            animationScript.UpdateAnimationState();
        }

        public override void OnDeath()
        {
            base.OnDeath();

            if (Random.value >= 0.2f)
            {
                if (_weaponDrop != null)
                {
                    Instantiate(_weaponDrop, transform.position, Quaternion.identity);
                }
            }
        }
    }
}
