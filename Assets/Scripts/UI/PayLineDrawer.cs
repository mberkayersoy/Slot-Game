using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using Zenject;

public class PayLineDrawer : MonoBehaviour
{
    [Inject] private UIManager _uiManager;
    [SerializeField] private UILineRenderer _lineRenderer;
    [SerializeField] private float _waitToDrawTime;
    private void Awake()
    {
        _uiManager.PayLineDetected += DrawPayLines;
    }
    public void DrawPayLines(List<PayLineComboData> list)
    {
        if (list.Count <= 0)
        {
            Invoke(nameof(PayLineDone), 0.5f);
        }
        else
        {
            StartCoroutine(DrawLineWait(list));
        }
    }

    private void PayLineDone()
    {
        _uiManager.OnPayLinesDone();
        _lineRenderer.color = new Color(0, 0, 0, 0);
    }

    private IEnumerator DrawLineWait(List<PayLineComboData> list)
    {
        yield return new WaitForSeconds(_waitToDrawTime);
        foreach (PayLineComboData data in list)
        {
            PayLineSO payLineSO = data.PayLineSO;
            List<(int, int)> payLineCellPositions = payLineSO.GetPayLineCellsWithOrder();

            for (int i = 0; i < payLineCellPositions.Count; i++)
            {
                (int column, int row) = payLineCellPositions[i];

                if (_uiManager.Reels[column].childCount <= row)
                {
                    Debug.LogError("Child index out of range! Column: " + column + ", Row: " + row);
                    continue;
                }

                RectTransform cellRectTransform = _uiManager.Reels[column].GetChild(row).GetComponent<RectTransform>();
                InstantScaleComponent scaleComponent = cellRectTransform.GetComponent<InstantScaleComponent>();
                if (cellRectTransform != null)
                {
                    Vector3 worldPosition = cellRectTransform.position;
                    Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(null, worldPosition);
                    _lineRenderer.Points[i] = screenPoint;

                    if (i < data.Combo)
                    {
                        scaleComponent.SetScale(_waitToDrawTime / 2);
                    }
                }
            }
            _lineRenderer.color = payLineSO.LineColor;
            _lineRenderer.LineThickness = 25;
            yield return new WaitForSeconds(_waitToDrawTime);
        }
        Invoke(nameof(PayLineDone), _waitToDrawTime);
    }

    private void OnDestroy()
    {
        _uiManager.PayLineDetected -= DrawPayLines;
    }
}
