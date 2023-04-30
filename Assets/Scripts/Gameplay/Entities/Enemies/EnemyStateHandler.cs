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
    
    public void PushState(EnemyState newState)
    {
        CurrentState.Pause();
        stateStack.Push(CurrentState);
        CurrentState = newState;
        newState.Enter();
    }

    public void PopState()
    {
        CurrentState.Exit();

        if (stateStack.Count == 0)
        {
            throw new Exception("Trying to pop from an empty state stack");
        }
        EnemyState prevState = stateStack.Pop();
        CurrentState = prevState;
        prevState.Resume();
    }
}