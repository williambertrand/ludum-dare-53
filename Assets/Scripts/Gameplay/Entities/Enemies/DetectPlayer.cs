using System;
using System.Collections;
using System.Collections.Generic;
using OTBG.Utility;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public SubscribablePhysics2D detectionTrigger;
    private Enemy enemyRef;
    
    void Awake()
    {
        detectionTrigger = GetComponentInChildren<SubscribablePhysics2D>();
        enemyRef = GetComponentInParent<Enemy>();
    }

    void Start()
    {
        detectionTrigger.OnTriggerEnterEvent += EnemyDetection_OnTriggerEnterEvent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        detectionTrigger.OnTriggerEnterEvent -= EnemyDetection_OnTriggerEnterEvent;
    }
    
    private void EnemyDetection_OnTriggerEnterEvent(Collider2D obj)
    {
        Debug.Log("on enter: " + obj.ToString());
        if (obj.CompareTag("Player"))
        {
            enemyRef.SetTarget(obj.gameObject);
            enemyRef.stateMachine.ChangeState(enemyRef.seekingState);
        }
    }
}
