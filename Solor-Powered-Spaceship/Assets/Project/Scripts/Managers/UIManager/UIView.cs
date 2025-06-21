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

        // [Header("UI Panel ")]
        // public GameObject gameOverPanel;
        // public GameObject inGamePanel;

        // [Header("UI Button and Text")]
        // public Button restartGame;
        // public Button quitGame;

        // public TextMeshProUGUI pressOnScreen;
        // public TextMeshProUGUI scoreText;
        // public TextMeshProUGUI highScoreText;
        // void Start()
        // {
        //     restartGame.onClick.AddListener(OnPressRestart);
        //     quitGame.onClick.AddListener(OnPressQuit);
        // }
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
                GamerEventManager.GameOver();
            }
        }

        // void GameOver()
        // {
        //     TogglePanel(gameOverPanel, true);

        // }

    //     void TogglePanel(GameObject panel, bool toggle)
    //     {
    //         panel.gameObject.SetActive(toggle);
    //     }

    //     void OnPressRestart() => SceneManager.LoadSceneAsync(0);
    //     void OnPressQuit() => Application.Quit();
     }
}
