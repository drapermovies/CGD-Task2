using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugInitBehaviour : MonoBehaviour
{
    public GameObject middle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(middle.transform.position);

        if (Vector3.Distance(transform.position, middle.transform.position) > 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, middle.transform.position, Time.deltaTime);
        }
        
    }
}
