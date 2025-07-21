using UnityEngine;
using ZombeezGameJam.Interfaces;
using ZombeezGameJam.Managers;
using ZombeezGameJam.Stats;

namespace ZombeezGameJam.Entities.Survivors
{
    public enum SurvivorStates
    {
        Idle,
        Flee,
        MoveToHordeCheckpoint,
        WaitingAtCheckpoint,
        MoveToLevelFinish,
    }

    public class Survivor : BaseEntity, IInteractable, IStatsApplicable
    {
        [SerializeField] internal SurvivorAnimationScript animationScript;
        [SerializeField] internal SurvivorMovementScript moveScript;

        [Header("Movement Values")]
        //[SerializeField] internal float movementSpeed = 100f;

        internal SurvivorStates currentState;

        public override void Start()
        {
            base.Start();
            //UpdateSurvivorStates(SurvivorStates.Wander);
        }

        private void OnEnable()
        {
            GameManager.OnFinalWaveBegins += RunToSafehouse;
            ColourLogger.RegisterColour(this, "orange");
        }

        private void OnDisable()
        {
            GameManager.OnFinalWaveBegins -= RunToSafehouse;
        }

        private void Update()
        {
            if (currentState == SurvivorStates.MoveToHordeCheckpoint)
            {
                if (!animationScript.spriteRenderer.isVisible)
                {
                    TeleportToCheckpoint();
                }
            }
        }

        public override void ApplyEntityStats()
        {
            base.ApplyEntityStats();
            animationScript.animator.runtimeAnimatorController = _stats.Controller;
        }

        public void UpdateSurvivorStates(SurvivorStates a_newState)
        {
            if (currentState == a_newState)
            {
                return;
            }

            currentState = a_newState;

            if (currentState == SurvivorStates.MoveToLevelFinish)
            {
                MovementSpeed = 100;
            } else if (currentState == SurvivorStates.Flee || currentState == SurvivorStates.MoveToHordeCheckpoint)
            {
                MovementSpeed = 150;
            }

            animationScript.UpdateAnimationState();
        }

        public void Interact(GameObject a_interactingObject)
        {
            if (currentState == SurvivorStates.Idle || currentState == SurvivorStates.Flee)
            {
                moveScript.SetDesiredPosition();
                UpdateSurvivorStates(SurvivorStates.MoveToHordeCheckpoint);
                //Debug.Log("It shall be done!");

                //Invoke(nameof(TeleportToCheckpoint), 2f);
            } else
            {
                //Debug.Log("But... I'm already here!");
            }
            //throw new System.NotImplementedException();
        }

        private void TeleportToCheckpoint()
        {
            //float randomOffset = moveScript._hordePosition.position.x + Random.Range(-0.5f, 0.5f);
            //Vector3 positionInCheckpoint = new Vector3(randomOffset, moveScript._hordePosition.position.y, moveScript._hordePosition.position.z);

            transform.position = moveScript._desiredPosition;
        }

        public override void OnDeath()
        {
            base.OnDeath();
            GameManager.instance.RemoveSurvivorFromHorde(gameObject);
        }

        public override void TakeDamage(int a_damageAmount)
        {
            base.TakeDamage(a_damageAmount);

            if (currentState == SurvivorStates.Idle)
            {
                UpdateSurvivorStates(SurvivorStates.Flee);
                moveScript._desiredPosition = new Vector3(Random.Range(moveScript._boundaryStart.position.x, moveScript._boundaryEnd.position.x), transform.position.y, transform.position.z);
            }

            ColourLogger.Log(this, "oh no!");
        }

        internal void RunToSafehouse()
        {
            if (currentState == SurvivorStates.WaitingAtCheckpoint)
            {
                moveScript._desiredPosition = moveScript._finishPosition.position;
                UpdateSurvivorStates(SurvivorStates.MoveToLevelFinish);
            }
        }

        public void SetCurrentStatSO(BaseEntityStats a_newStats)
        {
            _stats = a_newStats;
            ApplyEntityStats();
        }

        public BaseEntityStats GetEntityStats()
        {
            return _stats;
        }

        public int GetCurrentHealth()
        {
            return CurrentHealth;
        }

        public void SetInitialHealth(int a_startHealth)
        {
            CurrentHealth = a_startHealth;
        }

        public void PickUpWeapon(WeaponStats a_newWeapon)
        {
            return;
        }
    }
}
