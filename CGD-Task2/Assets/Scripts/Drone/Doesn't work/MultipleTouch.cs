using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTouch : MonoBehaviour
{
    public GameObject circle;
    public List<TouchLocation> touches = new List<TouchLocation>();

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        while(i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
            if (t.phase == TouchPhase.Began)
            {
                Debug.Log("touch began");
            }
            else if (t.phase == TouchPhase.Ended)
            {
                Debug.Log("touch ended");
            }
            else if (t.phase == TouchPhase.Moved)
            {
                Debug.Log("touch moved");
            }
            i++;
        }
    }
}
