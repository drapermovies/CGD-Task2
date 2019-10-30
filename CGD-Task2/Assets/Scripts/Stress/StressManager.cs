using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StressManager
{
    static bool Burnout = false;
    static bool Resting = false; 

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
