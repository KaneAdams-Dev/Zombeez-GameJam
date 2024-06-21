using UnityEngine;

namespace ZombeezGameJam.Entities.Enemies
{
    public class ZombieHitbox : MonoBehaviour
    {
        [SerializeField] private Zombie _zombieScript;

        internal bool isPlayerHit;

        #region Unity Methods

        // Start is called before the first frame update
        private void Start()
        {
            isPlayerHit = false;
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

        #endregion Unity Methods
    }
}
