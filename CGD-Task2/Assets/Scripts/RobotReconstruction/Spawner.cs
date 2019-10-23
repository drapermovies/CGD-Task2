using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject body_part = null;
    [SerializeField] private float conveyor_speed = 3.0f; //Time object moves across the screen
    [SerializeField] private float spawn_rate = 0.85f; //Time in seconds between parts spawning

    private float timer = 0.0f;

    private List<Transform> objects = new List<Transform>();

    /*
     * Called every frame
     */
    private void Update()
    {
        timer += Time.deltaTime;

        foreach (Transform form in objects)
        {
            if (!form.GetComponent<RobotPart>().has_owner)
            {
                form.GetComponent<RobotPart>().ConveyorMovement(conveyor_speed);
            }
        }

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
    }

    /*
     * Spawns a body part
     */
    bool SpawnBodyPart()
    {
        Transform obj = Instantiate(body_part).transform;
        objects.Add(obj);
        obj.position = this.transform.position;

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
