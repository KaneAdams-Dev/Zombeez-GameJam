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

        public void PlaySoundClip(AudioClip a_audioClip, Transform a_parent, float a_volume = 1f, float a_pitch = 1f, float a_minPitch = 0.8f, float a_maxPitch = 1.2f, int a_priority = 128)
        {
            AudioSource audioSource = Instantiate(soundFXPrefab, a_parent.position, Quaternion.identity);

            audioSource.clip = a_audioClip;
            audioSource.volume = a_volume;
            //int[] pentatonicSemitones = new[] { 0, 2, 4, 7, 9 };
            //int x = pentatonicSemitones[Random.Range(0, pentatonicSemitones.Length)];
            //for (int i = 0; i < x; i++)
            //{
            //    a_pitch *= 1.059463f;
            //}

            audioSource.pitch = a_pitch * Random.Range(a_minPitch, a_maxPitch);
            float clipLength = audioSource.clip.length;
            audioSource.priority = a_priority;

            audioSource.Play();

            Destroy(audioSource.gameObject, clipLength);
        }

        public void PlaySoundClip(AudioClip[] a_audioClips, Transform a_parent, float a_volume = 1f, float a_pitch = 1f, float a_minPitch = 0.8f, float a_maxPitch = 1.2f, int a_priority = 128)
        {
            int randomIndex = Random.Range(0, a_audioClips.Length);
            AudioClip chosenClip = a_audioClips[randomIndex];

            PlaySoundClip(chosenClip, a_parent, a_volume, a_pitch, a_minPitch, a_maxPitch, a_priority);
        }

        #endregion Custom Methods
    }
}
