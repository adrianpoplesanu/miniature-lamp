using UnityEngine;

[System.Serializable]
public class GameSettings
{
    [Header("Audio Settings")]
    public float masterVolume = 1.0f;
    public float musicVolume = 1.0f;
    public float sfxVolume = 1.0f;
    public bool soundEnabled = true;

    [Header("Graphics Settings")]
    public int qualityLevel = 2; // Default to High
    public int resolutionWidth = 1920;
    public int resolutionHeight = 1080;
    public bool fullscreen = true;
    public int targetFrameRate = 60;

    [Header("Gameplay Settings")]
    public float mouseSensitivity = 1.0f;
    public bool invertYAxis = false;

    // Default constructor with default values
    public GameSettings()
    {
        // Use the default values defined above
    }

    // Create a deep copy of settings
    public GameSettings Clone()
    {
        return new GameSettings
        {
            masterVolume = this.masterVolume,
            musicVolume = this.musicVolume,
            sfxVolume = this.sfxVolume,
            soundEnabled = this.soundEnabled,
            qualityLevel = this.qualityLevel,
            resolutionWidth = this.resolutionWidth,
            resolutionHeight = this.resolutionHeight,
            fullscreen = this.fullscreen,
            targetFrameRate = this.targetFrameRate,
            mouseSensitivity = this.mouseSensitivity,
            invertYAxis = this.invertYAxis
        };
    }
}
