using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    private Transform[] spawn_points;
    public GameObject bug_prefab;

    private float timer;
    private float time_max = 7.0f;
    private float time_min = 2.0f;
    private float spawn_time;

    private int spawn_number;
    private int spawn_min = 0;
    private int spawn_max = 4;
    public GameObject[] spawns;


    void Start()
    {
        spawns = new GameObject[4];
        setTimer();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawn_time)
        {
            spawnBug();
            setTimer();
        }
    }

    void setTimer()
    {
        spawn_time = Random.Range(time_min, time_max);
        timer = 0.0f;
    }

    void spawnBug()
    {
        spawn_number = Random.Range(spawn_min, spawn_max);
        
        switch(spawn_number)
        {
            case 0:
                Instantiate(bug_prefab, transform.position, Quaternion.identity, transform);
              break;

            case 1:
                Instantiate(bug_prefab, gameObject.transform.GetChild(0).transform.position, Quaternion.identity, transform);
                break;

            case 2:
                Instantiate(bug_prefab, gameObject.transform.GetChild(1).transform.position, Quaternion.identity, transform);
                break;

            case 3:
                Instantiate(bug_prefab, gameObject.transform.GetChild(2).transform.position, Quaternion.identity, transform);
                break;
        }
        Debug.Log(gameObject.transform.GetChild(2).name);
    }
}
