using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public CinemachineVirtualCamera cam;

    public Vector3 startPos;

    public float startXPos;
    public float distance;
    public float distanceModifier;
    public void Awake()
    {
        StartCoroutine(WaitForPlayer());
    }

    private void Start()
    {
        startPos = transform.position;
    }

    public IEnumerator WaitForPlayer()
    {
        while(cam == null)
        {
            cam = FindObjectOfType<CinemachineVirtualCamera>();
            yield return null;
        }

        startXPos = cam.transform.position.x;
    }

    private void Update()
    {
        distance = Mathf.Clamp(cam.transform.position.x - startXPos, 0, 99999);
        transform.position = new Vector3(startPos.x - (distance * distanceModifier), startPos.y, startPos.z);
    }
}
