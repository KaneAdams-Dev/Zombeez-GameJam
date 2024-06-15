using UnityEngine;

namespace ZombeezGameJam.Camera
{
    public class CameraFollowScript : MonoBehaviour
    {
        [SerializeField] private Transform objectToFollow;

        #region Unity Methods

        private void LateUpdate()
        {
            if (objectToFollow == null)
            {
                return;
            }

            transform.position = new Vector3(objectToFollow.position.x, Mathf.Clamp(objectToFollow.position.y + 0.25f, 0f, 100f), transform.position.z);
        }

        #endregion Unity Methods
    }
}
