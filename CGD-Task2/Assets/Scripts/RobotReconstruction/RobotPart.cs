using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPart : MonoBehaviour
{
    public bool has_owner { get; set; }
    [SerializeField] private RobotParts part = RobotParts.PART_HEAD;

    private bool was_seen = false;

    /*
     * Called just after object creation
     */
    private void Start()
    {
        GenerateBodyPart();
        has_owner = false;
    }

    /*
     * Called on destruction
     */
    private void OnDestroy()
    {
        FindObjectOfType<Spawner>().DestroyBodyPart(this.transform);
    }

    /*
     * Called once a frame
     */
    private void Update()
    {
        Renderer render = GetComponent<Renderer>();

        if(render.isVisible && !was_seen)
        {
            was_seen = true;
        }
        else if(!render.isVisible && was_seen)
        {
            Destroy(gameObject);
        }
    }

    /*
     * Assigns a random body part type to the object
     */
    void GenerateBodyPart()
    {
        int part_num = Random.Range(0, 3);

        part = (RobotParts)part_num;

        Debug.Log(part);
    }

    /*
     * Moves object along conveyor belt
     */
    public void ConveyorMovement(float speed)
    {
        Vector3 position = transform.position;
        position.x += speed * Time.deltaTime;
        transform.position = position;
    }

    private void OnMouseOver()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
