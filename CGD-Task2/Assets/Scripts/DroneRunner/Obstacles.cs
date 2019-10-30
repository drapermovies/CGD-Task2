using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
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
