using UnityEngine;

namespace ZombeezGameJam.Entities.Enemies
{
    public class ZombieMovementScript : MonoBehaviour
    {
        [SerializeField] private Zombie _zombie;


        private Rigidbody2D _rbody;

        [SerializeField] private LayerMask _platformLayer;

        private readonly float _raySize = 0.32f;

        #region Unity Methods

        private void Awake()
        {
            _rbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            //if (!CheckIfGrounded())
            //{
            //    _rbody.constraints = RigidbodyConstraints2D.None;
            //}

            if ((!_zombie.isShuffler) && (_zombie.currentState == ZombieStates.Patrol || _zombie.currentState == ZombieStates.Chase) && _zombie.target != null)
            {
                MoveZombie();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(_zombie.patrolStartPosition.position, _zombie.patrolEndPosition.position);
        }

        #endregion Unity Methods

        #region Custom Methods

        public void StartRoam()
        {
            _zombie.target = (Random.value > 0.5) ? _zombie.patrolStartPosition : _zombie.patrolEndPosition;
        }

        internal void MoveZombie()
        {
            transform.localScale = new Vector2(_zombie.IsFacingTarget(), transform.localScale.y);

            float xVelocity = _zombie.MovementSpeed * Time.fixedDeltaTime * transform.localScale.x;
            _rbody.linearVelocity = new Vector2(xVelocity, _rbody.linearVelocity.y);

            if (_zombie.currentState == ZombieStates.Patrol)
            {
                if (Vector3.Distance(_zombie.target.position, transform.position) < 0.3f)
                {
                    if (_zombie.target == _zombie.patrolStartPosition)
                    {
                        _zombie.target = _zombie.patrolEndPosition;
                    } else if (_zombie.target == _zombie.patrolEndPosition)
                    {
                        _zombie.target = _zombie.patrolStartPosition;
                    }
                }
            }
        }

        internal void StopMovingZombie()
        {
            _rbody.linearVelocity = Vector2.zero;
        }

        private bool CheckIfGrounded()
        {
            return Physics2D.Raycast(transform.position, Vector2.down, _raySize, _platformLayer);
        }

        #endregion Custom Methods
    }
}
