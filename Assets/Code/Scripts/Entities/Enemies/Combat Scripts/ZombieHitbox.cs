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

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (isPlayerHit)
            {
                return;
            }

            if (collision.gameObject.TryGetComponent(out BaseEntity entity) && /*collision.gameObject.CompareTag("Player")*/collision.transform == _zombieScript.target)
            {
                isPlayerHit = true;
                entity.TakeDamage(_zombieScript.attackStrength);
            }
        }

        #endregion Unity Methods
    }
}
