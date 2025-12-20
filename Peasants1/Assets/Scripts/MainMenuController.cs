using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private Button soundEnabledButton;
    [SerializeField] private Button backButton;

    [Header("Settings UI")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject buttonsContainer;
    [SerializeField] private GameObject backgroundMusic;
    
    [Header("Scene Config")]
    [SerializeField] private string newGameSceneName = "update this to the actual scene name";

    private void Awake()
    {
        if (newGameButton != null)
            newGameButton.onClick.AddListener(OnNewGameClicked);

        if (settingsButton != null)
            settingsButton.onClick.AddListener(OnSettingsClicked);

        if (exitButton != null)
            exitButton.onClick.AddListener(OnExitClicked);

        if (backButton != null)
            backButton.onClick.AddListener(OnBackClicked);

        if (soundEnabledButton != null)
            soundEnabledButton.onClick.AddListener(OnSoundEnabledClicked);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        if (newGameButton != null)
            newGameButton.onClick.RemoveListener(OnNewGameClicked);

        if (settingsButton != null)
            settingsButton.onClick.RemoveListener(OnSettingsClicked);

        if (exitButton != null)
            exitButton.onClick.RemoveListener(OnExitClicked);

        if (backButton != null)
            backButton.onClick.RemoveListener(OnBackClicked);

        if (soundEnabledButton != null)
            soundEnabledButton.onClick.RemoveListener(OnSoundEnabledClicked);
    }

    private void OnNewGameClicked()
    {
        if (!string.IsNullOrEmpty(newGameSceneName))
        {
            SceneManager.LoadScene(newGameSceneName);
        }
        else
        {
            Debug.LogWarning("MainMenuController: No scene name set for New Game.");
        }
    }

    private void OnSettingsClicked()
    {
        bool showSettings = settingsPanel == null || !settingsPanel.activeSelf;
        settingsButton.GetComponentInChildren<Text>().color = settingsButton.GetComponentInChildren<ButtonTextHover>().GetNormalColor();
        ToggleSettings(showSettings);
    }

    private void OnBackClicked()
    {
        backButton.GetComponentInChildren<Text>().color = backButton.GetComponentInChildren<ButtonTextHover>().GetNormalColor();
        ToggleSettings(false);
    }

    private void OnSoundEnabledClicked()
    {
        Debug.Log("Sound enabled clicked");
        string soundEnabledText = soundEnabledButton.GetComponentInChildren<Text>().text;
        Debug.Log("soundEnabledText: " + soundEnabledText);
        if (soundEnabledText == "sound enabled: on") {
            soundEnabledText = "sound enabled: off";
            SettingsManager.Instance.CurrentSettings.soundEnabled = false;
            Debug.Log(backgroundMusic.GetComponentInChildren<AudioSource>());
            backgroundMusic.GetComponentInChildren<AudioSource>().Stop();
        } else {
            soundEnabledText = "sound enabled: on";
            SettingsManager.Instance.CurrentSettings.soundEnabled = true;
            Debug.Log(backgroundMusic.GetComponentInChildren<AudioSource>());
            backgroundMusic.GetComponentInChildren<AudioSource>().Play();
        }
        //SettingsManager.Instance.CurrentSettings.soundEnabled = !SettingsManager.Instance.CurrentSettings.soundEnabled;
        soundEnabledButton.GetComponentInChildren<Text>().text = soundEnabledText;
        SettingsManager.Instance.SaveSettings();
    }

    private void ToggleSettings(bool showSettings)
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(showSettings);

        if (buttonsContainer != null)
            buttonsContainer.SetActive(!showSettings);
    }

    private void OnExitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

