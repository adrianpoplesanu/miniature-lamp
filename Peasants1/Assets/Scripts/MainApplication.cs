using UnityEngine;

public class MainApplication : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("running...");
        
        // Initialize settings manager early to ensure settings are loaded
        SettingsManager settingsManager = SettingsManager.Instance;
        Debug.Log("Settings loaded. Master Volume: " + settingsManager.CurrentSettings.masterVolume);
        
        test();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void test() {
        Debug.Log("test...");
    }
}
