using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PayLineSO", menuName = "New Pay Line")]
public class PayLineSO : ScriptableObject
{
    private const int ROW_COUNT = 3;
    private const int COLUMN_COUNT = 5;
    [SerializeField] private Color _lineColor;
    [SerializeField]private PayLineCell[] _payLine = new PayLineCell[ROW_COUNT * COLUMN_COUNT];
    public int RowCount => ROW_COUNT;
    public int ColumnCount => COLUMN_COUNT;
    public Color LineColor { get => _lineColor; private set => _lineColor = value; }

    public PayLineCell GetCell(int column, int row)
    {
        if (row < 0 || row >= RowCount || column < 0 || column >= ColumnCount)
        {
            Debug.LogError("Invalid row or column index!");
            return PayLineCell.UnDefined;
        }

        int index = column * RowCount + row;
        return _payLine[index];
    }

    public void SetCell(int column, int row, PayLineCell value)
    {
        if (row < 0 || row >= RowCount || column < 0 || column >= ColumnCount)
        {
            Debug.LogError("Invalid row or column index!");
            return;
        }

        int index = column * RowCount + row;
        _payLine[index] = value;
    }

    public List<(int,int)> GetPayLineCellsWithOrder()
    {
        List<(int, int)> positions = new List<(int, int)>();
        for (int column = 0; column < ColumnCount; column++)
        {
            for (int row = 0; row < RowCount; row++)
            {
                if (PayLineCell.Defined == GetCell(column, row))
                {
                    positions.Add((column, row));

                }
            }
        }
        return positions;

    }
}


[System.Serializable]
public enum PayLineCell
{
    UnDefined,
    Defined
}