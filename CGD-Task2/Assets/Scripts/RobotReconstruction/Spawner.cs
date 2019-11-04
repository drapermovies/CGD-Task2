using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool was_dragged { get; set; }

    [SerializeField] private GameObject body_part = null;
    //[SerializeField] private float conveyor_speed = 3.0f; //Time object moves across the screen
    [SerializeField] private float spawn_rate = 0.85f; //Time in seconds between parts spawning

    private float timer = 0.0f;

    private List<Transform> objects = new List<Transform>();

    //private Vector3 conveyor_dir = new Vector3(1.0f, 0.0f, 0.0f);

    
    //Called every frame
     
    private void Update()
    {
         timer += Time.deltaTime;

        //foreach (Transform form in objects)
        //{
        //    if (form.parent != null)
        //    {
        //        //form.GetComponent<RobotPart>().ConveyorMovement(conveyor_speed, conveyor_dir);
        //    }
        //}

        if (timer >= spawn_rate)
        {
            if(SpawnBodyPart())
            {
                timer = 0.0f;
            }
            else
            {
                Debug.LogError("Failed to spawn robot part");
            }
        }
        

        BoxCollider2D self_box = GetComponent<BoxCollider2D>();

        foreach (Transform child in FindObjectsOfType<Transform>())
        {
            if (child.name == "RobotPart(Clone)")
            {
                BoxCollider2D child_box = child.GetComponent<BoxCollider2D>();

                {
                    if (child.GetComponent<RobotPart>().is_colliding)
                    {
                        string obj_name = child.GetComponent<RobotPart>().obj_name;
                        child.SetParent(this.transform);
                    }
                }
            }
        }
    }


    /*
     * Spawns a body part
     */
    bool SpawnBodyPart()
    {
        Transform obj = Instantiate(body_part).transform;
        objects.Add(obj);
        obj.position = this.transform.position;
        obj.SetParent(this.transform);

        if(obj != null)
        {
            return true;
        }
        return false;
    }

    public void DestroyBodyPart(Transform part)
    {
        foreach(Transform obj in objects)
        {
            if(obj == part)
            {
                objects.Remove(obj);
                break;
            }
        }
    }

    private void OnMouseEnter()
    {
        was_dragged = false;
    }

    private void OnMouseExit()
    {
        was_dragged = true;
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if(collision.transform.name == "RobotPart(Clone)")
    //    {
    //        if (collision.transform.parent == null)
    //        {
    //            collision.GetComponent<RobotPart>().is_colliding = false;
    //        }
    //    }
    //}
}