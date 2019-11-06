﻿using System.Collections;
using System.Collections.Generic;
using System.IO; //Used for reading files

using UnityEngine;
using UnityEditor; //Used for reading Unity files
using UnityEngine.UI;

public class RobotGameUI : MonoBehaviour
{
    private Dictionary<string, Image> sprites;

    private Text score_text = null;

    /*
     * Called when the game object is created
     */
    private void Awake()
    {
        sprites = new Dictionary<string, Image>();

        //Looks for UI Sprites in children
        foreach(Transform child in GetComponentInChildren<Transform>())
        {
            Image image = child.GetComponent<Image>();
            if(image != null)
            {
                sprites.Add(child.gameObject.name, image);
            }
            if(child.name != "Title" && child.GetComponent<Text>())
            {
                score_text = child.GetComponent<Text>();
            }
        }
    }

    private void Update()
    {
        RobotGameManager rgm = FindObjectOfType<RobotGameManager>();
        if (rgm != null)
        {
            score_text.text = rgm.ScoreToDisplay();
        }
        else
        {
            score_text.text = "";
        }
    }

    public void UpdateUI()
    {
        string part_type = "Head";

        int attempts = 0;

        while(attempts < 3)
        {
            sprites[part_type].sprite = GenerateSprite(part_type);
            switch(attempts)
            {
                case 0:
                {
                    part_type = "Torso";
                    break;
                }
                case 1:
                {
                    part_type = "Legs";
                    break;
                }
            }
            attempts++;
        }
    }

    private Sprite GenerateSprite(string part_type)
    {
        Sprite sprite = null;
        string path = "Sprites/RobotReconstruction/";

        path += part_type + "/";

        path += FindObjectOfType<RobotRandomiser>().parts[part_type];

        //Load Sprite
        sprite = Resources.Load<Sprite>(path);

        return sprite;
    }

    /*
     * Gets the file extension of a sprite
     */
    private string TryExtension(string path)
    {
        string extension = ".png";
        int attempts = 0; //Used to iterate through file types

        while (!System.IO.File.Exists(path + extension))
        {
            //Change extension type till we get a match
            extension = ".jpg";
            attempts++;
            if (attempts > 1)
            {
                extension = "";
                Debug.LogError("No more extensions");
                return " ";
            }
        }
        return extension;
    }
}
