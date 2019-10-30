﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotGameManager : MonoBehaviour
{
    int score = 0;

    public void ChangeScore(int change)
    {
        score += change;
    }

    public string ScoreToDisplay()
    {
        return score.ToString() + " points";
    }
}
