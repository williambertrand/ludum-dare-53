using System.Collections;
using System.Collections.Generic;
using OTBG.Audio;
using OTBG.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoSingleton<GamePlayManager>
{
    private void OnEnable()
    {
        PlayerController.OnHealthChanged += Entity_OnDamaged;
    }

    private void Start()
    {
        PlayAudio();
    }


    private void OnDisable()
    {
        PlayerController.OnHealthChanged -= Entity_OnDamaged;
        StopAudio();
    }

    public void OnPlayerDeath()
    {
        FadeLoader.Instance.LoadLevel(GameScenes.GAME_OVER_SCENE);
    }

    public void PlayAgain()
    {
        FadeLoader.Instance.LoadLevel(GameScenes.GAMEPLAY_SCENE);
    }
    
    public void ReturnToMenu()
    {
        FadeLoader.Instance.LoadLevel(GameScenes.MENU_SCENE);
    }

    public void PlayAudio()
    {
        AudioManager.Instance.PlayMusicTrack(MusicTrackIDs.BGM_AMBIENCE);
        AudioManager.Instance.PlayMusicTrack(MusicTrackIDs.BGM_GAMEPLAY);
    }

    public void StopAudio()
    {
        AudioManager.Instance.StopMusicTrack(MusicTrackIDs.BGM_AMBIENCE);
        AudioManager.Instance.StopMusicTrack(MusicTrackIDs.BGM_GAMEPLAY);
    }



    private void Entity_OnDamaged(ValueChange change)
    {
        if (change.value <= 0)
            OnPlayerDeath();
    }
}
