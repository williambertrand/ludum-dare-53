using OTBG.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayMusicTrack(MusicTrackIDs.BGM_MAINMENU);
    }

    private void OnDisable()
    {
        
        AudioManager.Instance.StopMusicTrack(MusicTrackIDs.BGM_MAINMENU);
    }
}
