using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardUI : MonoBehaviour
{
    [SerializeField] private float _columnMoveDuration = 0.5f;
    [SerializeField] private Ease _columnEaseMode = Ease.Linear;
    [SerializeField] private ReelMovementController[] _uiReels;

    private void Awake()
    {
        _uiReels = GetComponentsInChildren<ReelMovementController>();
    }
    //void Start()
    //{
    //    StartCoroutine(ActivateMovement());
    //}

    //private IEnumerator ActivateMovement()
    //{
    //    foreach (var reel in _uiReels)
    //    {
    //        reel.Move(_columnMoveDuration, _columnEaseMode);
    //        yield return new WaitForSeconds(0.25f);
    //    }

    //    foreach (var reel in _uiReels)
    //    {
    //        reel.Move(_columnMoveDuration, _columnEaseMode);
    //        yield return new WaitForSeconds(0.25f);
    //    }
    //}
}
