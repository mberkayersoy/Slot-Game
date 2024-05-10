using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;
using System;
using Cysharp.Threading.Tasks;
using System.Threading;

public class ReelsController : MonoBehaviour
{
    [SerializeField] private float _reelMovementDuration = 0.5f;
    [SerializeField] private float _waitBeforeNextReel = 0.25f;
    [SerializeField] private Ease _easeType = Ease.Linear;

    [Inject] private UIManager _uiManager;
    private float _bottomTargetY;
    private float _aboveTargetY;
    private CancellationTokenSource _cancellationToken = new CancellationTokenSource();
    private bool _flag;
    private void Awake()
    {
        _uiManager.SpinStarted += AnimateReels;
        _uiManager.BoardGenerated += SwitchSymbols;
    }
    private void Start()
    {
        _bottomTargetY = -_uiManager.Reels[0].sizeDelta.y;
        _aboveTargetY = _uiManager.Reels[0].sizeDelta.y;
    }
    public async void SwitchSymbols(BaseSlotSymbolSO[] newBoard, int columnCount, int rowCount)
    {
        try
        {
            await UniTask.WaitUntilValueChanged(this, x => x._flag, cancellationToken: this.GetCancellationTokenOnDestroy());
            for (int columnIndex = 0; columnIndex < SlotGameCommonExtensions.COLUMN_COUNT; columnIndex++)
            {
                for (int rowIndex = 0; rowIndex < SlotGameCommonExtensions.ROW_COUNT; rowIndex++)
                {
                    var child = _uiManager.Reels[columnIndex].GetChild(rowIndex).GetComponent<BaseSymbolComponent>();
                    if (child != null)
                    {
                        child.SetData(SlotGameCommonExtensions.GetCell(newBoard, columnIndex, rowIndex));
                    }
                }
            }
        }
        catch (OperationCanceledException e)
        {
            Debug.Log(e.Message);
        }
    }
    private async void AnimateReels()
    {
        foreach (var reel in _uiManager.Reels)
        {
            await reel.DOAnchorPos(new Vector2(reel.anchoredPosition.x, _bottomTargetY), _reelMovementDuration)
                .SetEase(_easeType).OnComplete(() =>
                {
                    reel.anchoredPosition = new Vector2(reel.anchoredPosition.x, _aboveTargetY);
                });
        }
        _flag = !_flag;
        foreach (var reel in _uiManager.Reels)
        {
            await reel.DOAnchorPos(new Vector2(reel.anchoredPosition.x, 0f), _reelMovementDuration).SetEase(_easeType);
        }
        _uiManager.OnSpinEnded();
    }

    private void OnDestroy()
    {
        _uiManager.SpinStarted -= AnimateReels;
        _uiManager.BoardGenerated -= SwitchSymbols;

    }

}
