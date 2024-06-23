using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombeezGameJam.Entities.Survivors
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class SurvivorAnimationScript : MonoBehaviour
    {
        [SerializeField] private Survivor _survivorScript;

        internal Animator animator;
        internal SpriteRenderer spriteRenderer;

        private string _currentAnimation;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateAnimationState()
        {
            string newAnimation = _survivorScript.currentState switch
            {
                SurvivorStates.MoveToHordeCheckpoint => $"Weapon0_Run",
                SurvivorStates.WaitingAtCheckpoint => $"Weapon0_Idle",
                _ => $"Weapon0_{_survivorScript.currentState}",
            };

            if (_currentAnimation != newAnimation)
            {
                _currentAnimation = newAnimation;
                animator.Play(_currentAnimation);
            }
        }
    }
}
