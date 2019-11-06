using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressTimerScript : MonoBehaviour
{
    public float maxStress;

    public float restRecoverySpeed;
    public float burnoutRecoverySpeed;

    //UI elements
    Image stressBar;

    // Start is called before the first frame update
    void Start()
    {
        stressBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!StressManager.GetResting() && !StressManager.GetBurnout())
        {
            if (StressManager.GetStressLevel() < maxStress)
            {
                StressManager.IncreaseStressLevel(Time.deltaTime);
                stressBar.fillAmount = StressManager.GetStressLevel() / maxStress;
            }
            else
            {
                Debug.Log("BURNOUT");
                StressManager.SetBurnout(true);
                Debug.Log(StressManager.GetBurnout().ToString());
            }
        }
        else
        {
            if(StressManager.GetResting())
            {
                if(StressManager.GetStressLevel() > 0)
                {
                    StressManager.DecreaseStressLevel(Time.deltaTime * restRecoverySpeed);
                    stressBar.fillAmount = StressManager.GetStressLevel() / maxStress;
                }
                else
                {
                    StressManager.SetResting(false);
                }
            }
            else if (StressManager.GetBurnout())
            {
                if(StressManager.GetStressLevel() > 0)
                {
                    StressManager.DecreaseStressLevel(Time.deltaTime * burnoutRecoverySpeed);
                    stressBar.fillAmount = StressManager.GetStressLevel() / maxStress;
                }
                else
                {
                    StressManager.SetBurnout(false);
                }
            }
        }
    }
}
