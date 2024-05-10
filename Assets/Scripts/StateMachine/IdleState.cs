using UnityEngine;

public class IdleState : GameState
{
    private UIManager _uiManager;
    public IdleState(StateMachine stateMachine) : base(stateMachine)
    {
        _uiManager = stateMachine.SlotGameManager.UiManager;
        _uiManager.SpinClicked += SetSpinState;
    }
    public override void OnEnter()
    {
        if (_uiManager.AutoSpin)
        {
            SetSpinState();
        }
    }
    public override void OnExit() { }

    public void SetSpinState()
    {
        if (_stateMachine.CurrentState != this) return;

        if (_stateMachine.SlotGameManager.PaymentCalculator.CheckPlayerCanSpin())
        {
            _uiManager.OnSpinStarted();
            _stateMachine.ChangeState(_stateMachine.SpinState);
        }
    }
}