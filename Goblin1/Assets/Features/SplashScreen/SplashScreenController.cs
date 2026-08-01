using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{
    [SerializeField] private float timeoutSeconds = 5f;
    [SerializeField] private string nextSceneName = "MainMenu";

    private void Start()
    {
        StartCoroutine(LoadNextSceneAfterDelay());
    }

    private IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(timeoutSeconds);
        SceneManager.LoadScene(nextSceneName);
    }
}
