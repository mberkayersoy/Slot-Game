using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Action SpinClicked;
    public Action SpinStarted;
    public Action SpinEnded;
    public Action PayLinesDone;
    public Action InfoPanelActivated;
    public Action BetIncreased;
    public Action BetDecreased;
    public Action<int> CurrentBetChanged;
    public Action<float> CurrentWinChanged;
    public Action<int> TotalCoinChanged;
    public Action<BaseSlotSymbolSO[], int , int> BoardGenerated;
    public Action<List<PayLineComboData>> PayLineDetected;
    [SerializeField] private RectTransform[] _reels;
    [SerializeField] private Button _spinButton;
    [SerializeField] private Button _infoButton;
    [SerializeField] private Toggle _autoSpinToggle;
    private bool _autoSpin;
    private bool _isSpining;

    public bool AutoSpin { get => _autoSpin; private set => _autoSpin = value; }
    public RectTransform[] Reels { get => _reels; private set => _reels = value; }
    public bool IsSpining { get => _isSpining; private set => _isSpining = value; }

    private void Awake()
    {
        _spinButton.onClick.AddListener(OnClickedSpinButton);
        _autoSpinToggle.onValueChanged.AddListener(SpinMethodChanged);
        _infoButton.onClick.AddListener(OnInfoPanelActivated);
    }

    private void OnInfoPanelActivated()
    {
        InfoPanelActivated?.Invoke();
    }

    private void SpinMethodChanged(bool isAutoSpin)
    {
        AutoSpin = isAutoSpin;
    }
    private void OnClickedSpinButton()
    {
        SpinClicked?.Invoke();
    }
    public void OnPayLinesDone()
    {
        PayLinesDone?.Invoke();
    }
    public void OnSpinStarted()
    {
        _isSpining = true;
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
        _isSpining = false;
        SpinEnded?.Invoke();
    }
    public void OnBetDecreased()
    {
        BetDecreased?.Invoke();
    }

    public void OnBetIncreased()
    {
        BetIncreased?.Invoke();
    }

    public void OnCurrentBetChanged(int currentBet)
    {
        CurrentBetChanged?.Invoke(currentBet);
    }
    public void OnCurrentWinChanged(float currentWin)
    {
        CurrentWinChanged?.Invoke(currentWin);
    }
    public void OnTotalCoinChanged(int totalCoin)
    {
        TotalCoinChanged?.Invoke(totalCoin);
    }
    private void OnDestroy()
    {
        _spinButton.onClick.RemoveListener(OnSpinStarted);
        _autoSpinToggle.onValueChanged.RemoveListener(SpinMethodChanged);
    }
}
