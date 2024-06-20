using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombeezGameJam.Entities.Enemies
{
    public class ZombieMovementScript : MonoBehaviour
    {
        [SerializeField] private ZombiesScript _zombieScipt;

        //private bool isFacingRight;

        [SerializeField] private Transform patrolStartPosition;
        [SerializeField] private Transform patrolEndPosition;

        private Rigidbody2D _rbody;

        // Start is called before the first frame update
        void Start()
        {
            _rbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            //isFacingRight = _zombieScipt.IsFacingTarget();
        }

        private void FixedUpdate()
        {
            if (_zombieScipt.currentState != ZombieStates.Attack)
            {
                MoveZombie();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(patrolStartPosition.position, patrolEndPosition.position);
        }

        public void StartRoam()
        {
            _zombieScipt._target = (Random.value > 0.5) ? patrolStartPosition : patrolEndPosition;
        }

        private void MoveZombie()
        {
            transform.localScale = new Vector2(_zombieScipt.IsFacingTarget(), transform.localScale.y);//isFacingRight ? new Vector2(1, transform.localScale.y) : new Vector2(-1, transform.localScale.y);

            float xVelocity = _zombieScipt.movementSpeed * Time.fixedDeltaTime * transform.localScale.x;
            _rbody.velocity = new Vector2(xVelocity, _rbody.velocity.y);

            if (_zombieScipt.currentState == ZombieStates.Patrol)
            {
                Debug.Log(Vector3.Distance(_zombieScipt._target.position, transform.position));
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

            //float xVelocity = _playerScript.inputScript.xMoveInput * _playerScript.movementSpeed * Time.fixedDeltaTime;
            //_rbody.velocity = new Vector2(xVelocity, _rbody.velocity.y);
        }
    }
}
