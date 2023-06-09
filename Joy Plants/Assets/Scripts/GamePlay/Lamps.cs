using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamps : MonoBehaviour
{
    [SerializeField] float periodTime = 5;
    [SerializeField] float periodTimeDefault = 5;
    [SerializeField] float lightCost = 2;
    public GameObject light;
    private bool inZone;
    public bool lightOn;
    public bool lightOff;
    private MoneyManager moneyManager;

    private void Start()
    {
        moneyManager = GameObject.Find("GlobalManager").GetComponent<MoneyManager>();
    }
    private void Update()
    {
        if (!light.activeInHierarchy)
        {
            lightOn = false;
            lightOff = true;
        }

        if (light.activeInHierarchy)
        {
            lightOn = true;
            lightOff = false;

            periodTime -= Time.deltaTime;

            if (periodTime <= 0)
            {
                moneyManager.UpdateMoneySpent(lightCost);

                periodTime = periodTimeDefault;
            }
        }

        if (inZone)
        {
            LightsOnAndOff();
        }
    }

    private void LightsOnAndOff()
    {
        if (lightOn && Input.GetKeyDown(KeyCode.E))
        {
            light.SetActive(false);
        }

        if (lightOff && Input.GetKeyDown(KeyCode.E))
        {
            light.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("player is in zone");
            inZone = true;
        }
    }
        private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("player is out of zone");
            inZone = false;
        }
    }
}
