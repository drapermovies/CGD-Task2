using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreManager
{
    static int DroneScore;
    static int DebugScore;
    static int RobotScore;

    //Get & Set DroneScore
    public static void SetDroneScore(int newScore)
    {
        DroneScore = newScore;
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
}
