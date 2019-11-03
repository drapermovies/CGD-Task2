using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BugMovement : MonoBehaviour
{
    //movement variables
    [SerializeField] public float baseSpeed = 1;
    [SerializeField] public float XDirectionSpeed = 0;
    [SerializeField] public float YDirectionSpeed = 0;

    //movement rng variables
    public float minXSpeed = -1f;
    public float maxXSpeed = 1f;
    public float minYSpeed = -1f;
    public float maxYSpeed = 1f;
    public float xDrag = 100;
    public float yDrag = 100;

    //rng variables
    public float cooldownUntilRNG = 0.1f;
    public float minCooldownRNG = 1.5f;
    public float maxCoolDownRNG = 3f;
    public float chanceToGoToDanger = 30f;
    public float debuggerTimer = 0;

    //other
    public GameObject dangerZone;

    //debug
    public bool dangering = false;

    Rigidbody2D bug;
    Rigidbody2D dangerZoneArea;
    Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        bug = GetComponent<Rigidbody2D>();
        dangerZoneArea = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovementManager();
        rngManipulation();

        if (debuggerTimer > 10)
        {
            baseSpeed = baseSpeed * 1.5f;
            chanceToGoToDanger = chanceToGoToDanger + 5f;
            if (minCooldownRNG != maxCoolDownRNG)
            {
                minCooldownRNG = minCooldownRNG + 0.1f;
            }
            debuggerTimer = 0;
        }

        //track game timer
        debuggerTimer += Time.deltaTime;
    }

    private void MovementManager()
    {
        bug.velocity = transform.TransformDirection(velocity);
        if (XDirectionSpeed != 0)
        {
            velocity.x = baseSpeed * XDirectionSpeed;
        }
        if (YDirectionSpeed != 0)
        {
            velocity.y = baseSpeed * YDirectionSpeed;
        }
        bugDrag();
    }

    private void bugDrag()
    {
        if (XDirectionSpeed >= 0)
        {
            xDrag--;
        }
        if (XDirectionSpeed <= 0)
        {
            xDrag++;
        }
        if (YDirectionSpeed >= 0)
        {
            yDrag--;
        }
        if (YDirectionSpeed <= 0)
        {
            yDrag++;
        }
    }

    private void rngManipulation()
    {
        if (cooldownUntilRNG > 0)
        {
            cooldownUntilRNG -= Time.deltaTime;
        }
        else
        {
            if (Random.Range(1, 100) < chanceToGoToDanger)
            {
                dangering = true;
                if (this.transform.position.x < dangerZone.transform.position.x)
                {
                    XDirectionSpeed = Random.Range(-0.5f, maxXSpeed);
                }
                else
                {
                    XDirectionSpeed = Random.Range(minXSpeed, 0.5f);
                }
                if (this.transform.position.y < dangerZone.transform.position.y)
                {
                    YDirectionSpeed = Random.Range(0.5f, maxYSpeed);
                }
                else
                {
                    YDirectionSpeed = Random.Range(minYSpeed, 0.5f);
                }
            }
            else
            {
                dangering = false;
                // generate new speed values
                XDirectionSpeed = Random.Range(minXSpeed, maxXSpeed);
                YDirectionSpeed = Random.Range(minYSpeed, maxYSpeed);
            }
            // generate new cooldown until next rng shift
            cooldownUntilRNG = Random.Range(minCooldownRNG, maxCoolDownRNG);

            // reset drag
            if (XDirectionSpeed >= 0)
            {
                xDrag = 25;
            }
            else
            {
                xDrag = -25;
            }
            if (YDirectionSpeed >= 0)
            {
                yDrag = 25;
            }
            else
            {
                yDrag = -25;
            }
        }
    }
}
