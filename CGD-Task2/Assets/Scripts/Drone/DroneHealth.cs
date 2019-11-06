using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneHealth : MonoBehaviour
{
    public int health;
    public int numOfBatteries;

    DroneMovement drone;

    public Image[] batteries;
    public Sprite fullBattery;
    public Sprite emptyBattery;

    // Start is called before the first frame update
    void Start()
    {
        drone = GetComponent<DroneMovement>();
        numOfBatteries = drone.healthMax;
    }

    // Update is called once per frame
    void Update()
    {
        health = drone.health;
        if(health > numOfBatteries)
        {
            health = numOfBatteries;
        }

        for (int i =0; i < batteries.Length;   i++)
        {
            if (i < health)
            {
                batteries[i].sprite = fullBattery;
            }
            else
            {
                batteries[i].sprite = emptyBattery;
            }

            if(i < numOfBatteries)
            {
                batteries[i].enabled = true;
            }
            else
            {
                batteries[i].enabled = false;
            }
        }        
    }
}
