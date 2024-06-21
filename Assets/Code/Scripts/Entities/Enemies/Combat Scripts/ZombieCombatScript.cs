using UnityEngine;

namespace ZombeezGameJam.Entities.Enemies
{
    public class ZombieCombatScript : MonoBehaviour
    {
        [SerializeField] private Zombie _zombieScript;

        #region Unity Methods

        // Update is called once per frame
        private void Update()
        {
            if (_zombieScript._target == null)
            {
                _zombieScript.UpdateZombieState(ZombieStates.Patrol);
                return;
            }
            //Debug.Log(_zombieScript.IsFacingTarget());
            if (IsTargetInRange() /*&& _zombieScript.IsFacingTarget()*/)
            {
                Attack();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _zombieScript._attackRange);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _zombieScript._chaseRange);

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, _zombieScript._chaseRange + _zombieScript._chaseBuffer);
        }

        #endregion Unity Methods

        #region Custom Methods

        private bool IsTargetInRange()
        {
            return Vector3.Distance(transform.position, _zombieScript._target.position) <= _zombieScript._attackRange;
        }

        internal void Attack()
        {
            if (_zombieScript.currentState == ZombieStates.Attack)
            {
                return;
            }
            _zombieScript.UpdateZombieState(ZombieStates.Attack);
            Invoke(nameof(ResetAttack), _zombieScript.animationScript._animator.GetCurrentAnimatorClipInfo(0).Length);
        }

        private void ResetAttack()
        {
            _zombieScript.UpdateZombieState(ZombieStates.Idle);
            _zombieScript.hitboxScript.isPlayerHit = false;
        }

        #endregion Custom Methods
    }
}
