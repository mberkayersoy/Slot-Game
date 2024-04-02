using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    protected StateMachine _sm;
    public GameState(StateMachine stateMachine)
    {
        _sm = stateMachine;
    }
    public virtual void OnEnter()
    {

    }

    public virtual void OnExit()
    {

    }
}
