using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ZombeezGameJam.Entities.Player;

namespace ZombeezGameJam.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        [SerializeField] private TextMeshProUGUI _countdownTimerText;
        [SerializeField] private TextMeshProUGUI _zombieCounterText;
        [SerializeField] private TextMeshProUGUI _survivorCounterText;

        [SerializeField] private Image _healthbarFill;

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

        // Start is called before the first frame update
        private void Start()
        {
            DisableFinalWaveUI();
        }

        private void OnEnable()
        {
            GameManager.OnZombieCountUpdate += UpdateZombieCounterDisplay;
            GameManager.OnSurvivorCountUpdate += UpdateSurvivorCounterDisplay;
            GameManager.OnFinalWaveBegins += EnableFinalWaveUI;
            GameManager.OnCountdownUpdate += UpdateCountdownDisplay;

            Player.OnHealthChange += UpdateHealthbar;
        }

        private void OnDisable()
        {
            GameManager.OnZombieCountUpdate -= UpdateZombieCounterDisplay;
            GameManager.OnSurvivorCountUpdate -= UpdateSurvivorCounterDisplay;
            GameManager.OnFinalWaveBegins -= EnableFinalWaveUI;
            GameManager.OnCountdownUpdate -= UpdateCountdownDisplay;

            Player.OnHealthChange -= UpdateHealthbar;
        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion Unity Methods

        #region Custom Methods

        private void EnableFinalWaveUI()
        {
            _countdownTimerText.gameObject.SetActive(true);
            _zombieCounterText.gameObject.SetActive(false);
        }

        private void DisableFinalWaveUI()
        {
            _countdownTimerText.gameObject.SetActive(false);
            _zombieCounterText.gameObject.SetActive(true);
        }

        private void UpdateZombieCounterDisplay(int a_zombieCount)
        {
            _zombieCounterText.text = $"{a_zombieCount} Zombies Remaining";
        }
        
        private void UpdateSurvivorCounterDisplay(int a_survivorCount)
        {
            _survivorCounterText.text = $"{a_survivorCount} Survivors Recruited";
        }

        private void UpdateCountdownDisplay(int a_countdownTime)
        {
            _countdownTimerText.text = a_countdownTime > 0 ? a_countdownTime.ToString() : "GAME OVER!";
        }

        private void UpdateHealthbar(int a_currentHealth, int a_maxHealth)
        {
            _healthbarFill.fillAmount = (float)a_currentHealth / a_maxHealth;
        }

        #endregion Custom Methods
    }
}
