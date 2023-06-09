using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewPlantGrowth : MonoBehaviour
{
    [SerializeField] float timeBetweenStages;
    [SerializeField] float timeBetweenStagesDefault;
    [SerializeField] int maxGrowth;
    [SerializeField] float mustWaterTime;
    [SerializeField] float mustWaterDefault;
    [SerializeField] float mustWaterGrown;
    [SerializeField] float sellValue;
    [SerializeField] float plantValue;
    [SerializeField] float lightMultiplier;
    public TextMeshProUGUI timeToGrow;
    public TextMeshProUGUI timeToWater;
    public Lamps lampManager;
    private int currentProgression = 0;
    private bool canHarvest;
    private bool isDead = false;
    private bool plantGrowthStarted;
    private bool canPlantAgain = true;
    private bool inZone;
    private MoneyManager moneyManager;

    void Start()
    {
        mustWaterTime = mustWaterDefault;
        moneyManager = GameObject.Find("GlobalManager").GetComponent<MoneyManager>();
    }

    void Update()
    {
        timeToWater.text = mustWaterTime.ToString("F0");
        timeToGrow.text = timeBetweenStages.ToString("F0");

        if (plantGrowthStarted == true && isDead == false && canHarvest == false)
        {
            mustWaterTime -= Time.deltaTime;

            if (timeBetweenStages <= 0f)
            {
                Growth();
                timeBetweenStages = timeBetweenStagesDefault;
            }
        }
        if (canHarvest)
        {
            mustWaterTime -= Time.deltaTime;
            timeToGrow.text = "Grown";
        }
        if (plantGrowthStarted)
        {
            if (lampManager.lightOn == false)
            {
                timeBetweenStages -= Time.deltaTime;
                timeToGrow.color = Color.white;
            }

            if (lampManager.lightOn == true)
            {
                timeBetweenStages -= lightMultiplier * Time.deltaTime;
                timeToGrow.color = Color.yellow;
            }
        }
        if (mustWaterTime < 0)
        {
            PlantDie();
        }
        if (isDead)
        {
            timeToGrow.text = "Dead";
            timeToWater.text = "Dead";
        }
        if (inZone)
        {
            InteractWithPot();
        }
    }

    public void Growth ()
    {
        plantGrowthStarted = true;
        canPlantAgain = false;

        if (currentProgression != maxGrowth)
        {
            gameObject.transform.GetChild(currentProgression).gameObject.SetActive(true);
        }
        if (currentProgression > 0 && currentProgression < maxGrowth)
        {
            gameObject.transform.GetChild(currentProgression - 1).gameObject.SetActive(false);
        }
        if (currentProgression < maxGrowth)
        {
            Debug.Log(gameObject.name + " has reached phase " + gameObject.transform.GetChild(currentProgression).name);

            currentProgression++;      
        }
        if (transform.GetChild(3).gameObject.active)
        {
            canHarvest = true;
            plantGrowthStarted = false;
        }
        if (canHarvest)
        {
            PlantGrown();
        }
    }
    
    private void PlantGrown()
    {
        //CancelInvoke("Growth");
        Debug.Log(gameObject.name + " has grown");
        mustWaterTime = mustWaterGrown;
    }
    public void PlantDie()
    {
        for (int i = 0; i < 3; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }

        gameObject.transform.GetChild(4).gameObject.SetActive(true);

        //CancelInvoke("Growth");

        mustWaterTime = 0;

        plantGrowthStarted = false;
        isDead = true;
        canHarvest = false;

        Debug.Log(gameObject.name + " has died");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inZone = true;
        }
    }

        public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inZone = false;
        }
    }

    private void InteractWithPot()
    {
        if (canPlantAgain && Input.GetKeyDown(KeyCode.P))
        {
            Plant();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Water();
        }
        if (canHarvest && Input.GetKeyDown(KeyCode.H))
        {
            Harvest();
        }
        if (isDead && Input.GetKeyDown(KeyCode.J))
        {
            Discard();
        }
    }

    private void Plant()
    {
        canPlantAgain = false;
        Growth();
        //InvokeRepeating("Growth", 0, timeBetweenStages);
        moneyManager.UpdateMoneySpent(plantValue);
        Debug.Log(gameObject.name + " was planted");
    }
    private void Water()
    {
        mustWaterTime = mustWaterDefault;

        if (canHarvest)
        {
            mustWaterTime = mustWaterGrown;
        }

        Debug.Log(gameObject.name + " was watered");
    }
    private void Harvest()
    {
        canHarvest = false;
        canPlantAgain = true;
        transform.GetChild(3).gameObject.SetActive(false);
        currentProgression = 0;
        timeBetweenStages = timeBetweenStagesDefault;
        mustWaterTime = mustWaterDefault;
        moneyManager.UpdateMoneyEarned(sellValue);
        Debug.Log(gameObject.name + " was harvested");
    }

    private void Discard()
    {
        isDead = false;
        canPlantAgain = true;
        transform.GetChild(4).gameObject.SetActive(false);
        currentProgression = 0;
        timeBetweenStages = timeBetweenStagesDefault;
        mustWaterTime = mustWaterDefault;
        Debug.Log(gameObject.name + " was thrown out");
    }
}
