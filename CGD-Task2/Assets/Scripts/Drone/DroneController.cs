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
        if (drone.health > 0)
        {
            if (right && !drone.isRight)
            {
                drone.Movement(drone.moveDir);
                Debug.Log("Right");
                if (drone.isCentred)
                {
                    drone.isRight = true;
                    drone.isCentred = false;
                }
                else if (drone.isLeft)
                {
                    drone.isCentred = true;
                    drone.isLeft = false;
                }
            }
            else if (left && !drone.isLeft)
            {
                drone.Movement(-drone.moveDir);
                Debug.Log("Left");
                if (drone.isCentred)
                {
                    drone.isLeft = true;
                    drone.isCentred = false;
                }
                else if (drone.isRight)
                {
                    drone.isCentred = true;
                    drone.isRight = false;
                }
            }
        }
    }

}
