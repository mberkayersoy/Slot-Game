using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaymentCalculator
{
    private UIManager _uiManager;

    private int _currentBet = 1000;
    private float _currentWin;
    private int _totalCoin;
    private int _betChangeAmount = 1000;
    private int _currentFreeSpin;
    private float _changeAmountPercentage = 0.01f;

    private const string TOTAL_COIN_DATA_PATH = "PaymentCalculator_TotalCoin";
    public PaymentCalculator(SlotGameManager slotGameManager)
    {
        _uiManager = slotGameManager.UiManager;
        _uiManager.BetDecreased += DecreaseBet;
        _uiManager.BetIncreased += IncreaseBet;
        _uiManager.MaxBetChanged += SetMaxBet;
        _uiManager.MinBetChanged += SetMinBet;

        _totalCoin = SaveLoadManager<int>.Load(TOTAL_COIN_DATA_PATH);
        if (_totalCoin == default) { AddCoin(); }

        _uiManager.OnTotalCoinChanged(_totalCoin);
        _uiManager.OnCurrentBetChanged(_currentBet);
    }

    public void SaveTotalCoinData()
    {
        SaveLoadManager<int>.Save(_totalCoin, TOTAL_COIN_DATA_PATH);
    }
    public bool CheckPlayerCanSpin()
    {
        if (_currentFreeSpin > 0)
        {
            _currentFreeSpin--;
            _uiManager.OnFreeSpinCountChanged(_currentFreeSpin);
            _uiManager.OnCurrentBetChanged(_currentBet);

            return true;
        }

        if (_totalCoin < _currentBet)
        {
            Debug.Log("CAN'T SPIN! NOT ENOUGH COIN");
            return false;
        }

        _totalCoin -= _currentBet;
        _uiManager.OnTotalCoinChanged(_totalCoin);
        return true;
    }

    private void SetMaxBet()
    {
        if (_totalCoin <= 0)
        {
            _currentBet = _betChangeAmount;
        }
        else
        {
            _currentBet = _totalCoin;
        }

        _uiManager.OnCurrentBetChanged(_currentBet);
    }
    private void SetMinBet()
    {
        _currentBet = _betChangeAmount;
        _uiManager.OnCurrentBetChanged(_currentBet);
    }
    public void AddFreeSpin(int freeSpinAmount)
    {
        _currentFreeSpin += freeSpinAmount;
        _uiManager.OnFreeSpinCountChanged(_currentFreeSpin);
    }
    private void DecreaseBet()
    {
        _currentBet -= Mathf.RoundToInt(_totalCoin * _changeAmountPercentage);
        _currentBet = Mathf.Clamp(_currentBet, _betChangeAmount, _totalCoin);

        if (_currentBet <= 0)
        {
            _currentBet = _betChangeAmount;
        }
        _uiManager.OnCurrentBetChanged(_currentBet);
    }

    private void IncreaseBet()
    {
        _currentBet += Mathf.RoundToInt(_totalCoin * _changeAmountPercentage);
        _currentBet = Mathf.Clamp(_currentBet, _betChangeAmount, _totalCoin);

        if (_currentBet <= 0)
        {
            _currentBet = _betChangeAmount;
        }
        _uiManager.OnCurrentBetChanged(_currentBet);
    }

    public void AddCoin()
    {
        _totalCoin += 10000;
        _uiManager.OnTotalCoinChanged(_totalCoin);
    }
    public void CalculatePayment(List<PayLineComboData> matchedPayLines)
    {
        _currentWin = 0;

        foreach (PayLineComboData payLineComboData in matchedPayLines)
        {
            // '(1 - (payLineComboData.SymbolSO.GetWeight / 100f)' is done because the more GetWeight is, the less it should pay. It is inversely proportional.
            _currentWin += _currentBet * payLineComboData.SymbolSO.GetDictionary()[payLineComboData.Combo] * (1 - (payLineComboData.SymbolSO.GetWeight / 100f));
        }
        _totalCoin += (int)_currentWin;
        _uiManager.OnCurrentWinChanged(_currentWin);
        _uiManager.OnTotalCoinChanged(_totalCoin);
    }
}
