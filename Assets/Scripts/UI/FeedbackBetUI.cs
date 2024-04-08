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
    [SerializeField] private FreeSpinDisplayComponent _freeSpinPanel;
    [Inject] private UIManager _uiManager;

    private void Awake()
    {
        _decreaseBetButton.onClick.AddListener(OnClickDecreaseBetButton);
        _increaseBetButton.onClick.AddListener(OnClickIncreaseBetButton);
        _uiManager.CurrentBetChanged += DisplayCurrentBet;
        _uiManager.CurrentWinChanged += DisplayCurrentWin;
        _uiManager.TotalCoinChanged += DisplayTotalCoin;
        _uiManager.FreeSpinCountChanged += DisplayFreeSpin;
    }
    
    private void DisplayFreeSpin(int freeSpinCount)
    {
        _freeSpinPanel.SetText(freeSpinCount);

        if (freeSpinCount > 0)
        {
            _freeSpinPanel.gameObject.SetActive(true);
        }
        else
        {
            _freeSpinPanel.gameObject.SetActive(false);
        }
    }
    private void DisplayTotalCoin(int totalCoin)
    {
        _totalCoinText.text = "Total Coin: " + ShortenNumber(totalCoin);
    }

    private void DisplayCurrentWin(float currentWin)
    {
        _currentWinText.text = "Win: " + ShortenNumber(currentWin);
    }

    private void DisplayCurrentBet(int currentBet)
    {
        _currentBetText.text = "Bet: " + ShortenNumber(currentBet);
    }

    private void OnClickDecreaseBetButton()
    {
        if (_uiManager.IsSpining) return;
        _uiManager.OnBetDecreased();
    }
    private string ShortenNumber(float number)
    {
        return number >= 1000000000 ? (number / 1000000000f).ToString("F2") + "b" :
               number >= 1000000 ? (number / 1000000f).ToString("F2") + "m" :
               number >= 1000 ? (number / 1000f).ToString("F2") + "k" :
               number.ToString();
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
        _uiManager.FreeSpinCountChanged -= DisplayFreeSpin;
    }
}
