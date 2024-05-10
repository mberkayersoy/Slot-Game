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
    private BaseSlotSymbolSO[] _board = new BaseSlotSymbolSO[SlotGameCommonExtensions.ROW_COUNT * SlotGameCommonExtensions.COLUMN_COUNT];
    private RandomSelectorWithWeight<BaseSlotSymbolSO> _selector = new RandomSelectorWithWeight<BaseSlotSymbolSO>();

    public BaseSlotSymbolSO[] Board { get => _board; private set => _board = value; }

    public BaseSlotSymbolSO[] GenerateBoard()
    {
        for (int column = 0; column < SlotGameCommonExtensions.COLUMN_COUNT; column++)
        {
            for (int row = 0; row < SlotGameCommonExtensions.ROW_COUNT; row++)
            {
                BaseSlotSymbolSO symbol;
                do
                {
                    symbol = _selector.GetRandomWithWeight(_slotSymbols);
                } while (column == 0 && symbol.SymbolID == 0);

                SlotGameCommonExtensions.SetCell(_board, column, row, symbol);
            }
        }

        return _board;
    }
}
