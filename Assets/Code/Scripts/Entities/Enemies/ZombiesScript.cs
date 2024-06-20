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
        [SerializeField] internal ZombieMovementScript movementScript;
        [SerializeField] internal ZombieHitbox hitboxScript;

        [SerializeField] private GameObject _weaponDrop;

        [SerializeField] internal Transform _target;
        internal Transform _player;

        [SerializeField] internal float _attackRange;
        [SerializeField] internal float _chaseRange;

        [SerializeField] internal float movementSpeed = 100f;

        internal ZombieStates currentState;

        public override void Start()
        {
            base.Start();
            UpdateZombieState(ZombieStates.Idle);
        }

        public override void Update()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            if (_player != null)
            {
                if (Vector3.Distance(_player.position, transform.position) < _chaseRange)
                {
                    _target = _player;
                }
            }
        }

        internal void UpdateZombieState(ZombieStates a_newState)
        {
            if (currentState == a_newState)
            {
                return;
            }

            currentState = a_newState;
            animationScript.UpdateAnimationState();
            if (currentState == ZombieStates.Patrol)
            {
                movementScript.StartRoam();
            }
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

        internal float IsFacingTarget()
        {
            //float dot = Vector3.Dot(transform.right, (_target.position - transform.position).normalized);
            float direction = _target.position.x - transform.position.x;
            Debug.Log(direction / Mathf.Abs(direction));
            return direction / Mathf.Abs(direction);
        }
    }
}
