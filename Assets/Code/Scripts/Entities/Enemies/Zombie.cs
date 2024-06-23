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

    public class Zombie : BaseEntity
    {
        [Header("Script References")]
        [SerializeField] internal ZombieCombatScript combatScript;
        [SerializeField] internal ZombieAnimationScript animationScript;
        [SerializeField] internal ZombieMovementScript movementScript;
        [SerializeField] internal ZombieHitbox hitboxScript;

        [SerializeField] private GameObject _weaponDrop;

        [SerializeField] internal Transform _target;
        [SerializeField] private LayerMask _targetLayers;
        internal GameObject _player;

        [SerializeField] internal float _attackRange;
        [SerializeField] internal float _chaseRange;
        [SerializeField] internal float _chaseBuffer;

        [SerializeField] internal float movementSpeed = 100f;

        internal ZombieStates currentState;

        private Collider2D[] _entityOverlaps;

        #region Unity Methods

        public override void Start()
        {
            base.Start();

            UpdateZombieState(ZombieStates.Patrol);
        }

        private void Update()
        {
            _entityOverlaps = Physics2D.OverlapCircleAll(transform.position, _chaseRange + _chaseBuffer, _targetLayers.value);

            if (_entityOverlaps.Length == 0)
            {
                UpdateZombieState(ZombieStates.Patrol);
                return;
            }

            _player = FindClosestTarget(_entityOverlaps);

            //foreach(Collider2D entity in _entityOverlaps)
            //{
            float distanceToEntity = Vector3.Distance(_player.transform.position, transform.position);
            if (distanceToEntity < _chaseRange)
            {
                if (distanceToEntity < _attackRange)
                {
                    combatScript.Attack();
                } else
                {
                    _target = _player.transform;
                    UpdateZombieState(ZombieStates.Chase);
                }
            } else if (distanceToEntity > (_chaseRange + _chaseBuffer))
            {
                UpdateZombieState(ZombieStates.Patrol);
            }
            //}

            //_player = GameObject.FindGameObjectWithTag("Player");
            //if (_player != null)
            //{
            //    float distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);
            //    if (distanceToPlayer < _chaseRange)
            //    {
            //        if (distanceToPlayer < _attackRange)
            //        {
            //            combatScript.Attack();
            //        } else
            //        {
            //            _target = _player.transform;
            //            UpdateZombieState(ZombieStates.Chase);
            //        }
            //    } else if (distanceToPlayer > (_chaseRange + _chaseBuffer))
            //    {
            //        UpdateZombieState(ZombieStates.Patrol);
            //    }
            //} else
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
            //float dot = Vector3.Dot(transform.right, (_target.position - transform.position).normalized);
            float direction = _target.position.x - transform.position.x;
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
        }

        #endregion Custom Methods
    }
}
