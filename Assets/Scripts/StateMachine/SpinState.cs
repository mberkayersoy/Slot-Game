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
        _uiManager.OnBoardGenerated(_stateMachine.SlotGameManager.SlotBoardManager.GenerateBoard(), _stateMachine.SlotGameManager.SlotBoardManager.ColumnCount, _stateMachine.SlotGameManager.SlotBoardManager.RowCount);
    }

    public override void OnExit()
    {


    }
    private void SetNextState()
    {
        _stateMachine.ChangeState(_stateMachine.CalculationState);
    }


}