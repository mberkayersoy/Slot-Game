using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SlotGameCommonExtensions
{
    public static int COLUMN_COUNT = 5;
    public static int ROW_COUNT = 3;
    public static void IterateSlotBoard<T>(this T[] board, System.Action<int, int, T> action)
    {

    }

    public static T GetCell<T>(T[]board, int column, int row)
    {
        if (row < 0 || row >= ROW_COUNT || column < 0 || column >= COLUMN_COUNT)
        {
            Debug.LogError("Invalid row or column index!");
            return default;
        }

        int index = column * ROW_COUNT + row;
        return board[index];
    }

    public static void SetCell<T>(T[] board, int column, int row, T value)
    {
        if (row < 0 || row >= ROW_COUNT || column < 0 || column >= COLUMN_COUNT)
        {
            Debug.LogError("Invalid row or column index!");
            return;
        }

        int index = column * ROW_COUNT + row;
        board[index] = value;
    }
}
