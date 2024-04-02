using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;
using System;

public class ReelMovementController : MonoBehaviour
{
    [SerializeField] private float _reelMovementDuration = 0.5f;
    [SerializeField] private float _waitBeforeNextReel = 0.25f;
    [SerializeField] private Ease _easeType = Ease.Linear;
    [Inject] private UIManager _uiManager;

    private void Awake()
    {
        _uiManager.SpinStarted += StartMovement;
        _uiManager.BoardGenerated += SetSymbols;

        foreach (var reel in _uiManager.Reels)
        {
            reel.anchoredPosition = new Vector2(reel.anchoredPosition.x, reel.sizeDelta.y);
        }
    }

    private void SetSymbols(BaseSlotSymbolSO[] newBoard, int columnCount, int rowCount)
    {

        for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
        {
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                var child = _uiManager.Reels[columnIndex].GetChild(rowIndex).GetComponent<BaseSymbolComponent>();
                if (child != null)
                {
                    child.SetData(GetCell(newBoard, columnIndex, rowIndex, rowCount));
                }
            }
        }
    }
    public BaseSlotSymbolSO GetCell(BaseSlotSymbolSO[] newBoard, int column, int row, int rowCount = 3)
    {
        int index = column * rowCount + row;
        return newBoard[index];
    }

    private void StartMovement()
    {
        StartCoroutine(ShowSymbols());
    }

    private IEnumerator ShowSymbols()
    {
        foreach (var reel in _uiManager.Reels)
        {
            reel.DOAnchorPos(new Vector2(reel.anchoredPosition.x, 0f), _reelMovementDuration).SetEase(_easeType);
            yield return new WaitForSeconds(_waitBeforeNextReel);
        }

        _uiManager.OnSpinEnded();
    }

    private void OnDestroy()
    {
        _uiManager.SpinStarted -= StartMovement;
        _uiManager.BoardGenerated -= SetSymbols;
    }

}
