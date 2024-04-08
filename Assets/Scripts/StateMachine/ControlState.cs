using System.Collections.Generic;
using UnityEngine;

public class ControlState : GameState
{
    private PayLineSO[] _payLines;
    private SlotBoardGenerator _slotBoardManager;
    private List<PayLineComboData> _matchingPaylines = new List<PayLineComboData>();
    private int _minCombo = 3;
    private int _wildID = 0; // Zero is a Wild Symbol ID.
    private int _scatterID = 11; // Zero is a Wild Symbol ID.
    private int _currentScatterCount;
    public ControlState(StateMachine stateMachine, PayLineSO[] paylines) : base(stateMachine)
    {
        _payLines = paylines;
        _slotBoardManager = _stateMachine.SlotGameManager.SlotBoardManager;
    }

    public List<PayLineComboData> MatchingPaylines { get => _matchingPaylines; private set => _matchingPaylines = value; }

    public override void OnEnter()
    {
        _matchingPaylines.Clear();
        CalculateValidPayLines();
    }

    public override void OnExit()
    {
        _stateMachine.SlotGameManager.PaymentCalculator.CalculatePayment(_matchingPaylines);
        CalculateScatters();
    }
    private void CalculateValidPayLines()
    {
        for (int i = 0; i < _payLines.Length; i++)
        {
            List<(int, int)> payLinePath = _payLines[i].GetPayLineCellsWithOrder();
            int currentCombo = 0;
            BaseSlotSymbolSO currentSymbol = null;
            bool validPaylineFound = true;

            foreach (var position in payLinePath)
            {
                int column = position.Item1;
                int row = position.Item2;

                BaseSlotSymbolSO checkingSymbol = _slotBoardManager.GetCell(column, row);
                if (currentSymbol == null)
                {
                    currentSymbol = checkingSymbol;
                    currentCombo = 1; // Start a new combo with the current symbol
                    continue;
                }
                else if (checkingSymbol.SymbolID.Equals(currentSymbol.SymbolID) || checkingSymbol.SymbolID.Equals(_wildID)) 
                {
                    currentCombo++;
                }
                else
                {
                    if (currentCombo < _minCombo)
                    {
                        validPaylineFound = false;
                    }
                    break;
                }
            }

            if (validPaylineFound)
            {

                _matchingPaylines.Add(new PayLineComboData(currentCombo, currentSymbol as StandardSlotSymbolSO, _payLines[i]));
            }
        }

        foreach (PayLineComboData payLineComboData in _matchingPaylines)
        {
            Debug.Log("Valid  " + payLineComboData.PayLineSO.name + " Combo length: " + payLineComboData.Combo + " SymbolID: " + payLineComboData.SymbolSO.SymbolID);
        }
        _stateMachine.ChangeState(_stateMachine.FeedbackState);
    }

    private void CalculateScatters()
    {
        _currentScatterCount = 0;
        ScatterSymbolSO _scatter = null;
        foreach (var item in _slotBoardManager.Board)
        {
            if (item.SymbolID.Equals(_scatterID))
            {
                _currentScatterCount++;
                _scatter = item as ScatterSymbolSO;
            }
        }

        if ( _currentScatterCount >= 3) 
        {
            _scatter.ApplySymbolFeature(_currentScatterCount);
        }
    }

}

public struct PayLineComboData
{
    public int Combo;
    public StandardSlotSymbolSO SymbolSO;
    public PayLineSO PayLineSO;

    public PayLineComboData(int combo, StandardSlotSymbolSO symbolSO, PayLineSO payLineSO)
    {
        Combo = combo;
        SymbolSO = symbolSO;
        PayLineSO = payLineSO;
    }
}
