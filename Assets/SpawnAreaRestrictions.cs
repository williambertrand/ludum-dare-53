using Cinemachine;
using OTBG.Utility;
using QFSW.QC.Editor.Tools;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SpawnAreaRestrictions : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCam;

    public PolygonCollider2D restrictionBounds;
    public SubscribablePhysics2D restrictionTrigger;

    public GameObject rightCollider;
    public GameObject leftCollider;

    private void Awake()
    {
        StartCoroutine(WaitForCamera());
        restrictionTrigger = GetComponentInChildren<SubscribablePhysics2D>();
    }
    

    public IEnumerator WaitForCamera()
    {
        while(virtualCam == null)
        {
            virtualCam = FindObjectOfType<CinemachineVirtualCamera>();
            yield return null;
        }
    }

    public void Start()
    {
        restrictionTrigger.OnTriggerEnterEvent += RestrictionTrigger_OnTriggerEnterEvent;
        SpawnManager.OnAreaFinished += FinishWave;
        ToggleBarriers(false);
    }

    private void OnDisable()
    {
        restrictionTrigger.OnTriggerEnterEvent -= RestrictionTrigger_OnTriggerEnterEvent;
    }

    [Button]
    public void FinishWave(SpawnArea area)
    {
        PolygonCollider2D globalConfiner = FindObjectOfType<CameraGlobalConfiner>().GetComponent<PolygonCollider2D>();
        virtualCam.GetComponent<CinemachineConfiner>().m_BoundingShape2D = globalConfiner;
        SpawnManager.OnAreaFinished -= FinishWave;

        ToggleBarriers(false);
    }

    public void ToggleBarriers(bool isActive)
    {
        rightCollider.SetActive(isActive);
        leftCollider.SetActive(isActive);
    }

    public void RestrictionTrigger_OnTriggerEnterEvent(Collider2D obj)
    {
        restrictionTrigger.enabled = false;
        SpawnManager.Instance.StartSpawnArea(GetComponent<SpawnArea>());
        virtualCam.GetComponent<CinemachineConfiner>().m_BoundingShape2D = restrictionBounds;
        ToggleBarriers(true);
    }
}
