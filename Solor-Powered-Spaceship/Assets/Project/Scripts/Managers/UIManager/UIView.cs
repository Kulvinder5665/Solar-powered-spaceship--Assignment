using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace Solar.UI
{
    public class UIView : MonoBehaviour
    {
        [Header("UI Slider ")]
        public Slider solarBarSlider;
        public Slider fuelBarSlider;

        [Header("UI Panel ")]
        public GameObject mainMenuPanel;
        public GameObject inGamePanel;
        public GameObject gameOverPanel;

        [Header("UI Button and Text")]
        public Button playGameBtn;
        public Button restartGameBtn;
        public Button quitGameBtn;

        // public TextMeshProUGUI pressOnScreen;
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI highScoreText;

        //private Variable 
        private int currentScore;
        private float scoreTimer;
        void Start()
        {
            InitialSetUp();
        }

        private void InitialSetUp()
        {
            Time.timeScale = 0;
            currentScore = 0;
            scoreTimer = 0;
            scoreText.SetText("0");

            playGameBtn.onClick.AddListener(OnPlayButtonClicked);

            mainMenuPanel.SetActive(true);
            inGamePanel.SetActive(false);
            gameOverPanel.SetActive(false);
        }

        void Update()
        {
            if (inGamePanel.activeInHierarchy)
            {
                scoreTimer += Time.deltaTime;

                if (scoreTimer >= 1f)
                {
                    currentScore ++;
                    scoreText.SetText(currentScore.ToString());
                    scoreTimer = 0f;
                }
            }
        }
        
        void OnEnable()
        {
            GamerEventManager.OnEnergyChange += UpdateSolarEnergyBar;
            GamerEventManager.OnFuelChange += UpdateFuelEnergyBar;
            // GamerEventManager.OnPlayerDie += GameOver;
        }
        void OnDisable()
        {
            GamerEventManager.OnEnergyChange -= UpdateSolarEnergyBar;
            GamerEventManager.OnFuelChange -= UpdateFuelEnergyBar;
        }

        void UpdateSolarEnergyBar(float currentEnergy, float maxEnergy)
        {
            solarBarSlider.value = currentEnergy / maxEnergy;

        }
        void UpdateFuelEnergyBar(float currentFuel, float maxFuel)
        {
            fuelBarSlider.value = currentFuel / maxFuel;
            if (fuelBarSlider.value <= 0)
            {
                // GamerEventManager.GameOver();
                EnableGameOverUI();
            }
        }

        public void OnPlayButtonClicked()
        {
            Time.timeScale = 1;
            mainMenuPanel.SetActive(false);
            inGamePanel.SetActive(true);
            
            
        }
        public void IncrementScore()
        {
            currentScore = (int)Time.deltaTime;
            scoreText.SetText(currentScore.ToString());
        }


        public void EnableGameOverUI()
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            inGamePanel.SetActive(false);

            restartGameBtn.onClick.RemoveAllListeners();
            quitGameBtn.onClick.RemoveAllListeners();

            restartGameBtn.onClick.AddListener(OnPressRestartBtn);
            quitGameBtn.onClick.AddListener(OnPressQuitBtn);

            HighscoreSaveAndLoad();
        }

        private void HighscoreSaveAndLoad()
        {
            int highScore = PlayerPrefs.GetInt("HighScore", 0);
            if (currentScore > highScore)
            {
                PlayerPrefs.SetInt("HighScore", currentScore);
                highScore = currentScore;
            }

            highScoreText.SetText("High Score:" + highScore.ToString());
        }

        // Need some  rebuilding
        void OnPressRestartBtn() => SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        public void OnPressQuitBtn() => Application.Quit();
     }
}
