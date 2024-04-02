using System.Collections.Generic;
using UnityEngine;

public class ControlState : GameState
{
    private PayLineSO[] _payLines;
    private SlotBoardManager _slotBoardManager;
    private List<PayLineComboData> _matchingPaylines = new List<PayLineComboData>();
    public ControlState(StateMachine stateMachine, PayLineSO[] paylines) : base(stateMachine)
    {
        _payLines = paylines;
        //if (_sm == null) Debug.Log("_sm null!");
        //if (_sm._sgm == null) Debug.Log("_sm._sgm null!");
        //if (_sm._sgm.SlotBoardManager == null) Debug.Log("Slot Manager null");
        _slotBoardManager = _sm._sgm.SlotBoardManager;
    }

    public List<PayLineComboData> MatchingPaylines { get => _matchingPaylines; private set => _matchingPaylines = value; }

    public override void OnEnter()
    {
        _matchingPaylines.Clear();
        CalculateValidPayLines();
    }

    public override void OnExit()
    {
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
                    //Debug.Log("First Search => CurrentSymbol Null");
                    // Initialize symbol for this payline
                    currentSymbol = checkingSymbol;
                    currentCombo = 1; // Start a new combo with the current symbol
                    continue;
                }
                else if (checkingSymbol.SymbolID.Equals(currentSymbol.SymbolID))
                {
                    currentCombo++;
                }
                else
                {
                    if (currentCombo < 3)
                    {
                        validPaylineFound = false;
                    }
                }
            }
            if (validPaylineFound)
            {
                _matchingPaylines.Add(new PayLineComboData(currentCombo, currentSymbol, _payLines[i]));
            }
        }

        foreach (PayLineComboData payLineComboData in _matchingPaylines)
        {
            Debug.Log("Valid  " + payLineComboData.PayLineSO.name + " Combo length: " + payLineComboData.Combo + " SymbolID: " + payLineComboData.SymbolSO.SymbolID);
        }
        _sm.ChangeState(_sm.FeedbackState);
    }

}

public struct PayLineComboData
{
    public int Combo;
    public BaseSlotSymbolSO SymbolSO;
    public PayLineSO PayLineSO;

    public PayLineComboData(int combo, BaseSlotSymbolSO symbolSO, PayLineSO payLineSO)
    {
        Combo = combo;
        SymbolSO = symbolSO;
        PayLineSO = payLineSO;
    }
}
