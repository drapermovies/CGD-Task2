using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    public bool right;
    public bool left;

    public DroneMovement drone;

    private void OnMouseUp()
    {
        if (drone.health > 0 && !drone.moving)
        {
            if (right && !drone.isRight)
            {
                drone.Movement();
                Debug.Log("Right");
                if (drone.isCentred)
                {
                    drone.controller_right = true;
                }
                else if (drone.isLeft)
                {
                    drone.controller_right = true;
                }
            }
            else if (left && !drone.isLeft)
            {
                drone.Movement();
                Debug.Log("Left");
                if (drone.isCentred)
                {
                    drone.controller_left = true;
                }
                else if (drone.isRight)
                {
                    drone.controller_left = true;
                }
            }
        }
    }

}
