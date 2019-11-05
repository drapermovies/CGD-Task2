using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject explosion;

    private Vector3 startPos;
    
    Vector3 mouse_offset = Vector3.zero;

    private float invincibility_timer = 1.5f;
    private bool invincible = false;
    private float invis_timer = 0.20f;


    Vector2 startLerp, endLerp;
    public float lerpSpeed;
    public float timeStartedLerping;

    public bool controller_left;
    public bool controller_right;

    public bool moving = false;
    private float movement_direction = 0.0f;
    Vector2 old_pos;
    private float speed = 10.0f;

    Animator anim;

    private Vector2 touch_pos;
    bool can_move = true;

    private float score = 0.0f;
    public Text score_text;

    public GameObject score_effect;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        isCentred = true;
        health = healthMax;
        resetTime = 3;
        //startLerping();
        anim = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (health == 0)
            RechargeHealth();
        if (health != 0)
            resetTime = 3;
        if (health > 0)
        {
            if (!moving)
            {
                if ((Input.GetKeyUp(KeyCode.A) || controller_left) && !isLeft)
                {
                    Movement();
                    movement_direction = -moveDir;
                    moving = true;
                }
                else if ((Input.GetKeyUp(KeyCode.D) || controller_right) && !isRight)
                {
                    Movement();
                    movement_direction = moveDir;
                    moving = true;
                }
            }
            else
            {
                rotateAndMove();
            }
            
            score += 10.0f * Time.deltaTime;
        }
        if(score < 0.0f)
        {
            score = 0.0f;
        }
        score_text.text = "Score: " + Mathf.FloorToInt(score);

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
        else if(invincibility_timer <= 1.2f)
        {
            if (gameObject.GetComponent<SpriteRenderer>().color == Color.red)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    public void Movement()
    {      

        // In the centre of the screen
        if ((Input.GetKeyUp(KeyCode.A) || controller_left) && isCentred)
        {
            isLeft = true;
            isCentred = false;
            isRight = false;
        }
        else if ((Input.GetKeyUp(KeyCode.D) || controller_right) && isCentred)
        {
            isLeft = false;
            isCentred = false;
            isRight = true;            
        }

        // Either on the left or right
        else if ((Input.GetKeyUp(KeyCode.A) || controller_left) && !isCentred)
        {
            isLeft = false;
            isCentred = true;
            isRight = false;
        }
        else if ((Input.GetKeyUp(KeyCode.D) || controller_right)&& !isCentred)
        {
            isLeft = false;
            isCentred = true;
            isRight = false;            
        }
        old_pos = transform.position;
        //transform.position = Lerp(startLerp, endLerp, timeStartedLerping, lerpSpeed);
    }

    void rotateAndMove()
    {
        //startLerp = transform.position;
        //endLerp = startLerp + new Vector2(movement_direction, 0);
        // transform.Translate(movement_direction, 0, 0);

        if (movement_direction > 0.0f)
        {
            anim.SetBool("move_right", true);
        }
        else if (movement_direction < 0.0f)
        {
            anim.SetBool("move_left", true);
        }
        transform.position = Vector2.MoveTowards(transform.position,
            old_pos + new Vector2(movement_direction,0), speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, old_pos + new Vector2(movement_direction, 0)) < 0.1f)
        {
            transform.position = old_pos + new Vector2(movement_direction, 0);
            moving = false;
            controller_right = false;
            controller_left = false;
            anim.SetBool("move_right", false);
            anim.SetBool("move_left", false);
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (health != 0)
        {
            if (other.tag == "Obstacle" && !invincible)
            {
                Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                Destroy(other.gameObject);
                health--;
                invincible = true;
                if (health == 0)
                {
                    invincibility_timer = 4.5f;
                    score -= 50;
                    Instantiate(score_effect, gameObject.transform.position, gameObject.transform.rotation).GetComponent<TextMesh>().text = "-50";
                }
                else
                {
                    score -= 25;
                    Instantiate(score_effect, gameObject.transform.position, gameObject.transform.rotation).GetComponent<TextMesh>().text = "-25";
                }
                if(!StressManager.GetBurnout() && !StressManager.GetResting())
                {
                    StressManager.IncreaseStressLevel(1.0f);
                }
            }
            else if (other.tag == "Pickup")
            {
                Destroy(other.gameObject);
                if(health <3)
                {
                    health++;
                }
                score += 20;
                Instantiate(score_effect, gameObject.transform.position, gameObject.transform.rotation).GetComponent<TextMesh>().text = "+20";
                GetComponent<Spawner>().SpawnBodyPart();
            }
        }
    }

    void RechargeHealth()
    {
        moving = false;
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
    //    Vector2 pos_in = Input.mousePosition;

    //    Debug.Log("new pos:" + pos_in.x);
    //    Debug.Log("old pos:" + touch_pos.x);
    //    if (health > 0 && !moving && can_move)
    //    {
    //        if (pos_in.x >= touch_pos.x + 60 && !isRight)
    //        {
    //            touch_pos.x = pos_in.x;
    //            controller_right = true;
    //        }
    //        else if (pos_in.x <= touch_pos.x - 60 && !isLeft)
    //        {
    //            touch_pos.x = pos_in.x;
    //            controller_left = true;
    //        }
    //    }
    //}
    //private void OnMouseDown()
    //{
    //    touch_pos = Input.mousePosition;
    //}
    void OnTouchMove(Vector2 pos_in)
    {
        if (health > 0 && !moving && can_move)
        {
            if (pos_in.x >= touch_pos.x + 60 && !isRight)
            {
                touch_pos.x = pos_in.x;
                controller_right = true;
                //can_move = false;
            }
            else if(pos_in.x <= touch_pos.x - 60 && !isLeft)
            {
                touch_pos.x = pos_in.x;
                controller_left = true;
               // can_move = false;
            }
        }
    }
    void OnTouchDown(Vector2 pos_in)
    {
        touch_pos = pos_in;
    }
    //void OnTouchUp()
    //{
    //    can_move = true;
    //}
}
