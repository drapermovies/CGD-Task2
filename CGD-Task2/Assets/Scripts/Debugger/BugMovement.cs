using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BugMovement : MonoBehaviour
{
    //movement variables
    [SerializeField] public float baseSpeed = 1;

    //rng variables
    public float cooldownUntilRNG = 0.1f;
    public float minCooldownRNG = 3f;
    public float maxCoolDownRNG = 6f;
    public float chanceToGoToDanger = 30f;
    public float debuggerTimer = 0;
    public Vector2 distance;
    Vector3 decidedArea;
    public float angle = 0;

    //other
    public GameObject dangerZone;

    //debug
    public bool dangering = false;

    Rigidbody2D bug;

    // Start is called before the first frame update
    void Start()
    {
        bug = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rngManipulation();
        MovementManager();

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
        float decidedSpeed = baseSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, decidedArea, decidedSpeed);
        //transform.rotation *= Quaternion.AngleAxis(XDirectionSpeed * YDirectionSpeed * Time.deltaTime, Vector3.up);

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
            }
            else
            {
                dangering = false;
            }
            rerollDecidedArea();
            Vector3 dir = decidedArea - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            GetComponent<AudioSource>().Play();
            // generate new cooldown until next rng shift
            cooldownUntilRNG = Random.Range(minCooldownRNG, maxCoolDownRNG);
        }
    }

    private void rerollDecidedArea()
    {
        decidedArea.x = Random.Range(GetComponent<Renderer>().bounds.min.x, GetComponent<Renderer>().bounds.max.x);
        decidedArea.y = Random.Range(GetComponent<Renderer>().bounds.min.y, GetComponent<Renderer>().bounds.max.y);
    }
}
