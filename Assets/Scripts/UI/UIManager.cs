using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Action SpinClicked;
    public Action SpinStarted;
    public Action SpinEnded;
    public Action<BaseSlotSymbolSO[], int , int> BoardGenerated;
    public Action<List<PayLineComboData>> PayLineDetected;
    [SerializeField] private RectTransform[] _reels;
    [SerializeField] private Button _spinButton;
    [SerializeField] private Toggle _autoSpinToggle;
    private bool _autoSpin;

    public bool AutoSpin { get => _autoSpin; private set => _autoSpin = value; }
    public RectTransform[] Reels { get => _reels; private set => _reels = value; }

    private void Awake()
    {
        _spinButton.onClick.AddListener(OnClickedSpinButton);
        _autoSpinToggle.onValueChanged.AddListener(SpinMethodChanged);
    }

    private void SpinMethodChanged(bool isAutoSpin)
    {
        AutoSpin = isAutoSpin;
    }

    private void OnClickedSpinButton()
    {
        SpinClicked?.Invoke();
    }
    public void OnSpinStarted()
    {
        SpinStarted?.Invoke();
    }
    public void OnBoardGenerated(BaseSlotSymbolSO[] newBoard, int column, int row)
    {
        BoardGenerated?.Invoke(newBoard, column, row);
    }
    public void OnPayLineDetected(List<PayLineComboData> payLines)
    {
        PayLineDetected?.Invoke(payLines);
    }

    public void OnSpinEnded()
    {
        SpinEnded?.Invoke();
    }
    private void OnDestroy()
    {
        _spinButton.onClick.RemoveListener(OnSpinStarted);
        _autoSpinToggle.onValueChanged.RemoveListener(SpinMethodChanged);
    }
}
