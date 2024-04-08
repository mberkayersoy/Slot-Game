using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class FeedbackBetUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentBetText;
    [SerializeField] private TextMeshProUGUI _currentWinText;
    [SerializeField] private TextMeshProUGUI _totalCoinText;
    [SerializeField] private Button _decreaseBetButton;
    [SerializeField] private Button _increaseBetButton;
    [Inject] private UIManager _uiManager;

    private void Awake()
    {
        _decreaseBetButton.onClick.AddListener(OnClickDecreaseBetButton);
        _increaseBetButton.onClick.AddListener(OnClickIncreaseBetButton);
        _uiManager.CurrentBetChanged += DisplayCurrentBet;
        _uiManager.CurrentWinChanged += DisplayCurrentWin;
        _uiManager.TotalCoinChanged += DisplayTotalCoin;
    }

    private void DisplayTotalCoin(int totalCoin)
    {
        _totalCoinText.text = "Total Coin: " + totalCoin.ToString();
    }

    private void DisplayCurrentWin(float currentWin)
    {
        _currentWinText.text = "Win: " + currentWin.ToString();
    }

    private void DisplayCurrentBet(int currentBet)
    {
        _currentBetText.text = "Bet: " + currentBet.ToString();
    }

    private void OnClickDecreaseBetButton()
    {
        if (_uiManager.IsSpining) return;
        _uiManager.OnBetDecreased();
    }

    private void OnClickIncreaseBetButton()
    {
        if (_uiManager.IsSpining) return;
        _uiManager.OnBetIncreased();
    }

    private void OnDestroy()
    {
        _decreaseBetButton.onClick.RemoveListener(OnClickDecreaseBetButton);
        _increaseBetButton.onClick.RemoveListener(OnClickIncreaseBetButton);
        _uiManager.CurrentBetChanged -= DisplayCurrentBet;
        _uiManager.CurrentWinChanged -= DisplayCurrentWin;
        _uiManager.TotalCoinChanged -= DisplayTotalCoin;
    }
}
