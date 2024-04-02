using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PayLineSO", menuName = "New Pay Line")]
public class PayLineSO : ScriptableObject
{
    private const int _row = 3;
    private const int _column = 5;
    [SerializeField]private PayLineCell[] _payLine = new PayLineCell[_row * _column];

    public int RowCount => _row;
    public int ColumnCount => _column;

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
                    //Debug.Log("column: " + column + " row: " + row);
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