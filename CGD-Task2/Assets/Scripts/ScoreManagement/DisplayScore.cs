using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{

    private int score;
    public Text score_text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score = ScoreManager.CalculateTotalScore();
        score_text.text = "Score: " + Mathf.FloorToInt(score);
    }
}
