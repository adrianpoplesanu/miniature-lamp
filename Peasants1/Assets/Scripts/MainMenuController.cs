using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button backButton;

    [Header("Settings UI")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject buttonsContainer;

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
        ToggleSettings(showSettings);
    }

    private void OnBackClicked()
    {
        ToggleSettings(false);
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

