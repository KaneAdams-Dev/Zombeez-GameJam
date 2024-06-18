using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombeezGameJam.Entities.Enemies
{
    public class ZombieHitbox : MonoBehaviour
    {
        [SerializeField] private ZombiesScript _zombieScript;

        internal bool isPlayerHit;

        // Start is called before the first frame update
        void Start()
        {
            isPlayerHit = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isPlayerHit)
            {
                return;
            }

            if (collision.gameObject.TryGetComponent(out BaseEntity player) && collision.gameObject.CompareTag("Player"))
            {
                isPlayerHit = true;
                player.TakeDamage(_zombieScript.AttackStength);
            }
        }
    }
}
