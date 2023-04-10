using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlantGrowth : MonoBehaviour
{
    private int currentProgression = 0;
    [SerializeField] int timeBetweenStages;
    [SerializeField] int maxGrowth;
    public float mustWaterTime;
    [SerializeField] float mustWaterDefault;

    private bool plantGrowthStarted;

    // Start is called before the first frame update
    void Start()
    {
        mustWaterTime = mustWaterDefault;
    }

    // Update is called once per frame
    void Update()
    {
        if (plantGrowthStarted)
        {
            mustWaterTime -= Time.deltaTime;
        }

        if (mustWaterTime < 0)
        {
            PlantDie();
        }
    }

    public void Growth ()
    {
        plantGrowthStarted = true;

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
            currentProgression++;
        }
    }

    public void PlantDie()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }

        gameObject.transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                InvokeRepeating("Growth", timeBetweenStages, timeBetweenStages);
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                mustWaterTime = mustWaterDefault;
            }
        }
    }
}
