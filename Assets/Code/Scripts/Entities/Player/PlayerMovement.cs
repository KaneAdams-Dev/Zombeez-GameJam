using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombeezGameJam
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerScript _playerScript;

        private Rigidbody2D _rbody;

        [Header("Movement Values")]
        [SerializeField] private float _movementSpeed = 10f;
        [SerializeField] private float _jumpHeight = 10f;

        private bool _inAir;

        private SpriteRenderer _renderer;

        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            _rbody = GetComponent<Rigidbody2D>();
            _renderer = GetComponent<SpriteRenderer>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (_playerScript.inputScript.isJumping && !_inAir)
            {
                _rbody.velocity = new Vector2(_rbody.velocity.x, _jumpHeight);

            }
        }

        // This function is called every fixed framerate frame, if the MonoBehaviour is enabled
        private void FixedUpdate()
        {
            _rbody.velocity = new Vector2(_playerScript.inputScript.moveInput.x * _movementSpeed, _rbody.velocity.y);
            _renderer.flipX = _playerScript.inputScript.isFacingLeft;
        }

        // OnCollisionEnter2D is called when this collider2D/rigidbody2D has begun touching another rigidbody2D/collider2D (2D physics only)
        private void OnCollisionEnter2D(Collision2D collision)
        {
            _inAir = false;
        }

        // OnCollisionExit2D is called when this collider2D/rigidbody2D has stopped touching another rigidbody2D/collider2D (2D physics only)
        private void OnCollisionExit2D(Collision2D collision)
        {
            _inAir = true;
        }
    }
}
