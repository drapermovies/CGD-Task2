using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcctivateResting : MonoBehaviour
{
    private void OnMouseUp()
    {
        if(!StressManager.GetBurnout())
        {
            if(StressManager.GetResting())
            {
                StressManager.SetResting(false);
                Debug.Log(StressManager.GetResting().ToString());
            }
            else // Not Resting
            {
                StressManager.SetResting(true);
                Debug.Log(StressManager.GetResting().ToString());
            }
        }
    }
}
