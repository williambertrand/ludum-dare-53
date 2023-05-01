using OTBG.UI.Menues;
using UnityEngine.SceneManagement;

public class MainMenuWindow : Menu
{

    public override void Open()
    {
        base.Open();
    }

    public override void Close()
    {
        base.Close();
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(GameScenes.GAMEPLAY_SCENE);
    }

    public void ExitGame()
    {
        ApplicationManager.Instance.ExitGame();
    }
}
