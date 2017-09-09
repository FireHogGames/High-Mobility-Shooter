using UnityEngine;

public class SettingsManager : MonoBehaviour {
    
    public float musicVolume = 1f;
    public float sfxVolume = 1f;
    public int quality = 0;

    public static SettingsManager singleton;

    private void Start()
    {
        singleton = this;
        DontDestroyOnLoad(this);
        quality = QualitySettings.GetQualityLevel();
        musicVolume = PlayerPrefs.GetFloat("mVolume");
        sfxVolume = PlayerPrefs.GetFloat("sVolume");
        quality = PlayerPrefs.GetInt("quality");
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("mVolume", musicVolume);
        PlayerPrefs.SetFloat("sVolume", sfxVolume);
        PlayerPrefs.SetInt("quality", quality);
    }

    public void setSettings()
    {
        QualitySettings.SetQualityLevel(quality);
    }
}
