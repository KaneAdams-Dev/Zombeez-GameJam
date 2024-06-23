using UnityEngine;

namespace ZombeezGameJam.Entities.Enemies
{
    public class ZombieMovementScript : MonoBehaviour
    {
        [SerializeField] private Zombie _zombieScipt;

        [SerializeField] private Transform patrolStartPosition;
        [SerializeField] private Transform patrolEndPosition;

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
            if (!CheckIfGrounded())
            {
                _rbody.constraints = RigidbodyConstraints2D.None;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(patrolStartPosition.position, patrolEndPosition.position);
        }

        #endregion Unity Methods

        #region Custom Methods

        public void StartRoam()
        {
            _zombieScipt._target = (Random.value > 0.5) ? patrolStartPosition : patrolEndPosition;
        }

        internal void MoveZombie()
        {
            transform.localScale = new Vector2(_zombieScipt.IsFacingTarget(), transform.localScale.y);

            float xVelocity = _zombieScipt.movementSpeed * Time.fixedDeltaTime * transform.localScale.x;
            _rbody.velocity = new Vector2(xVelocity, _rbody.velocity.y);

            if (_zombieScipt.currentState == ZombieStates.Patrol)
            {
                if (Vector3.Distance(_zombieScipt._target.position, transform.position) < 0.3f)
                {
                    if (_zombieScipt._target == patrolStartPosition)
                    {
                        _zombieScipt._target = patrolEndPosition;
                    }else if (_zombieScipt._target == patrolEndPosition)
                    {
                        _zombieScipt._target = patrolStartPosition;
                    }
                }
            }
        }

        internal void StopMovingZombie()
        {
            _rbody.velocity = Vector2.zero;
        }

        private bool CheckIfGrounded()
        {
            return Physics2D.Raycast(transform.position, Vector2.down, _raySize, _platformLayer);
        }

        #endregion Custom Methods
    }
}
