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

    private float invincibility_timer = 1.5f;
    private bool invincible = false;
    private float invis_timer = 0.20f;


    Vector2 startLerp, endLerp;
    public float lerpSpeed;
    public float timeStartedLerping;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        isCentred = true;
        health = healthMax;
        resetTime = 3;
        //startLerping();
    }

    private void Update()
    {
        if (health == 0)
            RechargeHealth();
        if (health != 0)
            resetTime = 3;
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

        if (invincible)
        {
            updateInvincibility();
        }        
    }

    void updateInvincibility()
    {
        invincibility_timer -= Time.deltaTime;
        invis_timer -= Time.deltaTime;
        if (invis_timer <= 0.0f)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = !gameObject.GetComponent<SpriteRenderer>().enabled;
            invis_timer = 0.20f;
        }
        if (invincibility_timer <= 0.0f)
        {
            invincible = false;
            invincibility_timer = 1.5f;
            invis_timer = 0.20f;
            if (!gameObject.GetComponent<SpriteRenderer>().enabled)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    public void Movement(float direction)
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
        startLerp = transform.position;
        endLerp = startLerp + new Vector2(direction,0);

        //transform.position = Lerp(startLerp, endLerp, timeStartedLerping, lerpSpeed);

        transform.Translate(direction, 0, 0);
    }

    public void startLerping()
    {
        timeStartedLerping = Time.time;     
    }

    public Vector3 Lerp(Vector3 start, Vector3 end, float timeStartedLerping, float lerpTime = 1)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;

        float percentageCompleted = timeSinceStarted / lerpTime;

        var result = Vector3.Lerp(start, end, percentageCompleted);

        return result;
        //https://www.youtube.com/watch?v=62IFyHUdH9U
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (health != 0)
        {
            if (other.tag == "Obstacle" && !invincible)
            {
                Destroy(other.gameObject);
                health--;
                invincible = true;
                if(health == 0)
                {
                    invincibility_timer = 4.5f;
                }
                if(!StressManager.GetBurnout() && !StressManager.GetResting())
                {
                    StressManager.IncreaseStressLevel(1.0f);
                }
            }
            else if (other.tag == "Pickup")
            {
                Destroy(other.gameObject);
            }
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

    //private void OnMouseDrag()
    //{
    //    transform.position = GetMouseWorldPos() - mouse_offset;
    //}

    //Vector3 GetMouseWorldPos()
    //{
    //    Vector3 mouse_pos = Input.mousePosition;

    //    mouse_pos.z = Camera.main.WorldToScreenPoint(transform.position).z;

    //    return Camera.main.ScreenToWorldPoint(mouse_pos);
    //}

    //private void OnMouseDown()
    //{
    //    mouse_offset = transform.position - GetMouseWorldPos();
    //}
}
