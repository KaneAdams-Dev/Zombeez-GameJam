using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace ZombeezGameJam.Entities.Survivors
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SurvivorMovementScript : MonoBehaviour
    {
        [SerializeField] private Survivor _survivorScript;

        private Rigidbody2D _rbody;

        [SerializeField] internal Transform _hordePosition;

        private void Awake()
        {
            _rbody = GetComponent<Rigidbody2D>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            if (CheckIfMoving())
            {
                MoveSurvivor();
            }

        }

        internal void MoveSurvivor()
        {
            float xVelocity = _survivorScript.movementSpeed * Time.fixedDeltaTime * transform.localScale.x;
            _rbody.velocity = new Vector2(xVelocity, _rbody.velocity.y);

            if (_survivorScript.currentState == SurvivorStates.MoveToHordeCheckpoint)
            {
                if (Vector3.Distance(_hordePosition.position, transform.position) < 0.5f)
                {
                    _rbody.velocity = Vector2.zero;
                    transform.localScale = Vector3.one;
                    _survivorScript.UpdateSurvivorStates(SurvivorStates.WaitingAtCheckpoint);
                    return;
                }
                
                transform.localScale = new Vector3(IsFacingTarget(_hordePosition), transform.localScale.y, transform.localScale.z);
            }
        }

        internal float IsFacingTarget(Transform a_target)
        {
            //float dot = Vector3.Dot(transform.right, (_target.position - transform.position).normalized);
            float direction = a_target.position.x - transform.position.x;
            return direction / Mathf.Abs(direction);
        }

        private bool CheckIfMoving()
        {
            return _survivorScript.currentState == SurvivorStates.MoveToHordeCheckpoint || _survivorScript.currentState == SurvivorStates.MoveToLevelFinish;
        }
    }
}
