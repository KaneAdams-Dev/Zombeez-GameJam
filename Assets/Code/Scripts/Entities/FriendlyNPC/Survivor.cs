using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombeezGameJam.Interfaces;

namespace ZombeezGameJam.Entities.Survivors
{
    public enum SurvivorStates
    {
        Idle,
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

        public void UpdateSurvivorStates(SurvivorStates a_newState)
        {
            if (currentState == a_newState)
            {
                return;
            }

            currentState = a_newState;

            animationScript.UpdateAnimationState();
        }

        public void Interact()
        {
            if (currentState != SurvivorStates.WaitingAtCheckpoint)
            {
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
            float randomOffset = moveScript._hordePosition.position.x + Random.Range(-0.5f, 0.5f);
            Vector3 positionInCheckpoint = new Vector3(randomOffset, moveScript._hordePosition.position.y, moveScript._hordePosition.position.z);

            transform.position = positionInCheckpoint;
        }
    }
}
