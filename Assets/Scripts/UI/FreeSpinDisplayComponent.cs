using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FreeSpinDisplayComponent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _freeSpinText;

    public void SetText(int freeSpinCount)
    {
        _freeSpinText.text = "Free Spin \n" + freeSpinCount.ToString();
    }
}
