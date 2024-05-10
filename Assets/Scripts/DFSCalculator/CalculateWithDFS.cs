using System.Collections.Generic;
using UnityEngine;

public class CalculateWithDFS : MonoBehaviour
{
    [SerializeField] private List<List<string>> paylines = new List<List<string>>();
    [SerializeField] private string[] _board = new string[SlotGameCommonExtensions.ROW_COUNT * SlotGameCommonExtensions.COLUMN_COUNT];
    [SerializeField] private LineRenderer _lineRenderer;

    private void Awake()
    {
        for (int column = 0; column < SlotGameCommonExtensions.COLUMN_COUNT; column++)
        {
            for (int row = 0; row < SlotGameCommonExtensions.ROW_COUNT; row++)
            {
                SlotGameCommonExtensions.SetCell(_board, column, row ,column.ToString() + row.ToString());
            }
        }
        CalculatePayLinesWithDFS();
    }

    public string GetCell(int column, int row)
    {
        if (row < 0 || row >= SlotGameCommonExtensions.ROW_COUNT || column < 0 || column >= SlotGameCommonExtensions.COLUMN_COUNT)
        {
            Debug.LogError("Invalid row or column index!");
            return default;
        }

        int index = column * SlotGameCommonExtensions.ROW_COUNT + row;
        return _board[index];
    }

    public int GetBoardIndex(int column, int row)
    {
        int index = column * SlotGameCommonExtensions.COLUMN_COUNT + row;
        return index;
    }

    public void SetCell(int column, int row, string value)
    {
        if (row < 0 || row >= SlotGameCommonExtensions.ROW_COUNT || column < 0 || column >= SlotGameCommonExtensions.COLUMN_COUNT)
        {
            Debug.LogError("Invalid row or column index!");
            return;
        }

        int index = column * SlotGameCommonExtensions.ROW_COUNT + row;
        _board[index] = value;
    }

    private void CalculatePayLinesWithDFS()
    {
        DFS(0, new List<string>());

        foreach (var payline in paylines)
        {
            string paylineString = string.Join(", ", payline);
            Debug.Log("Payline: " + paylineString);
        }

        for (int i = 0; i < paylines.Count; i++)
        {
            if (paylines[i].Count > 1)
            {
                var line = Instantiate(_lineRenderer, Vector3.zero, Quaternion.identity);
                line.material.color = new Color(Random.value, Random.value, Random.value);
                for (int j = 0; j < paylines[i].Count; j++)
                {
                    int column = int.Parse(paylines[i][j][0].ToString());
                    int row = int.Parse(paylines[i][j][1].ToString());
                    line.SetPosition(j, new Vector3(column, row, 0));
                }
            }
        }
    }

    void DFS(int column, List<string> path)
    {
        if (column == SlotGameCommonExtensions.COLUMN_COUNT)
        {
            paylines.Add(new List<string>(path));
            return;
        }

        for (int row = 0; row < SlotGameCommonExtensions.ROW_COUNT; row++)
        {
            if (path.Count > 0)
            {
                int prevRow = int.Parse(path[path.Count - 1][1].ToString());
                if (Mathf.Abs(prevRow - row) > 1)
                    continue; // Not neighbour, skip
            }

            path.Add(column.ToString() + row.ToString());
            DFS(column + 1, path);
            path.RemoveAt(path.Count - 1);
        }
    }
}
