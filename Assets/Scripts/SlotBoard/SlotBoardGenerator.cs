using MyExtensions.RandomSelection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlotBoardGenerator
{
    public SlotBoardGenerator(BaseSlotSymbolSO[] slotSymbols)
    {
        _slotSymbols = slotSymbols;
    }
    private BaseSlotSymbolSO[] _slotSymbols;
    private BaseSlotSymbolSO[] _board = new BaseSlotSymbolSO[_rowCount * _columnCount];
    private RandomSelectorWithWeight<BaseSlotSymbolSO> _selector = new RandomSelectorWithWeight<BaseSlotSymbolSO>();
    private const int _rowCount = 3;
    private const int _columnCount = 5;
    public BaseSlotSymbolSO[] Board { get => _board; private set => _board = value; }

    public int RowCount => _rowCount;

    public int ColumnCount => _columnCount;

    public BaseSlotSymbolSO[] GenerateBoard()
    {
        for (int column = 0; column < _columnCount; column++)
        {
            for (int row = 0; row < _rowCount; row++)
            {
                BaseSlotSymbolSO symbol;
                do
                {
                    symbol = _selector.GetRandomWithWeight(_slotSymbols);
                } while (column == 0 && symbol.SymbolID == 0);

                SetCell(column, row, symbol);
            }
        }

        return _board;
    }


    public BaseSlotSymbolSO GetCell(int column, int row)
    {
        int index = column * _rowCount + row;
        //Debug.Log($"column: {column} , row: {row} , index: {index} , symbolID: {_board[index].SymbolID}");
        return _board[index];
    }


    public void SetCell(int column, int row, BaseSlotSymbolSO value)
    {
        int index = column * _rowCount + row;
        _board[index] = value;
    }
}
