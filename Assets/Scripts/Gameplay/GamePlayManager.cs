using System.Collections;
using System.Collections.Generic;
using OTBG.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoSingleton<GamePlayManager>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerDeath()
    {
        SceneManager.LoadScene(GameScenes.GAME_OVER_SCENE);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(GameScenes.GAMEPLAY_SCENE);
    }
    
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(GameScenes.MENU_SCENE);
    }
}
