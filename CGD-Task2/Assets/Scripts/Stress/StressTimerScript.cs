using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressTimerScript : MonoBehaviour
{

    public float maxStress;

    public float restRecoverySpeed;
    public float burnoutRecoverySpeed;

    float stressLevel;

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
            if (stressLevel < maxStress)
            {
                stressLevel += Time.deltaTime;
                stressBar.fillAmount = stressLevel / maxStress;
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
                if(stressLevel > 0)
                {
                    stressLevel -= Time.deltaTime * restRecoverySpeed;
                    stressBar.fillAmount = stressLevel / maxStress;
                }
                else
                {
                    StressManager.SetResting(false);
                }
            }
            else if (StressManager.GetBurnout())
            {
                if(stressLevel > 0)
                {
                    stressLevel -= Time.deltaTime * burnoutRecoverySpeed;
                    stressBar.fillAmount = stressLevel / maxStress;
                }
                else
                {
                    StressManager.SetBurnout(false);
                }
            }
        }
    }
}
