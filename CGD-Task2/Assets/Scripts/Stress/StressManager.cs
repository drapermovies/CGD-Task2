using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StressManager
{
    static float StressLevel;

    static bool Burnout = false;
    static bool Resting = false;

    //get & Set StressLevel
    public static void IncreaseStressLevel(float increaseValue)
    {
        StressLevel += increaseValue;
    }

    public static void DecreaseStressLevel(float decreaseValue)
    {
        StressLevel -= decreaseValue;
        if(StressLevel <0.0f)
        {
            StressLevel = 0.0f;
        }
    }

    public static float GetStressLevel()
    {
        return StressLevel;
    }

    //Get & Set BurnOut
    public static void SetBurnout(bool newBurnout)
    {
        Burnout = newBurnout;
    }

    public static bool GetBurnout()
    {
        return Burnout;
    }

    //Get & Set Resting
    public static void SetResting(bool newResting)
    {
        Resting = newResting;
    }

    public static bool GetResting()
    {
        return Resting;
    }
}
