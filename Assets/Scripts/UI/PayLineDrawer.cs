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

            // LineRenderer oluþtur
            LineRenderer lineRenderer = Instantiate(lineRendererPrefab, lineContainer);
            drawnLines.Add(lineRenderer);

            // Çizgi rengi, kalýnlýðý vb. ayarlarýný yapabilirsiniz
            // lineRenderer.startColor = ...;

            // Hücrelerin konumlarýna göre çizgiyi çiz
            lineRenderer.positionCount = payLineCells.Count;
            for (int i = 0; i < payLineCells.Count; i++)
            {
                (int column, int row) = payLineCells[i];
                Vector3 cellPosition = new Vector3(column, row, 0); // Hücrenin pozisyonu
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
