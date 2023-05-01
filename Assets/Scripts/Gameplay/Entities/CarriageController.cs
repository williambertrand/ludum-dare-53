using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using OTBG.Utility;
using UnityEngine;

public class CarriageController : MonoSingleton<CarriageController>
{
    private PlayerController playerController;


    private enum State
    {
        Moving,
        Still
    }

    public float moveSpeed;
    public float speedyBoi;

    private State state;
    private Animator _animator;
    private Vector3 targetPosition;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        StartCoroutine(WaitForPlayer());
    }

    private void OnEnable()
    {
        SpawnManager.OnAreaStarted += SpawnManager_OnAreaStarted;
        SpawnManager.OnAreaFinished += SpawnManager_OnAreaFinished;
    }

    void Start()
    {
        state = State.Moving;
    }

    private void OnDisable()
    {
        SpawnManager.OnAreaStarted -= SpawnManager_OnAreaStarted;
        SpawnManager.OnAreaFinished -= SpawnManager_OnAreaFinished;
    }

    public IEnumerator WaitForPlayer()
    {
        while (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
            yield return null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Moving:
                Movement();
                break;
        }
    }

    public void Movement()
    {
        float distanceToPlayer = (playerController.transform.position - transform.position).x;
        if (distanceToPlayer < 0)
        {
            ToggleMovementAnim(false);

            return;
        }

        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        ToggleMovementAnim(true);

    }


    private void SpawnManager_OnAreaFinished(SpawnArea obj)
    {
        state = State.Moving;
        ToggleMovementAnim(true);

    }

    private void SpawnManager_OnAreaStarted(SpawnArea obj)
    {
        transform.DOMove(obj.wagonPosition.position, speedyBoi).SetSpeedBased(true)
            .OnStart(() => ToggleMovementAnim(true))
            .OnComplete(() =>
            {
                ToggleMovementAnim(false);
                state = State.Still;
            });
    }

    public void ToggleMovementAnim(bool isMoving)
    {
        _animator.SetBool("moving", isMoving);

    }
}
