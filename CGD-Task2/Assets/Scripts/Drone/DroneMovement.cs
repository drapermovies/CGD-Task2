using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{

    public float moveDir;
    public bool isLeft;
    public bool isCentred;
    public bool isRight;

    public int healthMax;
    public int health;
    public float resetTime;
    private bool isCharging;

    private Vector3 startPos;
    
    Vector3 mouse_offset = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        isCentred = true;
        health = healthMax;
        resetTime = 3;
    }

    private void Update()
    {
        if (health == 0)
            RechargeHealth();
        if (health != 0)
            resetTime = 3;
        
    }


    void FixedUpdate()
    {
        if (health > 0)
        {
            if (Input.GetKeyUp(KeyCode.A) && !isLeft)
            {
                Movement(-moveDir);
            }
            else if (Input.GetKeyUp(KeyCode.D) && !isRight)
            {
                Movement(moveDir);
            }
        }
    }

    void Movement(float direction)
    {        
        // In the centre of the screen
        if (Input.GetKeyUp(KeyCode.A) && isCentred)
        {
            isLeft = true;
            isCentred = false;
            isRight = false;
        }
        else if (Input.GetKeyUp(KeyCode.D) && isCentred)
        {
            isLeft = false;
            isCentred = false;
            isRight = true;            
        }

        // Either on the left or right
        else if (Input.GetKeyUp(KeyCode.A) && !isCentred)
        {
            isLeft = false;
            isCentred = true;
            isRight = false;
        }
        else if (Input.GetKeyUp(KeyCode.D) && !isCentred)
        {
            isLeft = false;
            isCentred = true;
            isRight = false;            
        }
        transform.Translate(direction, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rubbish")
        {
            health--;
        }
    }

    void RechargeHealth()
    {

        isLeft = false;
        isCentred = true;
        isRight = false;
        transform.position = startPos - new Vector3 (0,moveDir);


        ResetTime();
    }

    void ResetTime()
    {
        

        if (resetTime > 0)
        {
            resetTime -= Time.deltaTime;
        }
        else if (resetTime <= 0)
        {
            health = healthMax;
            transform.position = startPos;
        }
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() - mouse_offset;
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mouse_pos = Input.mousePosition;

        mouse_pos.z = Camera.main.WorldToScreenPoint(transform.position).z;

        return Camera.main.ScreenToWorldPoint(mouse_pos);
    }

    private void OnMouseDown()
    {
        mouse_offset = transform.position - GetMouseWorldPos();
    }

}
