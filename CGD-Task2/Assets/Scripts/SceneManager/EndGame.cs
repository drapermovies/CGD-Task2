using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private float time = 0.0f;
    public int MaxTime;

    // Update is called once per frame
    void Update()
    {
        if (time < MaxTime)
        {
            time += Time.deltaTime;
        }
        else
        {
            Loader.SetGameFin(true);
            Loader.Load(Loader.SceneID.EndScreen);
        }
        
    }
}
