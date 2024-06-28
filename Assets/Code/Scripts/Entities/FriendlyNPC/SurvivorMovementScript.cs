using UnityEngine;

namespace ZombeezGameJam.Entities.Survivors
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SurvivorMovementScript : MonoBehaviour
    {
        [SerializeField] private Survivor _survivorScript;

        private Rigidbody2D _rbody;

        [SerializeField] internal Transform _hordePosition;
        [SerializeField] internal Transform _finishPosition;
        internal Vector3 _desiredPosition;

        [SerializeField] internal Transform _boundaryStart;
        [SerializeField] internal Transform _boundaryEnd;

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
            if (Input.GetKeyDown(KeyCode.H) && _survivorScript.currentState == SurvivorStates.WaitingAtCheckpoint)
            {
                _desiredPosition = _finishPosition.position;
                _survivorScript.UpdateSurvivorStates(SurvivorStates.MoveToLevelFinish);
            }
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
            float xVelocity = _survivorScript.MovementSpeed * Time.fixedDeltaTime * transform.localScale.x;
            _rbody.velocity = new Vector2(xVelocity, _rbody.velocity.y);

            if (_survivorScript.currentState == SurvivorStates.MoveToHordeCheckpoint)
            {
                if (Vector3.Distance(_desiredPosition, transform.position) < 0.05f)
                {
                    _rbody.velocity = Vector2.zero;
                    transform.localScale = Vector3.one;

                    GameManager.instance.AddSurvivorToHorde(gameObject);

                    _survivorScript.UpdateSurvivorStates(SurvivorStates.WaitingAtCheckpoint);
                    if (GameManager.instance.IsFinalWave)
                    {
                        _survivorScript.RunToSafehouse();
                    } 

                    return;
                }

                transform.localScale = new Vector3(IsFacingTarget(_desiredPosition), transform.localScale.y, transform.localScale.z);

            } else if (_survivorScript.currentState == SurvivorStates.MoveToLevelFinish)
            {
                if (Vector3.Distance(_desiredPosition, transform.position) < 0.05f)
                {
                    GameManager.instance.RemoveSurvivorFromHorde(gameObject);
                    Destroy(gameObject);
                }

                transform.localScale = new Vector3(IsFacingTarget(_desiredPosition), transform.localScale.y, transform.localScale.z);
            } else if (_survivorScript.currentState == SurvivorStates.Flee)
            {
                if (Vector3.Distance(_desiredPosition, transform.position) < 0.05f)
                {
                    _rbody.velocity = Vector2.zero;
                    _survivorScript.UpdateSurvivorStates(SurvivorStates.Idle);

                    return;
                }

                transform.localScale = new Vector3(IsFacingTarget(_desiredPosition), transform.localScale.y, transform.localScale.z);
            }
        }

        internal void SetDesiredPosition()
        {
            _desiredPosition = new Vector3(_hordePosition.position.x + Random.Range(-0.5f, 0.5f), transform.position.y, transform.position.z);
        }

        internal float IsFacingTarget(Vector3 a_targetToFace)
        {
            float direction = a_targetToFace.x - transform.position.x;
            return direction / Mathf.Abs(direction);
        }

        private bool CheckIfMoving()
        {
            return _survivorScript.currentState == SurvivorStates.MoveToHordeCheckpoint || _survivorScript.currentState == SurvivorStates.MoveToLevelFinish || _survivorScript.currentState == SurvivorStates.Flee;
        }
    }
}
