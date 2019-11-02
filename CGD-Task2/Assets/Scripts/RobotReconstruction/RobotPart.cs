﻿using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEditor; //Used with System.IO for File Reading
using UnityEngine.EventSystems;

public class RobotPart : MonoBehaviour
{
    public RobotPartsEnum part = RobotPartsEnum.PART_HEAD;

    public string obj_name { get; set; }

    public bool is_fading { get; set; }

    public List<string> colours;

    private bool was_seen = false;

    Vector3 target_scale = Vector3.zero;

    Vector3 mouse_offset = Vector3.zero;

    /*
     * Called on class creation
     */
    public RobotPart(string path = "Assets/Sprites/RobotReconstruction/Head")
    {
        //A list of colours for retrieving later
        colours = new List<string>();
        AssignColours(path);
    }

    /*
     * Called when the object starts being active
     */
    private void Start()
    {
        obj_name = "";

        GenerateBodyPart();

        target_scale = transform.localScale;
        GetComponent<SpriteRenderer>().sprite = GenerateSprite();

        transform.localScale = new Vector3(0.25f, 0.25f, 1f);
        ScaleBoundingBox();

        //transform.localScale = new Vector3(0.1f, 0.1f, 0.1f); //Code to make object appear nice when spawning by scaling up the obj
    }

    /*
     * Called on destruction
     */
     private void OnDestroy()
    {
        Spawner spawner = FindObjectOfType<Spawner>();

        if (spawner == null) return;

        spawner.DestroyBodyPart(this.transform);
     }

    /*
     * Called once a frame
     */
    private void Update()
    {
        Renderer render = GetComponent<Renderer>();

        //ScaleTransform(); //Was gonna implement code that makes it nicer to look at when objects spawn

        if (render.isVisible && !was_seen)
        {
            was_seen = true;
        }
        else if (!render.isVisible && was_seen)
        {
            if (!EditorApplication.isPaused)
            {
                Destroy(gameObject);
            }
        }

        if(is_fading)
        {
            FadeObjectOut();
        }
    }

    /*
     * Assigns a random body part type to the object
     */
    void GenerateBodyPart()
    {
        int part_num = Random.Range(0, 3);

        part = (RobotPartsEnum)part_num;
    }

    /*
     * Moves object along conveyor belt
     */
    public void ConveyorMovement(float speed, Vector3 dir)
    {
        Vector3 position = transform.position;
        float new_speed = speed * Time.deltaTime;

        position.x += new_speed * dir.x;
        position.y += new_speed * dir.y;

        transform.position = position;
    }

    /*
     * Called whenever the mouse is pressed
     */
    private void OnMouseDown()
    {
        mouse_offset = transform.position - GetMouseWorldPos();
    }

