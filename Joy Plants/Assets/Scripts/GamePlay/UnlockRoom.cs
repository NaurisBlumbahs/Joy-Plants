using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockRoom : MonoBehaviour
{
    [SerializeField] float roomCost = 500;
    public GameObject door;
    private MoneyManager moneyManager;
    private bool inZone;

    private void Start()
    {
        moneyManager = GameObject.Find("GlobalManager").GetComponent<MoneyManager>();
    }

    private void Update()
    {
        if (inZone && Input.GetKeyDown(KeyCode.E))
        {
            if (moneyManager.money >= roomCost)
            {
                UnlockNewRoom();
            }
            else
            {
                return;
            }
        }
    }

    private void UnlockNewRoom()
    {
        moneyManager.UpdateMoneySpent(roomCost);
        door.SetActive(false);
        inZone = false;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"));
        {
            inZone = true;
        }
    }
}
