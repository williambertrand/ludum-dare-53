using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnNotifier : MonoBehaviour
{

    public GameObject goDisplay;
    public float hideTime;


    public void OnEnable()
    {
        SpawnManager.OnAreaFinished += SpawnManager_OnAreaFinished;
        SpawnManager.OnAreaStarted += SpawnManager_OnAreaStarted;
    }

    private void SpawnManager_OnAreaStarted(SpawnArea obj)
    {
        goDisplay.SetActive(false);
    }

    public IEnumerator WaitToHide()
    {
        yield return new WaitForSeconds(hideTime);
        goDisplay.SetActive(false);
    }

    private void SpawnManager_OnAreaFinished(SpawnArea obj)
    {
        goDisplay.SetActive(true);
        StartCoroutine(WaitToHide());

    }

    private void OnDisable()
    {
        SpawnManager.OnAreaFinished -= SpawnManager_OnAreaFinished;
        SpawnManager.OnAreaStarted -= SpawnManager_OnAreaStarted;

    }
}
