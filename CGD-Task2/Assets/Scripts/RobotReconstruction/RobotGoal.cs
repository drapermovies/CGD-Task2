using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotGoal : MonoBehaviour
{
    public bool in_bound { get; set; }

    Vector2 mouse_pos = Vector2.zero;

    private void Awake()
    {
        in_bound = true;
    }

    private void Update()
    {
        if (FindObjectOfType<RobotRandomiser>().completed_robot)
        {
            foreach (RobotPart part in FindObjectsOfType<RobotPart>())
            {
                if (part.transform.parent == null)
                {
                    foreach (Transform child in this.transform.GetComponentInChildren<Transform>())
                    {
                        if (part.transform.position == child.transform.position)
                        {
                            part.is_fading = true;
                        }
                    }
                }
            }
            FindObjectOfType<RobotRandomiser>().completed_robot = false;
        }
    }

    private void OnTouchMove(Vector2 pos)
    {
        pos = Camera.main.ScreenToWorldPoint(pos);

        mouse_pos = pos;
    }

    void OnTouchDown(Vector2 vec)
    {
        vec = Camera.main.ScreenToWorldPoint(vec);
        mouse_pos = vec;
    }

    private void OnTouchUp(Vector2 pos)
    {
        in_bound = false;
    }
}
