using UnityEngine;
using ZombeezGameJam.Managers;
using ZombeezGameJam.Stats;
using ZombeezGameJam.Weapons;

namespace ZombeezGameJam.Entities.Enemies
{
    public enum ZombieStates
    {
        Spawn,
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

        [SerializeField] private WeaponStats[] _weaponStats;
        [SerializeField] private WeaponPickup _weaponPickup;

        [SerializeField] internal Transform target;
        [SerializeField] private LayerMask _targetLayers;
        internal GameObject player;

        [SerializeField] internal Transform patrolStartPosition;
        [SerializeField] internal Transform patrolEndPosition;

        [SerializeField] internal float attackRange;
        [SerializeField] internal float chaseRange;
        [SerializeField] internal float chaseBuffer;

        [SerializeField] internal int attackStrength = 5;

        public BaseEntityStats Stats
        {
            get => _stats;
            set => _stats = value;
        }

        [SerializeField] internal bool isShuffler;
        [SerializeField] internal bool hasSecondAttack;

        [SerializeField] internal float secondaryAttackRange;

        internal bool isSecondaryAttackReady;

        internal ZombieStates currentState;

        private Collider2D[] _entityOverlaps;

        [SerializeField] internal AudioClip[] movementAudio;
        [SerializeField] internal AudioClip attackAudio;
        [SerializeField] internal AudioClip deathAudio;

        #region Unity Methods

        public override void Start()
        {
            base.Start();
            isSecondaryAttackReady = true;

            currentState = ZombieStates.Spawn;
            UpdateZombieState(ZombieStates.Spawn);

            ColourLogger.RegisterColour(this, "green");
        }

        private void Update()
        {
            if (currentState == ZombieStates.Attack || currentState == ZombieStates.Attack2)
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

                if (CheckPlayerInSecondaryAttackRange(distanceToEntity) && hasSecondAttack && isSecondaryAttackReady)
                {
                    combatScript.SecondaryAttack();
                } else if (distanceToEntity < attackRange)
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

        public override void ApplyEntityStats()
        {
            base.ApplyEntityStats();

            animationScript._animator.runtimeAnimatorController = _stats.Controller;

            if (_stats is ZombieStats zombieStats)
            {
                chaseRange = zombieStats.ChaseRange;
                chaseBuffer = zombieStats.ChaseBuffer;
                attackRange = zombieStats.AttackRange;

                attackStrength = zombieStats.AttackStrength;

                isShuffler = zombieStats.IsShuffler;
                hasSecondAttack = zombieStats.HasSecondAttack;
                secondaryAttackRange = zombieStats.SecondaryAttackRange;

                movementAudio = zombieStats.MovementAudio;
                attackAudio = zombieStats.AttackAudio;
                deathAudio = zombieStats.DeathAudio;
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

            //if (currentState == ZombieStates.Spawn)
            //{
            //    boxCollider.enabled = false;
            //    circleCollider.enabled = false;
            //} else
            //{
            //    boxCollider.enabled = true;
            //    circleCollider.enabled = true;
            //}
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

        private bool CheckPlayerInSecondaryAttackRange(float a_distanceToTarget)
        {
            return a_distanceToTarget < secondaryAttackRange && a_distanceToTarget > attackRange && target.CompareTag("Player") && Random.value > 0.95f;
        }

        public override void OnDeath()
        {
            base.OnDeath();

            ColourLogger.Log(this, "Grrr (ouch in ZomB)");
            ColourLogger.LogWarning(this, "Grrr (ouch in ZomB)");
            ColourLogger.LogError(this, "Grrr (ouch in ZomB)");

            if (Random.value >= 0.2f && _weaponPickup != null)
            {
                WeaponPickup weapon = Instantiate(_weaponPickup, transform.position, Quaternion.identity);
                weapon.DroppedWeapon = _weaponStats[Random.Range(0, _weaponStats.Length)];
                Destroy(weapon.gameObject, 30f);
                //Debug.Log(weapon.DroppedWeapon.name);
            }

            if (deathAudio != null)
            {
                SoundFXManager.instance.PlaySoundClip(deathAudio, transform, a_priority: 80);
            }

            GameManager.instance.CountdownZombies();
        }

        #endregion Custom Methods
    }
}
