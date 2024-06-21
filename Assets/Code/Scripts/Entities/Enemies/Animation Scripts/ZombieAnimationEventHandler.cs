using UnityEngine;

namespace ZombeezGameJam.Entities.Enemies
{
    public class ZombieAnimationEventHandler : MonoBehaviour
    {
        [SerializeField] private Zombie _zombie;

        #region Custom Methods

        public void StartShuffle()
        {
            _zombie.movementScript.MoveZombie();
        }

        public void EndShuffle()
        {
            _zombie.movementScript.StopMovingZombie();
        }

        #endregion Custom Methods
    }
}
