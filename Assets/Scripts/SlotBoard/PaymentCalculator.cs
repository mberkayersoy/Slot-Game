using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaymentCalculator
{
    private UIManager _uiManager;

    private int _currentBet = 500;
    private float _currentWin;
    private int _totalCoin;
    private int _betChangeAmount = 1000;
    public PaymentCalculator(SlotGameManager slotGameManager)
    {
        _uiManager = slotGameManager.UiManager;
        _uiManager.BetDecreased += DecreaseBet;
        _uiManager.BetIncreased += IncreaseBet;
    }

    public bool CheckPlayerCanSpin()
    {
        if (_totalCoin < _currentBet)
        {
            Debug.Log("CAN'T SPIN! NOT ENOUGH COIN");
            return false;
        }

        _totalCoin -= _currentBet;
        _uiManager.OnTotalCoinChanged(_totalCoin);
        return true;
    }
    private void DecreaseBet()
    {
        _currentBet -= _betChangeAmount;
        _uiManager.OnCurrentBetChanged(_currentBet);
    }

    private void IncreaseBet()
    {
        _currentBet += _betChangeAmount;
        _uiManager.OnCurrentBetChanged(_currentBet);
    }

    public void CalculatePayment(List<PayLineComboData> matchedPayLines)
    {
        _currentWin = 0;

        foreach (PayLineComboData payLineComboData in matchedPayLines)
        {
            // '1 -' is done because the more GetWeight is, the less it should pay. It is inversely proportional.
            _currentWin += _currentBet * payLineComboData.SymbolSO.GetDictionary()[payLineComboData.Combo] * (1 - (payLineComboData.SymbolSO.GetWeight / 100f));
        }
        _totalCoin += (int)_currentWin;
        _uiManager.OnCurrentWinChanged(_currentWin);
        _uiManager.OnTotalCoinChanged(_totalCoin);
    }
}
