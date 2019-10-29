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
    public float minXSpeed = -1;
    public float maxXSpeed = 1;
    public float minYSpeed = -1;
    public float maxYSpeed = 1;
    public float xDrag = 100;
    public float yDrag = 100;

    //rng variables
    public float cooldownUntilRNG = 10;
    public float minCooldownRNG = 10;
    public float maxCoolDownRNG = 50;

    Rigidbody2D bug;
    Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        bug = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovementManager();
        rngManipulation();
    }

    private void MovementManager()
    {
        bug.velocity = transform.TransformDirection(velocity);
        if (XDirectionSpeed != 0)
        {
            velocity.x = baseSpeed * XDirectionSpeed * (xDrag / 100);
        }
        if (YDirectionSpeed != 0)
        {
            velocity.y = baseSpeed * YDirectionSpeed * (yDrag / 100);
        }
        bugDrag();
    }

    private void rngManipulation()
    {
        if (cooldownUntilRNG > 0)
        {
            cooldownUntilRNG--;
        }
        else
        {
            // generate new speed values
            XDirectionSpeed = Random.Range(minXSpeed, maxXSpeed);
            YDirectionSpeed = Random.Range(minYSpeed, maxYSpeed);
            // generate new cooldown until next rng shift
            cooldownUntilRNG = Random.Range(minCooldownRNG, maxCoolDownRNG);
            // reset drag
            xDrag = 100;
            yDrag = 100;
        }
    }

    private void bugDrag()
    {
        if (XDirectionSpeed > 0)
        {
            xDrag--;
        }
        else
        {
            xDrag++;
        }
        if (YDirectionSpeed > 0)
        {
            yDrag--;
        }
        else
        {
            yDrag++;
        }
    }
}
