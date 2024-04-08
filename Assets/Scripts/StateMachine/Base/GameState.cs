using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    protected StateMachine _stateMachine;
    public GameState(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    public virtual void OnEnter()
    {

    }

    public virtual void OnExit()
    {

    }
}
