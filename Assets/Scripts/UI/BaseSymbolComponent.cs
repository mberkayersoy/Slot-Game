using MyExtensions.ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseSymbolComponent : PoolableComponent
{
    [SerializeField] private BaseSlotSymbolSO _symbolSO;
    private Image _symbolImage;
    public BaseSlotSymbolSO SymbolSO { get => _symbolSO; set => _symbolSO = value; }

    private void Awake()
    {
        _symbolImage = GetComponent<Image>();
        SetData(_symbolSO);
    }
    public void SetData(BaseSlotSymbolSO symbol)
    {
        _symbolSO = symbol;
        _symbolImage.sprite = _symbolSO.SymbolSprite;

    }
}
