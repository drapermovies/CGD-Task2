using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public float speed;
    public float max_speed;
    // Start is called before the first frame update
    void Start()
    {
        max_speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(StressManager.GetBurnout())
        {
            speed = max_speed * 1.6f;
        }
        else
        {
            speed = max_speed;
        }
        //move downwards and delete if reach the bottom
        Vector3 pos = gameObject.transform.position;

        pos.y -= speed * Time.deltaTime;
        gameObject.transform.position = pos;

        if(pos.y <= -6)
        {
            Destroy(gameObject);
        }        
    }
}
