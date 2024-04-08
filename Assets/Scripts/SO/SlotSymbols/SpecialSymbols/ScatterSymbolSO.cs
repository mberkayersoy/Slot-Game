using MyExtensions.EventChannels;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "ScatterSymbolSO", menuName = "Slot Symbol/Scatter Symbol")]
public class ScatterSymbolSO : SpecialSlotSymbolSO
{
    [SerializeField] private ValueKeyPair<int, int>[] _paymentPairs;

    [SerializeField] private Dictionary<int, int> _paymentDic = new Dictionary<int, int>();
    public Dictionary<int, int> PaymentDic { get => _paymentDic; private set => _paymentDic = value; }

    [SerializeField] private IntEventChannelSO _giveFreeSpin;
    public void SetDictionary()
    {
        foreach (var item in _paymentPairs)
        {
            PaymentDic.Add(item.Key, item.Value);
        }
    }

    public void ApplySymbolFeature(int scatterCount)
    {
        SetDictionary();
        _giveFreeSpin.RaiseEvent(_paymentDic[scatterCount]);
    }
}