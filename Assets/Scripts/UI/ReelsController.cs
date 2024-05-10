using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;
using System;

public class ReelsController : MonoBehaviour
{
    [SerializeField] private float _reelMovementDuration = 0.5f;
    [SerializeField] private float _waitBeforeNextReel = 0.25f;
    [SerializeField] private Ease _easeType = Ease.Linear;
    [SerializeField] private float _bottomTargetY;
    [SerializeField] private float _aboveTargetY;
    [Inject] private UIManager _uiManager;

    private void Awake()
    {
        _uiManager.SpinStarted += StartMovement;
        _uiManager.BoardGenerated += DisplaySymbols;
        _bottomTargetY = -_uiManager.Reels[0].sizeDelta.y;
        _aboveTargetY = _uiManager.Reels[0].sizeDelta.y;
    }

    private void DisplaySymbols(BaseSlotSymbolSO[] newBoard, int columnCount, int rowCount)
    {
        StartCoroutine(WaitForDisplay(newBoard, columnCount, rowCount));
    }
    private IEnumerator WaitForDisplay(BaseSlotSymbolSO[] newBoard, int columnCount, int rowCount)
    {
        yield return new WaitForSeconds(_waitBeforeNextReel * columnCount);
        for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
        {
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                var child = _uiManager.Reels[columnIndex].GetChild(rowIndex).GetComponent<BaseSymbolComponent>();
                if (child != null)
                {
                    child.SetData(SlotGameCommonExtensions.GetCell(newBoard, columnIndex, rowIndex));
                }
            }
        }
    }
    private void StartMovement()
    {
        StartCoroutine(AnimateSymbols());
    }

    private IEnumerator AnimateSymbols()
    {
        foreach (var reel in _uiManager.Reels)
        {
            reel.DOAnchorPos(new Vector2(reel.anchoredPosition.x, _bottomTargetY), _reelMovementDuration)
                .SetEase(_easeType).OnComplete(() =>
                {
                    reel.anchoredPosition = new Vector2(reel.anchoredPosition.x, _aboveTargetY);
                });

            yield return new WaitForSeconds(_waitBeforeNextReel);
        }
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
        _uiManager.BoardGenerated -= DisplaySymbols;
    }

}
