using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ZombeezGameJam
{
    public class PlayerInputs : MonoBehaviour
    {
        [SerializeField] private PlayerScript _playerScript;

        private PlayerActions _playerActions;
        internal Vector2 moveInput;
        internal bool isJumping;
        internal bool isFacingLeft;

        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            _playerActions = new PlayerActions();
        }

        // Start is called before the first frame update
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {
            isJumping = _playerActions.Player_Map.Jump.triggered;
            //isFacingLeft = _playerActions.Player_Map.Movement.
        }

        // This function is called every fixed framerate frame, if the MonoBehaviour is enabled
        //private void FixedUpdate()
        //{
        //    moveInput = _playerActions.Player_Map.Movement.ReadValue<Vector2>();
        //    moveInput.y = 0f;
        //
        //
        //}

        // This function is called when the object becomes enabled and active
        private void OnEnable()
        {
            _playerActions.Player_Map.Enable();

            _playerActions.Player_Map.Movement.performed += OnMovementPerformed;
            _playerActions.Player_Map.Movement.canceled += OnMovementCanceled;
        }

        // This function is called when the behaviour becomes disabled or inactive
        private void OnDisable()
        {
            _playerActions.Player_Map.Disable();

            _playerActions.Player_Map.Movement.performed -= OnMovementPerformed;
            _playerActions.Player_Map.Movement.canceled -= OnMovementCanceled;
        }

        private void OnMovementPerformed(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
            moveInput.y = 0;

            isFacingLeft = moveInput.x < 0;
        }

        private void OnMovementCanceled(InputAction.CallbackContext context)
        {
            moveInput = Vector2.zero;
        }
    }
}
