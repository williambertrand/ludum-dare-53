using OTBG.Audio;
using OTBG.UI.Popups;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTab : Popup
{
    [SerializeField]
    private AudioTabUI _audioUI;

    public override void Open()
    {
        base.Open();
        LoadData(); 
    }

    public override void Close()
    {
        base.Close();
    }

    public void LoadData()
    {
        float bgmVolume = SaveData.Load<float>(AudioManager.BGM_VOLUME);
        float sfxVolume = SaveData.Load<float>(AudioManager.SFX_VOLUME);

        _audioUI.Initialise(this, bgmVolume, sfxVolume);
    }

    public void SaveBGMSetting(float value)
    {
        AudioManager.Instance.SaveBGM(value.GetValueFromPercentage(-80, 0));
    }
    public void SaveSFXSetting(float value)
    {
        AudioManager.Instance.SaveBGM(value.GetValueFromPercentage(-80, 0));
    }
}
