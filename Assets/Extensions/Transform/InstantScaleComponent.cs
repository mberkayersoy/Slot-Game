using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantScaleComponent : MonoBehaviour
{
    [SerializeField] private float _targetScale = 1f;
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private Ease _easeType = Ease.Linear;

    private float _initialScale;
    private Tween _tween;
    private bool _isCanvasElement = false;
    private RectTransform _rectTransform;
    private void Awake()
    {
        _initialScale = transform.localScale.x;
        
        _isCanvasElement = TryGetComponent(out _rectTransform);
    }
    public void SetScale(float duration = 0)
    {
        if (duration == 0) duration = _duration;
        if (_isCanvasElement)
        {
            _tween = _rectTransform.DOScale(_targetScale * Vector3.one, duration).SetEase(_easeType).OnComplete(() =>
            {
                _rectTransform.DOScale(_initialScale * Vector3.one, duration).SetEase(_easeType);
            });
        }
        else
        {
            _tween = transform.DOScale(_targetScale, duration).SetEase(_easeType).OnComplete(() =>
            {
                transform.DOScale(_initialScale, duration).SetEase(_easeType);
            });
        }

    }
    private void OnDisable()
    {
        _tween.Kill();
    }

    private void OnDestroy()
    {
        _tween.Kill();
    }
}
