using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombeezGameJam.Entities.Enemies
{
    public class ZombieCombatScript : MonoBehaviour
    {
        [SerializeField] private ZombiesScript _zombieScript;


        // Start is called before the first frame update
        void Start()
        {
            //_target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Update is called once per frame
        void Update()
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
            } else if (_zombieScript.currentState != ZombieStates.Attack) 
            {
                _zombieScript.UpdateZombieState(ZombieStates.Patrol);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _zombieScript._attackRange);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _zombieScript._chaseRange);
        }

        bool IsTargetInRange()
        {
            return Vector3.Distance(transform.position, _zombieScript._target.position) <= _zombieScript._attackRange;
        }

        

        void Attack()
        {
            if (_zombieScript.currentState == ZombieStates.Attack)
            {
                return;
            }
            _zombieScript.UpdateZombieState(ZombieStates.Attack);
            Invoke(nameof(ResetAttack), _zombieScript.animationScript._animator.GetCurrentAnimatorClipInfo(0).Length);
        }

        void ResetAttack()
        {
            _zombieScript.UpdateZombieState(ZombieStates.Idle);
            _zombieScript.hitboxScript.isPlayerHit = false;
        }
    }
}