    /*
     * Called when the mouse hovers over an object
     */
    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
           transform.parent = null; //Removes the spawner as a parent
        }
    }

    /*
     * Called when the mouse is dragged
     */
    private void OnMouseDrag()
    {
        Transform obj = FindObjectOfType<RobotGoal>().transform;

        foreach(Transform child in obj.GetComponentInChildren<Transform>())
        {
            if(child.transform.position != transform.position)
            {
                transform.position = GetMouseWorldPos() - mouse_offset;
            }
        }
    }

    private void ScaleTransform()
    {
        if (transform.localScale == target_scale)
            return;

        Vector3 local_scale = transform.localScale;
        local_scale.x *= (8 * Time.deltaTime);
        local_scale.y *= (8 * Time.deltaTime);
        transform.localScale = local_scale;
    }

    /*
     * Converts the mouse pos from screen coordinates to world coordinates
     */
    Vector3 GetMouseWorldPos()
    {
        Vector3 mouse_pos = Input.mousePosition;

        //This line converts object z position to screen space
        //Changed back to world space in the return statement
        mouse_pos.z = Camera.main.WorldToScreenPoint(transform.position).z;

        //Returns mouse pos and modified z pos converted to world space
        return Camera.main.ScreenToWorldPoint(mouse_pos);
    }

    private Sprite GenerateSprite()
    {
        Sprite sprite = null;
        string path = "Assets/Sprites/RobotReconstruction/";

        switch(part)
        {
            case RobotPartsEnum.PART_HEAD:
            {
                path += "Head";
                break;
            }
            case RobotPartsEnum.PART_TORSO:
            {
                path += "Torso";
                break;
            }
            case RobotPartsEnum.PART_LEGS:
            {
                path += "Legs";
                break;
            }
            default:
            {
                Debug.LogError("Part not found" + part);
                break;
            }
        }

        uint random_range = 0;

        if(AssetDatabase.IsValidFolder(path))
        {
            foreach(string file in Directory.GetFiles(path))
            {
                if (Path.GetExtension(file) == ".jpg" || 
                    Path.GetExtension(file) == ".png")
                {
                    random_range++;
                }
            }
        }

        //Used to randomly assign a sprite
        int random_number = (int)Random.Range(0, random_range);

        obj_name = GetColour(random_number);

        path += "/" + obj_name;
        path += TryExtension(path);

        //Load Sprite
        if(System.IO.File.Exists(path))
        {
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            sprite = Sprite.Create(texture, 
                                   new Rect(0, 0, texture.width, texture.height), 
                                   new Vector2(0.5f, 0.5f));
        }
        else
        {
            Debug.LogError("Cannot find " + path);
        }

        return sprite;
    }

    /*
     * Returns colour for randomly getting sprite
     */
    private string GetColour(int number)
    {
        string colour = "";

        switch(number)
        {
            case 0:
            {
                colour = "Blue";
                break;
            }
            case 1:
            {
                colour = "Grey";
                break;
            }
            case 2:
            {
                colour = "Red";
                break;
            }
            case 3:
            {
                colour = "Pink";
                break;
            }
            case 4:
            {
                colour = "Gold";
                break;
            }
            default:
            {
               Debug.LogError("Can't find colour " + colour + " with ID " + 
                              number);
               break;
            }
        }

        return colour;
    }

    /*
     * Gets the file extension of a sprite
     */
    private string TryExtension(string path)
    {
        string extension = ".png";
        int attempts = 0; //Used to iterate through file types

        while(!System.IO.File.Exists(path + extension))
        {
            //Change extension type till we get a match
            extension = ".jpg"; 
            attempts++;
            if(attempts > 1)
            {
                Debug.LogError("No more extensions");
                return null;
            }
        }
        return extension;
    }

    /*
     * Scales bounding box in relation to sprite chosen
     */
    private void ScaleBoundingBox()
    {
       BoxCollider2D box_collider = GetComponent<BoxCollider2D>();
       switch(part)
        {
            case RobotPartsEnum.PART_HEAD:
            {
                box_collider.size = new Vector2(7f, 4.5f);
                break;
            }
            case RobotPartsEnum.PART_TORSO:
            {
                box_collider.size = new Vector2(6f, 5f);
                break;
            }
            case RobotPartsEnum.PART_LEGS:
            {
                box_collider.size = new Vector2(3, 3);

                //Minor tweaks because the leg's offset for some reason
                box_collider.offset = new Vector2(0f, 0.1f); 

                break;
            }
        }
    }

    /*
     * Allows us to get colour names to retrieve in other classes
     */
    void AssignColours(string path)
    {
        foreach(string file in Directory.GetFiles(path))
        {
            int start_point = file.Length - 4;
            string extension = file.Substring(start_point, 4);
            int length = file.Length - 4;

            if (extension == ".jpg" || extension == ".png")
            {
                string file_name = Path.GetFileNameWithoutExtension(file);
                colours.Add(file_name);
            }
        }
    }

    private void FadeObjectOut()
    {
        Color sprite_colour = transform.GetComponent<SpriteRenderer>().color;
        Color change_colour = sprite_colour;

        if (sprite_colour.a > 0.1f)
        {
            change_colour.a = 0f;
        }
        else
        {
            sprite_colour.a = 0f;
            Destroy(gameObject);
        }

        transform.GetComponent<SpriteRenderer>().color = Color.Lerp(sprite_colour, 
                                                change_colour, 5f * Time.deltaTime);
    }
}
