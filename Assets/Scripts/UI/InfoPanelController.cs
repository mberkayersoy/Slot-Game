using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InfoPanelController : MonoBehaviour
{
    [SerializeField] private GameObject _infoPanel;
    [SerializeField] private GameObject[] _windows;
    [SerializeField] private Transform _paylineContainer;
    [SerializeField] private Transform _standardSymbolContainer;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private Button _leftButton;
    private int _currentIndex = 0;
    private float _scaleDuration = 0.5f;
    [Inject] private UIManager _uiManager;
    [Inject] private SlotGameManager _slotGameManager;
    private void Awake()
    {
        _exitButton.onClick.AddListener(Hide);
        _rightButton.onClick.AddListener(() => ChangeWindow(1));
        _leftButton.onClick.AddListener(() => ChangeWindow(-1));
        _uiManager.InfoPanelActivated += Show;
        SetInfos();
    }
    private void ChangeWindow(int direction)
    {
        int newIndex = _currentIndex + direction;

        if (newIndex < 0 || newIndex >= _windows.Length)
        {
            return;
        }

        _currentIndex = newIndex;

        foreach (var window in _windows)
        {
            window.SetActive(false);
        }

        var currentWindow = _windows[_currentIndex];
        currentWindow.SetActive(true);
        currentWindow.transform.localScale = Vector3.zero;
        currentWindow.transform.DOScale(Vector3.one, _scaleDuration);
    }


    private void SetInfos()
    {
        for (int i = 0; i < _slotGameManager.PayLines.Length; i++)
        {
            _paylineContainer.GetChild(i).GetComponent<InfoPanelPayLineDrawer>().SetPayLineColor(_slotGameManager.PayLines[i]);
        }

        for (int i = 0; i < _slotGameManager.SlotSymbols.Length; i++)
        {
            if (_slotGameManager.SlotSymbols[i] is StandardSlotSymbolSO)
            {
                _standardSymbolContainer.GetChild(i).GetComponent<InfoPanelSymbolDataSetter>().SetData(_slotGameManager.SlotSymbols[i] as StandardSlotSymbolSO);

            }
        }
    }

    private void Hide()
    {
        _infoPanel.transform.DOScale(Vector3.zero, _scaleDuration).OnComplete(() =>
        {
            _infoPanel.SetActive(false);
        });

    }
    private void Show()
    {
        _infoPanel.transform.localScale = Vector3.zero;
        _infoPanel.SetActive(true);
        _infoPanel.transform.DOScale(Vector3.one, _scaleDuration);

    }

    private void OnDestroy()
    {
        _exitButton.onClick.RemoveListener(Hide);
        _uiManager.InfoPanelActivated -= Show;
        _rightButton.onClick.RemoveListener(() => ChangeWindow(1));
        _leftButton.onClick.RemoveListener(() => ChangeWindow(-1));
    }
}
