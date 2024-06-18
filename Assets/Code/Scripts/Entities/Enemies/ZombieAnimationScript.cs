using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombeezGameJam.Entities.Enemies
{
    [RequireComponent(typeof(Animator))]
    public class ZombieAnimationScript : MonoBehaviour
    {
        [SerializeField] private ZombiesScript _zombieScript;

        internal Animator _animator;

        private string _currentAnimation;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
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
            string newAnimation = _zombieScript.currentState switch
            {
                ZombieStates.Patrol => "Zombie_Walk",
                ZombieStates.Chase => "Zombie_Walk",
                _ => $"Zombie_{_zombieScript.currentState}",
            };

            if (_currentAnimation != newAnimation)
            {
                _currentAnimation = newAnimation;
                _animator.Play(_currentAnimation);
            }
        }
    }
}
