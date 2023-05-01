using DG.Tweening;
using Mono.CSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecretAchievement : MonoBehaviour
{

    public GameObject achievementObj;

    public float movementDistance;

    public void OnEnable()
    {
        PlayerMovementController.OnPlayerMove += PlayerMovementController_OnPlayerMove;
    }

    private void PlayerMovementController_OnPlayerMove()
    {
        PlayerMovementController.OnPlayerMove -= PlayerMovementController_OnPlayerMove;

        ShowAchievement();
    }

    public void OnDisable()
    {
        PlayerMovementController.OnPlayerMove -= PlayerMovementController_OnPlayerMove;
    }

    public void ShowAchievement()
    {
        StartCoroutine(MoveAchievement());
    }

    private IEnumerator MoveAchievement()
    {
        achievementObj.transform.DOMove(achievementObj.transform.position - Vector3.up * movementDistance, 1.5f);

        yield return new WaitForSeconds(5);

        achievementObj.transform.DOMove(achievementObj.transform.position + Vector3.up * movementDistance, 1.5f);

    }
}
