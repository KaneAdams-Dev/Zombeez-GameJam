using UnityEngine;
using ZombeezGameJam.Stats;

namespace ZombeezGameJam.Entities.Enemies
{
    public enum ZombieStates
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Attack2,
    }

    public class Zombie : BaseEntity
    {
        [Header("Script References")]
        [SerializeField] internal ZombieCombatScript combatScript;
        [SerializeField] internal ZombieAnimationScript animationScript;
        [SerializeField] internal ZombieMovementScript movementScript;
        [SerializeField] internal ZombieHitbox hitboxScript;

        [SerializeField] private GameObject _weaponDrop;

        [SerializeField] internal Transform target;
        [SerializeField] private LayerMask _targetLayers;
        internal GameObject player;

        [SerializeField] internal Transform patrolStartPosition;
        [SerializeField] internal Transform patrolEndPosition;

        [SerializeField] internal float attackRange;
        [SerializeField] internal float chaseRange;
        [SerializeField] internal float chaseBuffer;

        //[SerializeField] internal float movementSpeed = 100f;

        public BaseEntityStats Stats
        {
            get => _stats;
            set => _stats = value;
        }
        
        [SerializeField] internal bool isShuffler;
        [SerializeField] internal bool hasSecondAttack;

        internal ZombieStates currentState;

        private Collider2D[] _entityOverlaps;

        [SerializeField] internal AudioClip[] movementAudio;
        [SerializeField] internal AudioClip attackAudio;

        #region Unity Methods

        private void Awake()
        {
            currentState = ZombieStates.Idle;
            UpdateZombieState(ZombieStates.Idle);
        }

        public override void Start()
        {
            base.Start();
        }

        public override void ApplyEntityStats()
        {
            base.ApplyEntityStats();

            animationScript._animator.runtimeAnimatorController = _stats.Controller;

            if (_stats is ZombieStats zombieStats)
            {
                chaseRange = zombieStats.ChaseRange;
                chaseBuffer = zombieStats.ChaseBuffer;
                attackRange = zombieStats.AttackRange;

                isShuffler = zombieStats.IsShuffler;
                hasSecondAttack = zombieStats.HasSecondAttack;

                movementAudio = zombieStats.MovementAudio;
                attackAudio = zombieStats.AttackAudio;
            }
        }

        private void Update()
        {
            if (currentState == ZombieStates.Attack)
            {
                return;
            }

            _entityOverlaps = Physics2D.OverlapCircleAll(transform.position, chaseRange + chaseBuffer, _targetLayers.value);

            if (_entityOverlaps.Length == 0)
            {
                UpdateZombieState(ZombieStates.Patrol);
                return;
            }

            player = FindClosestTarget(_entityOverlaps);

            float distanceToEntity = Vector3.Distance(player.transform.position, transform.position);
            if (distanceToEntity < chaseRange)
            {
                target = player.transform;

                if (distanceToEntity < attackRange)
                {
                    combatScript.Attack();
                } else
                {
                    UpdateZombieState(ZombieStates.Chase);
                }
            } else
            {
                UpdateZombieState(distanceToEntity > (chaseRange + chaseBuffer) ? ZombieStates.Patrol : ZombieStates.Chase);

            }
            //if (distanceToEntity < (_chaseRange + _chaseBuffer))
            //{
            //    UpdateZombieState(ZombieStates.Patrol);
            //}
        }

        #endregion Unity Methods

        #region Custom Methods

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

        internal GameObject FindClosestTarget(Collider2D[] a_potentialTargets)
        {
            GameObject closest = null;
            float closestDistance = 100f;

            foreach (Collider2D target in a_potentialTargets)
            {
                float distanceToEntity = Vector3.Distance(target.transform.position, transform.position);

                if (closest == null || distanceToEntity < closestDistance)
                {
                    closest = target.gameObject;
                    closestDistance = distanceToEntity;
                }
            }

            return closest;
        }

        internal float IsFacingTarget()
        {
            float direction = target.position.x - transform.position.x;
            return direction / Mathf.Abs(direction);
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

            GameManager.instance.CountdownZombies();
        }

        #endregion Custom Methods
    }
}
