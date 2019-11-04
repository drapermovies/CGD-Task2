using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public LayerMask touchInputMask;

    private List<GameObject> touchList = new List<GameObject>();
    private GameObject[] touchesOld;

    // Update is called once per frame
    void Update()
    {
        Touch[] touches = Input.touches;

        for(int i = 0; i < Input.touchCount; i ++)
        {
            Debug.Log(touches[i].position);

            Ray ray = Camera.main.ScreenPointToRay(touches[i].position);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, touchInputMask))
            {
                Debug.Log("QUAZ");

                GameObject recipient = hit.transform.gameObject;
                touchList.Add(recipient);

                if (touches[i].phase == TouchPhase.Began)
                {
                    recipient.SendMessage("OnTouchDown", touches[i].position, SendMessageOptions.DontRequireReceiver);
                    Debug.Log("POOP");
                }

                if (touches[i].phase == TouchPhase.Ended)
                {
                    recipient.SendMessage("OnTouchUp", touches[i].position, SendMessageOptions.DontRequireReceiver);
                }

                if (touches[i].phase == TouchPhase.Stationary)
                {
                    recipient.SendMessage("OnTouchStay", touches[i].position, SendMessageOptions.DontRequireReceiver);
                }

                if (touches[i].phase == TouchPhase.Moved)
                {
                    recipient.SendMessage("OnTouchMove", touches[i].position, SendMessageOptions.DontRequireReceiver);
                }

                if (touches[i].phase == TouchPhase.Canceled)
                {
                    recipient.SendMessage("OnTouchExit", touches[i].position, SendMessageOptions.DontRequireReceiver);
                }
            }
        }

    }
}