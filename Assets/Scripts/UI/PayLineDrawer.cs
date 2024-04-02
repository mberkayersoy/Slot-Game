using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PayLineDrawer : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRendererPrefab;
    [SerializeField] private Transform lineContainer;
    [Inject] private UIManager _uiManager;
    private List<LineRenderer> drawnLines = new List<LineRenderer>();

    public void DrawPayLines(List<PayLineComboData> list)
    {
        ClearDrawnLines();

        foreach (PayLineComboData data in list)
        {
            PayLineSO payLineSO = data.PayLineSO;
            List<(int, int)> payLineCells = payLineSO.GetPayLineCellsWithOrder();

            // LineRenderer olu�tur
            LineRenderer lineRenderer = Instantiate(lineRendererPrefab, lineContainer);
            drawnLines.Add(lineRenderer);

            // �izgi rengi, kal�nl��� vb. ayarlar�n� yapabilirsiniz
            // lineRenderer.startColor = ...;

            // H�crelerin konumlar�na g�re �izgiyi �iz
            lineRenderer.positionCount = payLineCells.Count;
            for (int i = 0; i < payLineCells.Count; i++)
            {
                (int column, int row) = payLineCells[i];
                Vector3 cellPosition = new Vector3(column, row, 0); // H�crenin pozisyonu
                lineRenderer.SetPosition(i, cellPosition);
            }
        }
    }

    private void ClearDrawnLines()
    {
        foreach (var line in drawnLines)
        {
            Destroy(line.gameObject);
        }
        drawnLines.Clear();
    }

}
