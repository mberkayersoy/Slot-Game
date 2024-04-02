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
        foreach (var item in _paymentPairs)
        {
            PaymentDic.Add(item.Key, item.Value);
        }
    }

    public Dictionary<int, int> PaymentDic { get => _paymentDic; private set => _paymentDic = value; }
}

[System.Serializable]
public class ValueKeyPair<TKey, TValue> 
{
    public TKey Key;
    public TValue Value;
}
