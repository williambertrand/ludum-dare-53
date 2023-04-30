using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinder : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public PlayerController controller;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        StartCoroutine(WaitForPlayer());   
    }

    public IEnumerator WaitForPlayer()
    {
        while (controller == null)
        {
            controller = FindObjectOfType<PlayerController>();
            yield return null;
        }

        virtualCamera.Follow = controller.transform;
    }
}
