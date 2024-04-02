
using System;
using UnityEngine;

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
        if (_sm == null) Debug.Log("_sm null!");
        if (_sm._sgm == null) Debug.Log("_sm._sgm null!");
        if (_sm._sgm.SlotBoardManager == null) Debug.Log("Slot Manager null");
        _uiManager.OnBoardGenerated(_sm._sgm.SlotBoardManager.GenerateBoard(), _sm._sgm.SlotBoardManager.ColumnCount, _sm._sgm.SlotBoardManager.RowCount);
    }

    public override void OnExit()
    {


    }
    private void SetNextState()
    {
        _sm.ChangeState(_sm.CalculationState);
    }


}