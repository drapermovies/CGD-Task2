using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScript : MonoBehaviour
{

    private Vector2 clickdown_pos;
    private Vector2 touch_pos;
    public bool grabbed = false;

    // Start is called before the first frame update
    void Start()
    {
     
    }
    
    void OnTouchMove(Vector2 new_pos)
    {
        touch_pos = new_pos;
        touch_pos = Camera.main.ScreenToWorldPoint(touch_pos);
        transform.localPosition = new Vector2(touch_pos.x - clickdown_pos.x, touch_pos.y - clickdown_pos.y);
    }

    private void OnTouchDown(Vector2 new_pos)
    {
        touch_pos = new_pos;
        touch_pos = Camera.main.ScreenToWorldPoint(touch_pos);
            transform.localPosition = new Vector2(touch_pos.x - clickdown_pos.x, touch_pos.y - clickdown_pos.y);
            clickdown_pos.x = touch_pos.x - transform.localPosition.x;
            clickdown_pos.y = touch_pos.y - transform.localPosition.y;
            grabbed = true;        
    }

    private void OnTouchUp()
    {
        grabbed = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "disposal" && grabbed == true)
        {
            Destroy(this.gameObject);
            ScoreManager.SetDebugScore(ScoreManager.GetDebugScore() + 5);
            collision.gameObject.GetComponent<AudioSource>().Play();
            Debug.Log(ScoreManager.GetDebugScore());
        }
    }


}
