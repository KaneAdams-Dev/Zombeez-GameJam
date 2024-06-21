using UnityEngine;

namespace ZombeezGameJam.Entities.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationScript : MonoBehaviour
    {
        [SerializeField] private Player _playerScript;

        internal Animator _animator;

        private string _currentAnimation;

        #region Unity Methods

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        #endregion Unity Methods

        #region Custom Methods

        public void UpdateAnimationState()
        {
            string newAnimation = _playerScript.currentState switch
            {
                PlayerStates.Jump => "Jump",
                PlayerStates.Midair => "Midair",
                PlayerStates.Land => "Land",
                _ => $"Weapon{(int)_playerScript.currentWeapon}_{_playerScript.currentState}",
            };

            if (_currentAnimation != newAnimation)
            {
                _currentAnimation = newAnimation;
                _animator.Play(_currentAnimation);
            }
        }

        #endregion Custom Methods
    }
}
