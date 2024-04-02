using UnityEngine;

public class IdleState : GameState
{
    private UIManager _uiManager;
    public IdleState(StateMachine stateMachine, UIManager uiManager) : base(stateMachine)
    {
        _uiManager = uiManager;
        _uiManager.SpinClicked += SetSpinState;
    }
    public override void OnEnter()
    {
        if (_uiManager.AutoSpin)
        {
            SetSpinState();
        }

    }

    public override void OnExit()
    {

    }
    public void SetSpinState()
    {
        if (_sm.CurrentState != this) return;

        _uiManager.OnSpinStarted();
        _sm.ChangeState(_sm.SpinState);
    }

}