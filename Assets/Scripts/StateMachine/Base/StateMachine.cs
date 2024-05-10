using UnityEngine;
public class StateMachine
{
    private GameState _currentState;
    public readonly SlotGameManager SlotGameManager;
    private IdleState _idleState;
    private SpinState _spinState;
    private ControlState _controlState;
    private FeedbackState _feedbackState; 
    public StateMachine(SlotGameManager sgm)
    {
        SlotGameManager = sgm;
        _idleState = new IdleState(this);
        _spinState = new SpinState(this);
        _controlState = new ControlState(this);
        _feedbackState = new FeedbackState(this);
        ChangeState(_idleState);
    }
    public GameState CurrentState { get => _currentState; private set => _currentState = value; }
    public IdleState IdleState { get => _idleState; private set => _idleState = value; }
    public SpinState SpinState { get => _spinState; private set => _spinState = value; }
    public ControlState CalculationState { get => _controlState; private set => _controlState = value; }
    public FeedbackState FeedbackState { get => _feedbackState; private set => _feedbackState = value; }

    public void ChangeState(GameState newState)
    {
        if (CurrentState != null)
        {
            CurrentState.OnExit();
        }

        CurrentState = newState;
        CurrentState.OnEnter();
    }
}