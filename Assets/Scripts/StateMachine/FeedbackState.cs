
using System;
using UnityEngine;

public class FeedbackState : GameState
{
    private UIManager _uiManager;
    public FeedbackState(StateMachine stateMachine, UIManager uiManager) : base(stateMachine)
    {
        _uiManager = uiManager;
        _uiManager.PayLinesDone += SetIdleState;
    }

    private void SetIdleState()
    {
        _stateMachine.ChangeState(_stateMachine.IdleState);
    }

    public override void OnEnter()
    {
        _uiManager.OnPayLineDetected(_stateMachine.CalculationState.MatchingPaylines);
    }

    public override void OnExit() { }


}