using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InfoPanelSymbolDataSetter : MonoBehaviour
{
    [SerializeField] private Image _symbolImage;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    
    public void SetData(StandardSlotSymbolSO symbolSO)
    {
        _symbolImage.sprite = symbolSO.SymbolSprite;

        string description = "";

        foreach (var item in symbolSO.GetDictionary())
        {
            description += item.Key + " - " + item.Value + "\n";

        }
        _descriptionText.text = description;
    }
}
