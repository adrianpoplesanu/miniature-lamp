using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "GameScene";

    public void OnNewGameClicked()
    {
        Debug.Log("New Game button clicked");
        SceneManager.LoadScene(gameSceneName);
    }

    public void OnSettingsClicked()
    {
        Debug.Log("Settings button clicked");
    }

    public void OnQuitClicked()
    {
        Debug.Log("Quit button clicked");
    }
}
