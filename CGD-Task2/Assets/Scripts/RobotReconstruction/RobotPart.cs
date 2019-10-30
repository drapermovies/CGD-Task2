using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RobotPart : MonoBehaviour
{
    [SerializeField] private RobotPartsEnum part = RobotPartsEnum.PART_HEAD;

    private bool was_seen = false;

    Vector3 target_scale = Vector3.zero;

    Vector3 mouse_offset = Vector3.zero;

    /*
     * Called just after object creation
     */
    private void Start()
    {
        GenerateBodyPart();
        target_scale = transform.localScale;
        //transform.localScale = new Vector3(0.1f, 0.1f, 0.1f); //Code to make object appear nice when spawning by scaling up the obj
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

        //ScaleTransform(); //Was gonna implement code that makes it nicer to look at when objects spawn

        if (render.isVisible && !was_seen)
        {
            was_seen = true;
        }
        else if (!render.isVisible && was_seen)
        {
            if (!Debug.isDebugBuild)
            {
                Destroy(gameObject);
            }
        }
    }

    /*
     * Assigns a random body part type to the object
     */
    void GenerateBodyPart()
    {
        int part_num = Random.Range(0, 3);

        part = (RobotPartsEnum)part_num;

        Debug.Log(part);
    }

    /*
     * Moves object along conveyor belt
     */
    public void ConveyorMovement(float speed, Vector3 dir)
    {
        Vector3 position = transform.position;
        float new_speed = speed * Time.deltaTime;

        position.x += new_speed * dir.x;
        position.y += new_speed * dir.y;

        transform.position = position;
    }

    private void OnMouseDown()
    {
        mouse_offset = transform.position - GetMouseWorldPos();
    }

    /*
     * Called when the mouse hovers over an object
     */
    private void OnMouseOver()
    {
        GetComponent<SpriteRenderer>().color = Color.red;

        if (Input.GetMouseButton(0))
        {
           transform.parent = null; //Removes the spawner as a parent
        }
    }

    /*
     * Called when the mouse is dragged
     */
    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() - mouse_offset;
    }

    /*
     * Called when the mouse is no longer hovering over an object
     */
    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void ScaleTransform()
    {
        if (transform.localScale == target_scale)
            return;

        Vector3 local_scale = transform.localScale;
        local_scale.x *= (8 * Time.deltaTime);
        local_scale.y *= (8 * Time.deltaTime);
        transform.localScale = local_scale;
    }

    /*
     * Converts the mouse pos from screen coordinates to world coordinates
     */
    Vector3 GetMouseWorldPos()
    {
        Vector3 mouse_pos = Input.mousePosition;

        mouse_pos.z = Camera.main.WorldToScreenPoint(transform.position).z;

        return Camera.main.ScreenToWorldPoint(mouse_pos);
    }
}
