using System;
using UnityEngine;
using Zenject;

public class SlotGameManager : MonoBehaviour
{
    [SerializeField] private BaseSlotSymbolSO[] _slotSymbols;
    [SerializeField] private PayLineSO[] _payLines;

    [Inject] private UIManager _uiManager;
    private StateMachine _stateMachine;
    private SlotBoardManager _slotBoardManager;
    public UIManager UiManager { get => _uiManager; }
    public SlotBoardManager SlotBoardManager { get => _slotBoardManager; private set => _slotBoardManager = value; }
    public PayLineSO[] PayLines { get => _payLines; private set => _payLines = value; }

    private void Awake()
    {
        _slotBoardManager = new SlotBoardManager(_slotSymbols);
        _stateMachine = new StateMachine(this);
    }
}
