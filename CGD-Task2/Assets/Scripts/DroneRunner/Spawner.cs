using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawners;
    public GameObject[] prefabs;

    private float max_timer = 1.0f;
    private float timer;

    private int pickup_counter;
    private int pickup_rate = 5;

    private int last_spawner = 0;

    // Start is called before the first frame update
    void Start()
    {
        timer = max_timer;
        pickup_counter = pickup_rate;
    }

    // Update is called once per frame
    void Update()
    {
        //when timer reaches 0 spawn an instance of obstacle or item at randomised spawner pos;
        timer -= Time.deltaTime;
        if(timer <= 0.0f)
        {
            timer = max_timer;
            last_spawner = chooseSpawner(last_spawner);
            Vector3 position = spawners[last_spawner].transform.position;

            GameObject spawn;
            if(pickup_counter == 0)
            {
                pickup_counter = pickup_rate;
                spawn = prefabs[1];
            }
            else
            {
                spawn = prefabs[0];
            }

            Instantiate(spawn, position, Quaternion.identity);
            pickup_counter--;
        }
    }

    int chooseSpawner(int previous)
    {
        int new_spawner = Random.Range(0, 3);

        //if new spawner is the same as previous
        //change to one of the other 2
        if(new_spawner == previous)
        {
            if(new_spawner == 2)
            {
                new_spawner--;
            }
            else if(new_spawner == 0)
            {
               new_spawner++;
            }
            else
            {
            if (Random.Range(0, 2) == 0)
                {
                    new_spawner++;
                }
                else
                {
                    new_spawner--;
                }
            }            
        }
        return new_spawner;
    }
}