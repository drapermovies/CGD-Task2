using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SerenityFadeInAndOut : MonoBehaviour
{

    Image Serenity;

    void Start()
    {
        Serenity = GetComponent<Image>();
        Serenity.color = new Color(Serenity.color.r, Serenity.color.g, Serenity.color.b, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (StressManager.GetResting())
        {
            if(Serenity.color.a < 1.0f)
            { 
                Serenity.color = new Color(Serenity.color.r, Serenity.color.g, Serenity.color.b, Serenity.color.a + (Time.deltaTime * 2));
            }
        }
        else
        {
            if(Serenity.color.a > 0.0f)
            {
                Serenity.color = new Color(Serenity.color.r, Serenity.color.g, Serenity.color.b, Serenity.color.a - (Time.deltaTime * 2));
            }
        }
    }
}
