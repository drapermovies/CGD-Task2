using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool was_dragged { get; set; }

    [SerializeField] private GameObject body_part = null;
    [SerializeField] private float conveyor_speed = 3.0f;
    [SerializeField] private float spawn_rate = 0.85f; //Time in seconds between parts spawning

    private float timer = 0.0f;

    private List<Transform> objects = new List<Transform>();

    private Vector3 conveyor_dir = new Vector3(1f, 0f, 0f);
    
    //Called every frame
     
    private void Update()
    {
         timer += Time.deltaTime;

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

        foreach (Transform form in objects)
        {
            if (form.parent != null)
            {
                form.GetComponent<RobotPart>().ConveyorMovement(conveyor_speed, conveyor_dir);
            }
        }

        foreach(RobotPart part in FindObjectsOfType<RobotPart>())
        {
            BoxCollider self = GetComponent<BoxCollider>();
            BoxCollider part_collider = part.GetComponent<BoxCollider>();

            if(part_collider.bounds.Intersects(self.bounds))
            {
                part.is_colliding = false;
            }
        }
    }


    /*
     * Spawns a body part
     */
    public bool SpawnBodyPart()
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
}