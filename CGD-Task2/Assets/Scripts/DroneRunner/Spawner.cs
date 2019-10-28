using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawners;
    public GameObject[] prefabs;
    private float max_timer = 1.0f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = max_timer;
    }

    // Update is called once per frame
    void Update()
    {
        //when timer reaches 0 spawn an instance of obstacle or item at randomised spawner pos;
        timer -= Time.deltaTime;
        if(timer <= 0.0f)
        {
            timer = max_timer;

            Vector3 position = spawners[Random.Range(0, 3)].transform.position;
            Instantiate(prefabs[0], position, Quaternion.identity);
        }
    }
}
