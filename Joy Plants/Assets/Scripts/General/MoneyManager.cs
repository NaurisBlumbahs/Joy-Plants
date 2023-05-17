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
    }
    private void Update()
    {
        moneyToText.text = money.ToString("F0");
    }
    public void UpdateMoneyEarned(float moneyEarned)
    {
        money += moneyEarned;
    }
    public void UpdateMoneySpent(float moneySpent)
    {
        money -= moneySpent;
    }
}
