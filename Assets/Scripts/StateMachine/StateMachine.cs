using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
   public State CurrentState { get; private set; }

    public void Initialize(State startState)
    {
        CurrentState = startState;
        startState.Enter();
    }
    public void ChangeState(State newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        newState.Enter();
    }
}
