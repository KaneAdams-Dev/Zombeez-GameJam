using UnityEngine;

namespace ZombeezGameJam.Entities.Player
{
    [RequireComponent(typeof(Player), typeof(Rigidbody2D))]
    public class PlayerMovementScript : MonoBehaviour
    {
        [SerializeField] private Player _playerScript;

        [Space(5)]
        [SerializeField] private LayerMask _jumpableLayers;

        private readonly float _raySize = 0.32f;

        private Rigidbody2D _rbody;

        #region Unity Methods

        private void Awake()
        {
            _rbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (transform.position.y < -1f)
            {
                _playerScript.OnFall();
            }

            if (!CheckIfGrounded() && _playerScript.currentState == PlayerStates.Jump)
            {
                _playerScript.UpdatePlayerState(PlayerStates.Midair);
            }

            if (CheckIfGrounded() && _playerScript && _playerScript.currentState == PlayerStates.Midair)
            {
                _playerScript.UpdatePlayerState(PlayerStates.Land);
                Invoke(nameof(ExecuteLanding), 0.2f);
            }
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        #endregion Unity Methods

        #region Custom Methods

        private void MovePlayer()
        {
            transform.localScale = _playerScript.inputScript.isFacingLeft ? new Vector2(-1, transform.localScale.y) : new Vector2(1, transform.localScale.y);

            float xVelocity = _playerScript.inputScript.xMoveInput * _playerScript.MovementSpeed * Time.fixedDeltaTime;
            _rbody.linearVelocity = new Vector2(xVelocity, _rbody.linearVelocity.y);
        }

        public void ExecuteJump()
        {
            if (CheckIfGrounded())
            {
                _playerScript.UpdatePlayerState(PlayerStates.Jump);

                float yVelocity = _playerScript.jumpHeight * Time.fixedDeltaTime;
                _rbody.linearVelocity = new Vector2(_rbody.linearVelocity.x, yVelocity);
            }
        }

        public void ExecuteLanding()
        {
            _playerScript.UpdatePlayerState(_playerScript.inputScript.xMoveInput == 0 ? PlayerStates.Idle : PlayerStates.Run);
        }

        private bool CheckIfGrounded()
        {
            return Physics2D.Raycast(transform.position, Vector2.down, _raySize, _jumpableLayers);
        }

        #endregion Custom Methods
    }
}
