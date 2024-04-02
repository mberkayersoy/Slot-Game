
using UnityEngine;

public class FeedbackState : GameState
{
    private UIManager _uiManager;
    public FeedbackState(StateMachine stateMachine, UIManager uiManager) : base(stateMachine)
    {
        _uiManager = uiManager;
    }

    public override void OnEnter()
    {

        _uiManager.OnPayLineDetected(_sm.CalculationState.MatchingPaylines);
    }

    public override void OnExit()
    {
        
    }

}