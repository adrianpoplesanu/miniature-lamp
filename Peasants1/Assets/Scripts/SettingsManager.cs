using UnityEngine;
using System.IO;

public class SettingsManager : MonoBehaviour
{
    private static SettingsManager _instance;
    public static SettingsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject settingsObject = new GameObject("SettingsManager");
                _instance = settingsObject.AddComponent<SettingsManager>();
                DontDestroyOnLoad(settingsObject);
            }
            return _instance;
        }
    }

    private GameSettings _currentSettings;
    private const string SETTINGS_FILE_NAME = "gameSettings.json";

    public GameSettings CurrentSettings
    {
        get
        {
            if (_currentSettings == null)
            {
                LoadSettings();
            }
            return _currentSettings;
        }
    }

    private void Awake()
    {
        // Ensure singleton pattern
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveSettings();
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            SaveSettings();
        }
    }

    private void OnDestroy()
    {
        SaveSettings();
    }

    /// <summary>
    /// Load settings from persistent data path. Creates default settings if none exist.
    /// </summary>
    public void LoadSettings()
    {
        string filePath = GetSettingsFilePath();

        if (File.Exists(filePath))
        {
            try
            {
                string json = File.ReadAllText(filePath);
                _currentSettings = JsonUtility.FromJson<GameSettings>(json);
                
                // Apply the loaded settings
                ApplySettings(_currentSettings);
                
                Debug.Log("Settings loaded successfully from: " + filePath);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to load settings: " + e.Message);
                CreateDefaultSettings();
            }
        }
        else
        {
            Debug.Log("No settings file found, creating default settings.");
            CreateDefaultSettings();
        }
    }

    /// <summary>
    /// Save current settings to persistent data path.
    /// </summary>
    public void SaveSettings()
    {
        if (_currentSettings == null)
        {
            Debug.LogWarning("Cannot save settings: settings are null. Creating default settings.");
            CreateDefaultSettings();
            return;
        }

        try
        {
            string filePath = GetSettingsFilePath();
            string json = JsonUtility.ToJson(_currentSettings, true); // true for pretty print
            File.WriteAllText(filePath, json);
            Debug.Log("Settings saved successfully to: " + filePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save settings: " + e.Message);
        }
    }

    /// <summary>
    /// Update settings and save them immediately.
    /// </summary>
    public void UpdateSettings(GameSettings newSettings)
    {
        if (newSettings == null)
        {
            Debug.LogWarning("Cannot update settings: newSettings is null.");
            return;
        }

        _currentSettings = newSettings.Clone();
        ApplySettings(_currentSettings);
        SaveSettings();
    }

    /// <summary>
    /// Apply settings to the game (audio, graphics, etc.)
    /// </summary>
    private void ApplySettings(GameSettings settings)
    {
        // Apply audio settings
        if (AudioListener.volume != settings.masterVolume)
        {
            AudioListener.volume = settings.masterVolume;
        }

        // Apply quality settings
        if (QualitySettings.GetQualityLevel() != settings.qualityLevel)
        {
            QualitySettings.SetQualityLevel(settings.qualityLevel);
        }

        // Apply resolution and fullscreen
        if (Screen.width != settings.resolutionWidth || 
            Screen.height != settings.resolutionHeight || 
            Screen.fullScreen != settings.fullscreen)
        {
            Screen.SetResolution(settings.resolutionWidth, settings.resolutionHeight, settings.fullscreen);
        }

        // Apply frame rate
        Application.targetFrameRate = settings.targetFrameRate;
    }

    /// <summary>
    /// Create default settings and save them.
    /// </summary>
    private void CreateDefaultSettings()
    {
        _currentSettings = new GameSettings();
        
        // Use current screen resolution as default
        _currentSettings.resolutionWidth = Screen.width;
        _currentSettings.resolutionHeight = Screen.height;
        _currentSettings.fullscreen = Screen.fullScreen;
        
        // Use current quality level as default
        _currentSettings.qualityLevel = QualitySettings.GetQualityLevel();
        
        ApplySettings(_currentSettings);
        SaveSettings();
    }

    /// <summary>
    /// Get the file path for settings storage.
    /// </summary>
    private string GetSettingsFilePath()
    {
        return Path.Combine(Application.persistentDataPath, SETTINGS_FILE_NAME);
    }

    /// <summary>
    /// Reset settings to default values.
    /// </summary>
    public void ResetToDefaults()
    {
        CreateDefaultSettings();
    }
}
