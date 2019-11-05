using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotGoal : MonoBehaviour
{
    public bool in_bound { get; set; }

    private void Awake()
    {
        in_bound = false;
    }

    private void OnMouseOver()
    {
        in_bound = true;
    }

    private void OnMouseExit()
    {
        in_bound = false;
    }
}
