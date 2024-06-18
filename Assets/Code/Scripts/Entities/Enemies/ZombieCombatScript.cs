using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombeezGameJam.Entities.Enemies
{
    public class ZombieCombatScript : MonoBehaviour
    {
        [SerializeField] private ZombiesScript _zombieScript;

        [SerializeField] private Transform _target;

        // Start is called before the first frame update
        void Start()
        {
            //_target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (_target == null)
            {
                return;
            }

            if (IsTargetInRange() && IsFacingTarget())
            {
                Attack();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(_zombieScript._attackRange, _zombieScript._attackRange, _zombieScript._attackRange));
        }

        bool IsTargetInRange()
        {
            return Vector3.Distance(transform.position, _target.position) <= _zombieScript._attackRange;
        }

        bool IsFacingTarget()
        {
            float dot = Vector3.Dot(transform.right, (_target.position - transform.position).normalized);
            return dot > 0.75f;
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
