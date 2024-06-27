using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombeezGameJam
{
    public class AudioRolloff : MonoBehaviour
    {
        private AudioListener _audioListener;

        private void Awake()
        {
            _audioListener = Camera.main.GetComponent<AudioListener>();
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
