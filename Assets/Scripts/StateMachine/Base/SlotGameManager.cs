using MyExtensions.EventChannels;
using System;
using UnityEditor;
using UnityEngine;
using Zenject;

public class SlotGameManager : MonoBehaviour
{
    [SerializeField] private BaseSlotSymbolSO[] _slotSymbols;
    [SerializeField] private PayLineSO[] _payLines;
    [SerializeField] private IntEventChannelSO _addFreeSpin;
    [SerializeField] private bool _calculateWithDFS;

    [Inject] private UIManager _uiManager;
    private StateMachine _stateMachine;
    private SlotBoardGenerator _slotBoardManager;
    private PaymentCalculator _paymentCalculator;
    public UIManager UiManager { get => _uiManager; }
    public SlotBoardGenerator SlotBoardManager { get => _slotBoardManager; private set => _slotBoardManager = value; }
    public PayLineSO[] PayLines { get => _payLines; private set => _payLines = value; }
    public BaseSlotSymbolSO[] SlotSymbols { get => _slotSymbols; set => _slotSymbols = value; }
    public PaymentCalculator PaymentCalculator { get => _paymentCalculator; private set => _paymentCalculator = value; }
    public bool CalculateWithDFS { get => _calculateWithDFS; private set => _calculateWithDFS = value; }

    private void Awake()
    {
        _addFreeSpin.OnEventRaised += AddFreeSpin;
        _uiManager.CoinAdded += AddTestCoin;
        _uiManager.GameExited += QuitGame;
        _slotBoardManager = new SlotBoardGenerator(_slotSymbols);
        _stateMachine = new StateMachine(this);
        _paymentCalculator = new PaymentCalculator(this);
    }
    private void AddTestCoin()
    {
        _paymentCalculator.AddCoin();
    }
    private void AddFreeSpin(int freeSpinCount)
    {
        _paymentCalculator.AddFreeSpin(freeSpinCount);
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
    private void OnDestroy()
    {
        _paymentCalculator.SaveTotalCoinData();
        _addFreeSpin.OnEventRaised -= AddFreeSpin;
        _uiManager.GameExited -= QuitGame;
    }
}
