using UnityEngine;
using UnityEngine.UI;

public class InfoPanelPayLineDrawer : MonoBehaviour
{
    public void SetPayLineColor(PayLineSO paylineSO)
    {
        Image[] images = GetComponentsInChildren<Image>();

        foreach (var pos in paylineSO.GetPayLineCellsWithOrder())
        {
            images[1 + GetCell(pos.Item1, pos.Item2)].color = paylineSO.LineColor;
        }
    }
    private int GetCell(int column, int row, int rowCount = 3)
    {
        int index = column * rowCount + row;
        return index;
    }
}
