using UnityEngine;

namespace ZombeezGameJam.Entities.Player
{
    [RequireComponent(typeof(PlayerScript), typeof(SpriteRenderer), typeof(Animator))]
    public class PlayerAnimationScript : MonoBehaviour
    {
        [SerializeField] private PlayerScript _playerScript;

        private Animator _animator;
        private SpriteRenderer _renderer;

        private string _currentAnimation;

        #region Unity Methods

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _renderer = GetComponent<SpriteRenderer>();
        }

        #endregion Unity Methods

        #region Custom Methods

        public void UpdateAnimationState()
        {
            _renderer.flipX = _playerScript.inputScript.isFacingLeft;

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
