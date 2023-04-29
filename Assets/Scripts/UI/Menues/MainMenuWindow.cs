using OTBG.UI.Menues;
using UnityEngine.SceneManagement;

public class MainMenuWindow : Menu
{
    public string gameplaySceneName;

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
        SceneManager.LoadSceneAsync(gameplaySceneName);
    }

    public void ExitGame()
    {
        ApplicationManager.Instance.ExitGame();
    }
}
