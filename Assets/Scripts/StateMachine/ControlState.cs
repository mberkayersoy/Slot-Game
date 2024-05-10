using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class ControlState : GameState
{
    private PayLineSO[] _payLines;
    private SlotBoardGenerator _slotBoardManager;
    private List<PayLineComboData> _matchingPaylines = new List<PayLineComboData>();
    private int _minCombo = 3;
    private int _wildID = 0; // Zero is a Wild Symbol ID.
    private int _scatterID = 11; // Eleven is a Scatter Symbol ID.
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

        if (_stateMachine.SlotGameManager.CalculateWithDFS)
        {

        }
        else
        {
            CalculateValidPayLines();
        }

    }

    public override void OnExit()
    {
        _stateMachine.SlotGameManager.PaymentCalculator.CalculatePayment(_matchingPaylines);

    }

    private void CalculateWithDFS()
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

                BaseSlotSymbolSO checkingSymbol = SlotGameCommonExtensions.GetCell(_slotBoardManager.Board, column, row);
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
                if (currentSymbol is StandardSlotSymbolSO)
                {
                    _matchingPaylines.Add(new PayLineComboData(currentCombo, currentSymbol as StandardSlotSymbolSO, _payLines[i]));
                }

            }
        }
        CalculateScatters();
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

        if (_currentScatterCount >= _minCombo) 
        {
            _scatter.ApplySymbolFeature(_currentScatterCount);
        }

        _stateMachine.ChangeState(_stateMachine.FeedbackState);
    }

    //private void CalculatePayLinesWithDFS()
    //{
    //    DFS(0, new List<string>());

    //    foreach (var payline in _payLines)
    //    {
    //        string paylineString = string.Join(", ", payline);
    //        Debug.Log("Payline: " + paylineString);
    //    }

    //    for (int i = 0; i < _payLines.Count; i++)
    //    {
    //        if (paylines[i].Count > 1)
    //        {
    //            var line = Instantiate(_lineRenderer, Vector3.zero, Quaternion.identity);
    //            line.material.color = new Color(Random.value, Random.value, Random.value);
    //            for (int j = 0; j < paylines[i].Count; j++)
    //            {
    //                int column = int.Parse(paylines[i][j][0].ToString());
    //                int row = int.Parse(paylines[i][j][1].ToString());
    //                line.SetPosition(j, new Vector3(column, row, 0));
    //            }
    //        }
    //    }
    //}

    //void DFS(int column, List<string> path)
    //{
    //    if (column == ColumnCount)
    //    {
    //        paylines.Add(new List<string>(path));
    //        return;
    //    }

    //    for (int row = 0; row < RowCount; row++)
    //    {
    //        if (path.Count > 0)
    //        {
    //            int prevRow = int.Parse(path[path.Count - 1][1].ToString());
    //            if (Mathf.Abs(prevRow - row) > 1)
    //                continue; // Not neighbour, skip
    //        }

    //        path.Add(column.ToString() + row.ToString());
    //        DFS(column + 1, path);
    //        path.RemoveAt(path.Count - 1);
    //    }
    //}
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
