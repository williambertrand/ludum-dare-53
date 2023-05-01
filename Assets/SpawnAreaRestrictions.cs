using Cinemachine;
using OTBG.Utility;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

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
        restrictionTrigger.OnTriggerEnterEvent += RestrictionTrigger_OnTriggerEnterEvent;
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
        SetGlobalConfiner();
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
        if (GetComponent<SpawnArea>() != area)
            return;

        SetGlobalConfiner();

        ToggleBarriers(false);
        SpawnManager.OnAreaFinished -= FinishWave;
    }

    public void SetGlobalConfiner()
    {
        PolygonCollider2D globalConfiner = FindObjectOfType<CameraGlobalConfiner>().GetComponent<PolygonCollider2D>();
        virtualCam.GetComponent<CinemachineConfiner>().m_BoundingShape2D = globalConfiner;
    }

    public void ToggleBarriers(bool isActive)
    {
        rightCollider.SetActive(isActive);
        leftCollider.SetActive(isActive);
    }

    public void RestrictionTrigger_OnTriggerEnterEvent(Collider2D obj)
    {
        restrictionTrigger.gameObject.SetActive(false);
        SpawnManager.Instance.StartSpawnArea(GetComponent<SpawnArea>());
        virtualCam.GetComponent<CinemachineConfiner>().m_BoundingShape2D = restrictionBounds;
        ToggleBarriers(true);
    }
}
