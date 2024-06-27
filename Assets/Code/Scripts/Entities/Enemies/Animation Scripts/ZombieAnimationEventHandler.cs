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

        public void PlayMovementSound()
        {
            if (GetComponent<SpriteRenderer>().isVisible)
            {
                SoundFXManager.instance.PlayRandomSoundClip(_zombie.movementAudio, _zombie.transform, 1f);
            }
        }

        public void PlayAttackSound()
        {
            if (GetComponent<SpriteRenderer>().isVisible)
            {
                SoundFXManager.instance.PlaySoundClip(_zombie.attackAudio, _zombie.transform, 1f);
            }
        }

        #endregion Custom Methods
    }
}
