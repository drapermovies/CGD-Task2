using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreManager
{
    static int DroneScore = 0;
    static int DebugScore = 0;
    static int RobotScore = 0;

    //Get & Set DroneScore
    public static void SetDroneScore(int newScore)
    {
        DroneScore = newScore;
        Debug.Log(DroneScore);
    }

    public static int GetDroneScore()
    {
        return DroneScore;
    }

    //Get & Set DebugScore
    public static void SetDebugScore(int newScore)
    {
        DebugScore = newScore;
    }

    public static int GetDebugScore()
    {
        return DebugScore;
    }

    //Get & Set RobotScore
    public static void SetRobotScore(int newScore)
    {
        RobotScore = newScore;
    }

    public static int GetRobotScore()
    {
        return RobotScore;
    }

    //calculate total
    public static int CalculateTotalScore()
    {
        Debug.Log(DroneScore + DebugScore + RobotScore);
        return DroneScore + DebugScore + RobotScore;
    }

    public static void ResetScores()
    {
        DroneScore = 0;
        DebugScore = 0;
        RobotScore = 0;
    }
}
