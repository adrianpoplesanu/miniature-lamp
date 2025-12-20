using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSettingsLoader : MonoBehaviour
{
    [SerializeField] private Button soundEnabledButton;
    [SerializeField] private GameObject backgroundMusic;
    void Start() {
        foreach (AudioSource src in FindObjectsOfType<AudioSource>(true)) {
            Debug.Log("stopping audio source: " + src.name);
            src.Stop();
        }
        bool soundEnabled = SettingsManager.Instance.CurrentSettings.soundEnabled;
        if (soundEnabled) {
            soundEnabledButton.GetComponentInChildren<Text>().text = "sound enabled: on";
            Debug.Log("starting background music");
            Debug.Log(backgroundMusic.scene.name);
            backgroundMusic.GetComponentInChildren<AudioSource>().Play();
        } else {
            soundEnabledButton.GetComponentInChildren<Text>().text = "sound enabled: off";
            Debug.Log("stopping background music");
            Debug.Log(backgroundMusic.scene.name);
            backgroundMusic.GetComponentInChildren<AudioSource>().Stop();
        }
    }

    void Update() {
        
    }
}
