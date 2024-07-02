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
            if (_zombieScript.currentState == ZombieStates.Spawn) { 
                return;
            }
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
            
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, _zombieScript.secondaryAttackRange);
            
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
            if (_zombieScript.currentState == ZombieStates.Attack || _zombieScript.currentState == ZombieStates.Attack2 || _zombieScript.currentState == ZombieStates.Spawn || _zombieScript.target == null)
            {
                return;
            }
            transform.localScale = new Vector2(_zombieScript.IsFacingTarget(), transform.localScale.y);

            _zombieScript.UpdateZombieState(ZombieStates.Attack);
            Invoke(nameof(ResetAttack), _zombieScript.animationScript._animator.GetCurrentAnimatorClipInfo(0).Length);
        }

        internal void SecondaryAttack()
        {
            if (_zombieScript.currentState == ZombieStates.Attack || _zombieScript.currentState == ZombieStates.Attack2 || _zombieScript.target == null)
            {
                return;
            }

            _zombieScript.isSecondaryAttackReady = false;

            transform.localScale = new Vector2(_zombieScript.IsFacingTarget(), transform.localScale.y);

            _zombieScript.UpdateZombieState(ZombieStates.Attack2);
            Invoke(nameof(ResetAttack), _zombieScript.animationScript._animator.GetCurrentAnimatorClipInfo(0).Length);
            Invoke(nameof(ResetSecondaryAttack), _zombieScript.animationScript._animator.GetCurrentAnimatorClipInfo(0).Length + 3f);
        }

        private void ResetAttack()
        {
            _zombieScript.UpdateZombieState(ZombieStates.Idle);
            _zombieScript.hitboxScript.isPlayerHit = false;
        }

        private void ResetSecondaryAttack()
        {
            _zombieScript.isSecondaryAttackReady = true;
        }

        #endregion Custom Methods
    }
}
