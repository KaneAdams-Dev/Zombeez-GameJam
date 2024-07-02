using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using ZombeezGameJam.Managers;

namespace ZombeezGameJam.Entities.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [Header("Script References")]
        [SerializeField] private Player _playerScript;

        internal bool isFacingLeft;
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
            GameManager.OnGameOver += DisableInputs;
            EnableInputs();
        }

        private void OnDisable()
        {
            GameManager.OnGameOver -= DisableInputs;
            DisableInputs();
        }

        #endregion Unity Methods

        #region Custom Methods

        private void OnMovementPerformed(InputAction.CallbackContext context)
        {
            xMoveInput = context.ReadValue<float>();

            isFacingLeft = xMoveInput < 0;
            _playerScript.UpdatePlayerState(PlayerStates.Run);
        }

        private void OnMovementCanceled(InputAction.CallbackContext context)
        {
            xMoveInput = 0;
            _playerScript.UpdatePlayerState(PlayerStates.Idle);
        }

        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            _playerScript.movementScript.ExecuteJump();
        }

        private void OnFirePerformed(InputAction.CallbackContext context)
        {
            if (CanPlayerFire())
            {
                if (context.interaction is HoldInteraction)
                {
                    //Debug.Log("Spray and Pray");
                    _playerScript.weaponHandler.StartFiring();
                } else if (context.interaction is TapInteraction) { }
                {
                    //Debug.Log("Tappy-Tap!");
                    _playerScript.weaponHandler.Fire();
                }

            }
        }

        private void OnFireEnded(InputAction.CallbackContext context)
        {
            _playerScript.weaponHandler.StopFiring();
        }

        private void OnInteractPerformed(InputAction.CallbackContext context)
        {
            //Debug.Log("Interact button pressed");
            _playerScript.interactor.CheckForInteractions();
        }

        internal bool CanPlayerFire()
        {
            bool isUnarmed = _playerScript.currentWeapon == WeaponTypes.Unarmed;
            PlayerStates[] invalidStates = new PlayerStates[] { PlayerStates.Jump, PlayerStates.Midair, PlayerStates.Land };
            bool isInValidState = invalidStates.Contains(_playerScript.currentState);

            return !(isUnarmed || isInValidState);
        }

        private void EnableInputs()
        {
            _playerActionsMap.Enable();

            _playerActionsMap.Movement.performed += OnMovementPerformed;
            _playerActionsMap.Movement.canceled += OnMovementCanceled;

            _playerActionsMap.Jump.performed += OnJumpPerformed;
            _playerActionsMap.Fire.performed += OnFirePerformed;
            _playerActionsMap.Fire.canceled += OnFireEnded;
            _playerActionsMap.Interact.performed += OnInteractPerformed;
        }

        private void DisableInputs()
        {
            _playerActionsMap.Disable();

            _playerActionsMap.Movement.performed -= OnMovementPerformed;
            _playerActionsMap.Movement.canceled -= OnMovementCanceled;

            _playerActionsMap.Jump.performed -= OnJumpPerformed;
            _playerActionsMap.Fire.performed -= OnFirePerformed;
            _playerActionsMap.Fire.canceled -= OnFireEnded;
            _playerActionsMap.Interact.performed -= OnInteractPerformed;
        }

        #endregion Custom Methods
    }
}
