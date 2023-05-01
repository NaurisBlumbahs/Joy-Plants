using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public float money;
    public TextMeshProUGUI moneyToText;

    private void Start()
    {
        money = 100f;
        moneyToText.text = money.ToString("F0");
    }
}
