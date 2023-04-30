using System.Collections.Generic;
using System;

public class EnemyStateHandler
{
    public EnemyState CurrentState { get; private set; }

    //Useful when we have a state we want to temporarily be in and then return to prev state
    public Stack<EnemyState> stateStack;
    
    public void Initialize(EnemyState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
        stateStack = new Stack<EnemyState>();
    }

    public void ChangeState(EnemyState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        newState.Enter();
    }
}