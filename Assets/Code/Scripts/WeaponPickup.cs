using UnityEngine;
using ZombeezGameJam.Entities.Player;

namespace ZombeezGameJam
{
    public class WeaponPickup : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerScript playerScript))
            {
                playerScript.currentWeapon = PlayerWeapons.Revolver1;
                Destroy(gameObject);
            }
        }
    }
}
