using UnityEngine;

namespace ZombeezGameJam.Entities.Enemies
{
    [RequireComponent(typeof(Animator))]
    public class ZombieAnimationScript : MonoBehaviour
    {
        [SerializeField] private Zombie _zombieScript;

        internal Animator _animator;

        private string _currentAnimation;

        #region Unity Methods

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        // Start is called before the first frame update
        private void Start()
        {
            if (_zombieScript.currentState == ZombieStates.Patrol)
            {
                _animator.Play(_currentAnimation, 0, Random.value);
            }
        }

        #endregion Unity Methods

        #region Custom Methods

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

            if (_zombieScript.currentState == ZombieStates.Chase)
            {
                _animator.speed = 1.5f;
            } else
            {
                _animator.speed = 1f;
            }
        }

        #endregion Custom Methods
    }
}
