using System;
using System.Collections;
using System.Collections.Generic;
using OTBG.Utility;
using UnityEngine;

public class CarriageController : MonoSingleton<CarriageController>
{

    private enum State
    {
        Moving,
        Still
    }

    public float moveSpeed;
    
    private State state;
    private Animator _animator;
    private Vector3 targetPosition;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        state = State.Still;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Still:
                break;
            case State.Moving:
                var step =  moveSpeed * Time.deltaTime; 
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

                // Check if the position of the cube and sphere are approximately equal.
                if (Vector3.Distance(transform.position, targetPosition) < 0.005f)
                {
                    // Swap the position of the cylinder.
                    _animator.SetBool("moving", false);
                    state = State.Still;
                }
                break;
        }
    }

    public void MoveToNextLocation(Vector3 location)
    {
        targetPosition = location;
        _animator.SetBool("moving", true);
        state = State.Moving;
    }
}
