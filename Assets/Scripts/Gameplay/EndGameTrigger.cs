using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    public TextMeshProUGUI winText;
    private void Awake()
    {
        winText.DOFade(0, 0);
    }
    IEnumerator FadeOut()
    {
        winText.DOFade(1, 2.0f);
        yield return new WaitForSeconds(5);
        FadeLoader.Instance.LoadLevel(GameScenes.MENU_SCENE);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<CarriageController>())
        {
            Debug.Log("WIN");
            StartCoroutine(FadeOut());
        }
    }
}
