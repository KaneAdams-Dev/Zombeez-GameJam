using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ZombeezGameJam.Entities.Player
{
    public class PlayerInputScript : MonoBehaviour
    {
        [Header("Script References")]
        [SerializeField] private PlayerScript _playerScript;

        internal bool isFacingLeft;
        internal bool isFiring;
        internal float xMoveInput;

        private PlayerActions _playerActions;
        private PlayerActions.Player_MapActions _playerActionsMap;

        #region Unity Methods

        private void Awake()
        {
            _playerActions = new PlayerActions();
            _playerActionsMap = _playerActions.Player_Map;
        }

        private void OnEnable()
        {
            _playerActionsMap.Enable();

            _playerActionsMap.Movement.performed += OnMovementPerformed;
            _playerActionsMap.Movement.canceled += OnMovementCanceled;

            _playerActionsMap.Jump.performed += OnJumpPerformed;
            _playerActionsMap.Fire.performed += OnFirePerformed;
        }

        private void OnDisable()
        {
            _playerActionsMap.Disable();

            _playerActionsMap.Movement.performed -= OnMovementPerformed;
            _playerActionsMap.Movement.canceled -= OnMovementCanceled;

            _playerActionsMap.Jump.performed -= OnJumpPerformed;
            _playerActionsMap.Fire.performed -= OnFirePerformed;
        }

        #endregion Unity Methods

        #region Custom Methods

        private void OnMovementPerformed(InputAction.CallbackContext context)
        {
            xMoveInput = context.ReadValue<float>();
            //moveInput.y = 0;

            isFacingLeft = xMoveInput < 0;//moveInput.x < 0;
            _playerScript.UpdatePlayerState(PlayerStates.Run);
        }

        private void OnMovementCanceled(InputAction.CallbackContext context)
        {
            xMoveInput = 0;//moveInput = Vector2.zero;
            _playerScript.UpdatePlayerState(PlayerStates.Idle);
        }

        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            _playerScript.movementScript.ExecuteJump();
        }

        private void OnFirePerformed(InputAction.CallbackContext context)
        {
            _playerScript.weaponScript.FireWeapon();
        }

        #endregion Custom Methods
    }
}
