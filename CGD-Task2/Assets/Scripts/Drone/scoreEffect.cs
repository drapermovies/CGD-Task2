using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = transform.position;
        pos.z = -1;
        transform.position = pos;
        if(gameObject.GetComponent<TextMesh>().text == "-25" || gameObject.GetComponent<TextMesh>().text == "-50")
        {
            gameObject.GetComponent<TextMesh>().color = Color.red;
        }
        else
        {
            gameObject.GetComponent<TextMesh>().color = Color.green;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        pos.y += 3 * Time.deltaTime;
        transform.position = pos;

        Color col = gameObject.GetComponent<TextMesh>().color;
        col.a -= 1.5f * Time.deltaTime;
        gameObject.GetComponent<TextMesh>().color = col;

        if(col.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
