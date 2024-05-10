using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using Zenject;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;

public class PayLineDrawer : MonoBehaviour
{
    [Inject] private UIManager _uiManager;
    [SerializeField] private UILineRenderer _lineRenderer;
    [SerializeField] private float _waitToDrawTime;
    private void Awake()
    {
        _uiManager.PayLineDetected += DrawPayLines;
    }
    public async void DrawPayLines(List<PayLineComboData> list)
    {
        if (list.Count <= 0)
        {
            PayLineDone();
        }
        else
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_waitToDrawTime / 2));
            await DrawLineWait(list);
        }
    }

    private void PayLineDone()
    {
        _uiManager.OnPayLinesDone();
        _lineRenderer.color = new Color(0, 0, 0, 0);
    }

    private async UniTask DrawLineWait(List<PayLineComboData> list)
    {
        PayLineSO payLineSO;
        List<(int, int)> payLineCellPositions;
        foreach (PayLineComboData data in list)
        {
            payLineSO = data.PayLineSO;
            payLineCellPositions = payLineSO.GetPayLineCellsWithOrder();

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
            await UniTask.Delay(TimeSpan.FromSeconds(_waitToDrawTime));
        }
        PayLineDone();
    }

    private void OnDestroy()
    {
        _uiManager.PayLineDetected -= DrawPayLines;
    }
}
