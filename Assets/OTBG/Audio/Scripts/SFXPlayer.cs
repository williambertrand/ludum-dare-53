using OTBG.Audio;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SFXPlayer : MonoBehaviour
{
    [HideInInspector]
    public string nameToPlay;

    public bool isOneShot = true;

    public void PlaySFX()
    {
        AudioManager.Instance.PlaySoundEffect(nameToPlay, isOneShot);
    }
}
