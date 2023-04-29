using UnityEngine;
using UnityEngine.UI;

public class AudioTabUI : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    private AudioTab _audioTab;

    public void Initialise(AudioTab tab, float bgm, float sfx)
    {
        _audioTab = tab;
        bgmSlider.value = bgm.GetPercentageBetween(-80, 0);
        sfxSlider.value = sfx.GetPercentageBetween(-80, 0);
    }

    public void OnBGMUpdated(float value)
    {
        _audioTab.SaveBGMSetting(value);
    }

    public void OnSFXUpdated(float value)
    {
        _audioTab.SaveSFXSetting(value);
    }

}
