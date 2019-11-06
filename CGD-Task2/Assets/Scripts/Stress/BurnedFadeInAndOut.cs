using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurnedFadeInAndOut : MonoBehaviour
{

    Image Burned;

    void Start()
    {
        Burned = GetComponent<Image>();
        Burned.color = new Color(Burned.color.r, Burned.color.g, Burned.color.b, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (StressManager.GetBurnout())
        {
            if (Burned.color.a < 1.0f)
            {
                Burned.color = new Color(Burned.color.r, Burned.color.g, Burned.color.b, Burned.color.a + (Time.deltaTime * 2));
            }
        }
        else
        {
            if (Burned.color.a > 0.0f)
            {
                Burned.color = new Color(Burned.color.r, Burned.color.g, Burned.color.b, Burned.color.a - (Time.deltaTime * 2));
            }
        }
    }
}

