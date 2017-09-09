using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour {

    public Slider musicSlider;
    public Slider SFXSlider;
    public Dropdown qualityDropDown;

    private void Start()
    {
        qualityDropDown.value = QualitySettings.GetQualityLevel();
        musicSlider.value = SettingsManager.singleton.musicVolume;
        SFXSlider.value = SettingsManager.singleton.sfxVolume;
        qualityDropDown.value = SettingsManager.singleton.quality;
    }

    //apply settings
    public void Apply()
    {
        SettingsManager.singleton.musicVolume = musicSlider.value;
        SettingsManager.singleton.sfxVolume = SFXSlider.value;
        SettingsManager.singleton.quality = qualityDropDown.value;
        SettingsManager.singleton.SaveSettings();
        SettingsManager.singleton.setSettings();
    }
}
