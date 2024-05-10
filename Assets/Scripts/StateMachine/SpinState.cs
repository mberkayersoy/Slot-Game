public class SpinState : GameState
{
    private UIManager _uiManager;

    public SpinState(StateMachine stateMachine, UIManager uiManager) : base(stateMachine)
    {
        _uiManager = uiManager;
        _uiManager.SpinEnded += SetNextState;
    }
    public override void OnEnter()
    {
        _uiManager.OnBoardGenerated(_stateMachine.SlotGameManager.SlotBoardManager.GenerateBoard(), 
            SlotGameCommonExtensions.COLUMN_COUNT,
            SlotGameCommonExtensions.ROW_COUNT);
    }
    public override void OnExit() { }
    private void SetNextState()
    {
        _stateMachine.ChangeState(_stateMachine.CalculationState);
    }
}