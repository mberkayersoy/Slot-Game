using System;
using UnityEngine;
using Zenject;

public class SlotGameManager : MonoBehaviour
{
    [SerializeField] private BaseSlotSymbolSO[] _slotSymbols;
    [SerializeField] private PayLineSO[] _payLines;

    [Inject] private UIManager _uiManager;
    private StateMachine _stateMachine;
    private SlotBoardGenerator _slotBoardManager;
    private PaymentCalculator _paymentCalculator;
    public UIManager UiManager { get => _uiManager; }
    public SlotBoardGenerator SlotBoardManager { get => _slotBoardManager; private set => _slotBoardManager = value; }
    public PayLineSO[] PayLines { get => _payLines; private set => _payLines = value; }
    public BaseSlotSymbolSO[] SlotSymbols { get => _slotSymbols; set => _slotSymbols = value; }
    public PaymentCalculator PaymentCalculator { get => _paymentCalculator; private set => _paymentCalculator = value; }

    private void Awake()
    {
        _slotBoardManager = new SlotBoardGenerator(_slotSymbols);
        _stateMachine = new StateMachine(this);
        _paymentCalculator = new PaymentCalculator(this);
    }
}
