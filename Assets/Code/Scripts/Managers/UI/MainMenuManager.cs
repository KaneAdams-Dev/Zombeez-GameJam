using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZombeezGameJam.Managers.UI
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject _playButton;

        // Start is called before the first frame update
        void Start()
        {
            _playButton.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void EnableUI()
        {
            _playButton.SetActive(true);
        }

        public void OnPlayButtonPressed()
        {
            SceneManager.LoadScene(Scenes.Level1.GetHashCode());
        }

        public void OnExitButtonPressed()
        {
            Application.Quit();
        }
    }
}
