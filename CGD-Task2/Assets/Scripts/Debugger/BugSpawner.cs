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

    private Quaternion rot;

    Vector2 spawn_pos;


    void Start()
    {
        setTimer();
        rot = Quaternion.identity;
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
                spawn_pos.y = Random.Range(GetComponent<Renderer>().bounds.min.y, GetComponent<Renderer>().bounds.max.y);
                spawn_pos.x = transform.position.x;
              break;

            case 1:
                spawn_pos.x = Random.Range(gameObject.transform.GetChild(0).GetComponent<Renderer>().bounds.min.x,
                                           gameObject.transform.GetChild(0).GetComponent<Renderer>().bounds.max.x);
                spawn_pos.y = gameObject.transform.GetChild(0).transform.position.y;
                break;

            case 2:
                spawn_pos.y = Random.Range(gameObject.transform.GetChild(1).GetComponent<Renderer>().bounds.min.y,
                                           gameObject.transform.GetChild(1).GetComponent<Renderer>().bounds.max.y);
                spawn_pos.x = gameObject.transform.GetChild(1).transform.position.x;
                break;

            case 3:
                spawn_pos.x = Random.Range(gameObject.transform.GetChild(2).GetComponent<Renderer>().bounds.min.x,
                                           gameObject.transform.GetChild(2).GetComponent<Renderer>().bounds.max.x);
                spawn_pos.y = gameObject.transform.GetChild(2).transform.position.y;
                break;
        }
        Instantiate(bug_prefab, new Vector2(spawn_pos.x, spawn_pos.y), rot);
    }
}
