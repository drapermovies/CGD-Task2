using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugInitBehaviour : MonoBehaviour
{
    public GameObject middle;
    private Vector2 target_pos;
    private float speed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        transform.LookAt(middle.transform.position, middle.transform.position * -1);
        target_pos = new Vector2(middle.transform.position.x, middle.transform.position.y);
        if (Vector2.Distance(transform.position, target_pos) > 4 && (GetComponent<DragScript>().grabbed == false))
        {
            Vector2 pos = Vector2.MoveTowards(transform.position, middle.transform.position, Time.deltaTime * speed);
            transform.position = pos;
        }
        else
        {
            this.GetComponent<BugMovement>().enabled = true;
            this.GetComponent<BugInitBehaviour>().enabled = false;
        }
        
    }
}
