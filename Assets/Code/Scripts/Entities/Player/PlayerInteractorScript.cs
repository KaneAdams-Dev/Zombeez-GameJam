using UnityEngine;
using ZombeezGameJam.Interfaces;

namespace ZombeezGameJam.Entities.Player
{
    public class PlayerInteractorScript : MonoBehaviour
    {
        [SerializeField] private Player _playerScript;

        [SerializeField] private float _interactRange;

        [SerializeField] private LayerMask _interactLayers;

        #region Unity Methods

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(from: transform.position, direction: _interactRange * transform.localScale.x * transform.right);
        }

        #endregion Unity Methods

        #region Custom Methods

        public void CheckForInteractions()
        {
            Debug.Log("Looking for buttons to press!");

            RaycastHit2D hit = Physics2D.Raycast(origin: transform.position, direction: transform.right * transform.localScale.x, distance: _interactRange, _interactLayers.value);

            if (hit.collider != null)
            {
                Debug.Log(hit.transform.name);

                if (hit.collider.gameObject.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact();
                }
            }
        }

        #endregion Custom Methods
    }
}
