using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace Solar.UI
{
    public class UIView : MonoBehaviour
    {
        #region  Var
        [Header("UI Slider ")]
        public Slider solarBarSlider;
        public Slider fuelBarSlider;

        [Header("UI Panel ")]
        public GameObject restartPanel;
        public GameObject inGamePanel;
        public GameObject gameOverPanel;

        [Header("UI Button and Text")]
        public Button playGameBtn;
        public Button openRestartPopUpBtn;
        public Button quitGameBtn;


        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI highScoreText;

        //private Variable 
        private int currentScore;
        private float scoreTimer;
        #endregion

        #region  Methods
        void Start()
        {
            InitialSetUp();
            openRestartPopUpBtn.onClick.AddListener(OnPressRestartBtn);
            quitGameBtn.onClick.AddListener(OnPressQuitBtn);
            playGameBtn.onClick.AddListener(OnPlayButtonClicked);
        }

        private void InitialSetUp()
        {
         
            currentScore = 0;
            scoreTimer = 0;
            scoreText.SetText("0");



            restartPanel.SetActive(false);
            inGamePanel.SetActive(true);
            gameOverPanel.SetActive(false);
        }

        void Update()
        {
            if (inGamePanel.activeInHierarchy)
            {
                scoreTimer += Time.deltaTime;

                if (scoreTimer >= 1f)
                {
                    currentScore++;
                    scoreText.SetText("Score : " + currentScore.ToString());
                    scoreTimer = 0f;
                }
            }
        }
        #region  events and func
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
                 GamerEventManager.PlayerHasDie();
                EnableGameOverUI();
            }
        }
        #endregion


        public void IncrementScore()
        {
            currentScore = (int)Time.deltaTime;
            scoreText.SetText("Score : " + currentScore.ToString());
        }


        public void EnableGameOverUI()
        {
            GameService.Instance.SetGamePause(true);
            gameOverPanel.SetActive(true);
            inGamePanel.SetActive(false);

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

            highScoreText.SetText("High Score: " + highScore.ToString());
        }


        public void OnPlayButtonClicked()
        {

            GameService.Instance.ResetGame();

            ResetUIView();
            gameOverPanel.SetActive(false);
            restartPanel.SetActive(false);
            inGamePanel.SetActive(true);
             GameService.Instance.SetGamePause(false);

        }
        void OnPressRestartBtn() => restartPanel.gameObject.SetActive(true);
        public void OnPressQuitBtn() => Application.Quit();
        #endregion


        #region ResetUI
        public void ResetUIView()
        {
            
            currentScore = 0;
            scoreText.text = $"Score : {currentScore}";
            solarBarSlider.value = 1f;
            fuelBarSlider.value = 1f;

            inGamePanel.SetActive(true);
            gameOverPanel.SetActive(false);
            restartPanel.SetActive(false);

        }
        #endregion

       

    }
}
