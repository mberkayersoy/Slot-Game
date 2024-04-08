using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "StandardSlotSymbolSO",menuName = "Slot Symbol/Standard Symbol")]
public class StandardSlotSymbolSO : BaseSlotSymbolSO
{
    [SerializeField] private ValueKeyPair<int,int>[] _paymentPairs;
    [SerializeField] private Dictionary<int,int> _paymentDic = new Dictionary<int, int> ();

    public void SetDictionary()
    {
        _paymentDic.Clear();
        foreach (var item in _paymentPairs)
        {
            _paymentDic.Add(item.Key, item.Value);
        }
    }

    public Dictionary<int, int> GetDictionary()
    {
        SetDictionary();
        return _paymentDic;
    }
}

[System.Serializable]
public class ValueKeyPair<TKey, TValue> 
{
    public TKey Key;
    public TValue Value;
}
