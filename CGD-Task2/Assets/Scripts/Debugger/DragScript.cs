using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScript : MonoBehaviour
{

    private Vector2 clickdown_pos;
    private Vector2 mouse_pos;
    public bool grabbed = false;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (grabbed == true)
        {
            mouse_pos = Input.mousePosition;
            mouse_pos = Camera.main.ScreenToWorldPoint(mouse_pos);
            transform.localPosition = new Vector2(mouse_pos.x - clickdown_pos.x, mouse_pos.y - clickdown_pos.y);
        }
      
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouse_pos = Input.mousePosition;
            mouse_pos = Camera.main.ScreenToWorldPoint(mouse_pos);
            transform.localPosition = new Vector2(mouse_pos.x - clickdown_pos.x, mouse_pos.y - clickdown_pos.y);
            clickdown_pos.x = mouse_pos.x - transform.localPosition.x;
            clickdown_pos.y = mouse_pos.y - transform.localPosition.y;
            grabbed = true;
        }
    }

    private void OnMouseUp()
    {
        grabbed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "disposal" && grabbed == true)
        {
            Destroy(this.gameObject);
        }
    }


}
