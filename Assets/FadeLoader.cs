using DG.Tweening;
using OTBG.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeLoader : Singleton<FadeLoader>
{
    public float fadeInTime;
    public float fadeOutTime;


    public Image fadeImage;

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }


    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
    }

    private void Start()
    {
        FadeToNothing(0);
    }

    private void FadeToBlack()
    {
        fadeImage.DOFade(1, fadeInTime);
    }

    private void FadeToNothing(float time)
    {
        fadeImage.DOFade(0, time);
    }

    public void LoadLevel(string levelName)
    {
        StartCoroutine(Load(levelName));
    }
    public void LoadLevel(int levelIndex)
    {
        StartCoroutine(Load(levelIndex));
    }

    private IEnumerator Load(string levelName)
    {
        FadeToBlack();

        yield return new WaitForSeconds(fadeInTime);

        SceneManager.LoadSceneAsync(levelName);

    }
    private IEnumerator Load(int levelIndex)
    {
        FadeToBlack();

        yield return new WaitForSeconds(fadeInTime);

        SceneManager.LoadSceneAsync(levelIndex);

    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        FadeToNothing(fadeOutTime);
    }

}
