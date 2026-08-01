using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void OnNewGameClicked()
    {
        Debug.Log("New Game button clicked");
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
