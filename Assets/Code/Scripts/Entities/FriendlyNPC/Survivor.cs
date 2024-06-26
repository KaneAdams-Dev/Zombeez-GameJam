using UnityEngine;
using ZombeezGameJam.Interfaces;

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

    public class Survivor : BaseEntity, IInteractable
    {
        [SerializeField] internal SurvivorAnimationScript animationScript;
        [SerializeField] internal SurvivorMovementScript moveScript;

        [Header("Movement Values")]
        [SerializeField] internal float movementSpeed = 100f;

        internal SurvivorStates currentState;

        [SerializeField] internal BaseEntityStats stats;

        public override void Start()
        {
            base.Start();

            //UpdateSurvivorStates(SurvivorStates.Wander);
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
            animationScript.animator.runtimeAnimatorController = stats.Controller;
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
                movementSpeed = 100;
            } else if (currentState == SurvivorStates.Flee || currentState == SurvivorStates.MoveToHordeCheckpoint)
            {
                movementSpeed = 150;
            }

            animationScript.UpdateAnimationState();
        }

        public void Interact()
        {
            if (currentState != SurvivorStates.WaitingAtCheckpoint)
            {
                moveScript.SetDesiredPosition();
                UpdateSurvivorStates(SurvivorStates.MoveToHordeCheckpoint);
                Debug.Log("It shall be done!");

                //Invoke(nameof(TeleportToCheckpoint), 2f);
            } else
            {
                Debug.Log("But... I'm already here!");
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
                Debug.Log("RUN! " + moveScript._desiredPosition);
            }
        }
    }
}
