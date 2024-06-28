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
            if (_zombieScript.target == null)
            {
                _zombieScript.UpdateZombieState(ZombieStates.Patrol);
                return;
            }
            //Debug.Log(_zombieScript.IsFacingTarget());
            //if (IsTargetInRange() /*&& _zombieScript.IsFacingTarget()*/)
            //{
            //    Attack();
            //}
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _zombieScript.attackRange);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _zombieScript.chaseRange);

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, _zombieScript.chaseRange + _zombieScript.chaseBuffer);
        }

        #endregion Unity Methods

        #region Custom Methods

        private bool IsTargetInRange()
        {
            return Vector3.Distance(transform.position, _zombieScript.target.position) <= _zombieScript.attackRange;
        }

        internal void Attack()
        {
            if (_zombieScript.currentState == ZombieStates.Attack || _zombieScript.target == null)
            {
                return;
            }
            transform.localScale = new Vector2(_zombieScript.IsFacingTarget(), transform.localScale.y);

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
