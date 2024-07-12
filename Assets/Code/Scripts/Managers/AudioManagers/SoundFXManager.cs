using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombeezGameJam
{
    public class SoundFXManager : MonoBehaviour
    {
        public static SoundFXManager instance;

        [SerializeField] private AudioSource soundFXPrefab;

        #region Unity Methods

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
        }

        #endregion Unity Methods

        #region Custom Methods

        public void PlaySoundClip(AudioClip a_audioClip, Transform a_parent, float a_volume, int a_priority = 128)
        {
            AudioSource audioSource = Instantiate(soundFXPrefab, a_parent.position, Quaternion.identity);

            audioSource.clip = a_audioClip;
            audioSource.volume = a_volume;
            float clipLength = audioSource.clip.length;
            audioSource.priority = a_priority;

            audioSource.Play();

            Destroy(audioSource.gameObject, clipLength);
        }

        public void PlayRandomSoundClip(AudioClip[] a_audioClips, Transform a_parent, float a_volume, int a_priority = 128)
        {
            int randomIndex = Random.Range(0, a_audioClips.Length);
            AudioClip chosenClip = a_audioClips[randomIndex];

            PlaySoundClip(chosenClip, a_parent, a_volume, a_priority);
        }

        #endregion Custom Methods
    }
}
