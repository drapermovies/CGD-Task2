using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows;

using UnityEngine;
using UnityEditor; //Used with System.IO for File Reading
using UnityEngine.EventSystems;

public class RobotPart : MonoBehaviour
{
    public RobotPartsEnum part = RobotPartsEnum.PART_HEAD;

    public string obj_name { get; set; }

    public bool is_fading { get; set; }
    public bool is_colliding { get; set; }

    public bool played_audio { get; set; }

    private bool was_seen = false;

    public float mass;
    public Rigidbody rb;

    Vector3 target_scale = Vector3.zero;
    Vector2 mouse_offset = Vector2.zero;
    Vector2 touch_pos = Vector2.zero;

    /*
     * Called when the object starts being active
     */
    private void Start()
    {
        obj_name = "";
        is_colliding = true;
    
        //rb = GetComponent<Rigidbody>();
        //rb.mass = mass;

        GenerateBodyPart();

        if (part == RobotPartsEnum.PART_LEGS)
        {
            gameObject.AddComponent(typeof(BoxCollider));
            transform.localScale = new Vector3(4f, 4.4f, 1f);
            //rb.freezeRotation = true;
            GetComponent<BoxCollider>().size = new Vector3(4, 4, 1);
            mass = 0.1f;
        }
        else
        {
            if(part == RobotPartsEnum.PART_TORSO)
            {
                transform.localScale = new Vector3(3f, 3f, 1f);

            }
            else
            {
                // Head Transform
                transform.localScale = new Vector3(2.7f, 3.8f, 1f);
            }
            gameObject.AddComponent(typeof(BoxCollider));
            //rb.freezeRotation = true;

        }

        string part_string = PartString();

        target_scale = transform.localScale;
        GetComponent<SpriteRenderer>().sprite = GenerateSprite();

        if (FindObjectOfType<RobotRandomiser>().GetSuccess(part_string))
        {
            if (FindObjectOfType<RobotRandomiser>().GetColour(part_string)
                == obj_name)
            {
                FindObjectOfType<Spawner>().SpawnBodyPart();
                Destroy(gameObject);
                return;
            }
        }

        ScaleBoundingBox();

        //transform.localScale = new Vector3(0.1f, 0.1f, 0.1f); //Code to make object appear nice when spawning by scaling up the obj

        played_audio = false;
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
            Destroy(gameObject);
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
    private void OnTouchDown(Vector2 pos)
    {
        if (StressManager.GetBurnout())
        {
            //return;
        }

        Vector2 temp_mouse = Vector2.zero;
        temp_mouse.x = touch_pos.x - transform.position.x;
        temp_mouse.y = touch_pos.y - transform.position.y;
        mouse_offset = temp_mouse;

        touch_pos = pos;

        this.transform.parent = null; //Removes the spawner as a parent
    }

    /*
     * Called when the mouse is dragged
     */
    private void OnTouchMove(Vector2 pos)
    {
        if(StressManager.GetBurnout())
        {
            //return;
        }
        Transform obj = FindObjectOfType<RobotGoal>().transform;
        Vector2 new_position = Vector2.zero;

        touch_pos = pos;

        transform.position = GetTouchWorldPos();

        if(transform.parent)
        {
            transform.parent = null;
        }
    }

    void OnTouchUp(Vector2 pos)
    {
        if(!is_colliding)
        {
            if (!played_audio)
            {
                transform.GetChild(0).GetComponent<AudioSource>().Play();
                FindObjectOfType<RobotGameManager>().ChangeScore(-5);
                played_audio = true;
            }

            RobotRandomiser randomiser = FindObjectOfType<RobotRandomiser>();
            if (!randomiser.GetSuccess(PartString()))
            {
                is_fading = true;
            }
            else if(randomiser.GetColour(PartString()) != obj_name)
            {
                is_fading = true;
            }

        }
    }

    /*
    private void ScaleTransform()
    {
        if (transform.localScale == target_scale)
            return;

        Vector3 local_scale = transform.localScale;
        local_scale.x *= (8 * Time.deltaTime);
        local_scale.y *= (8 * Time.deltaTime);
        transform.localScale = local_scale;
    }
    */

    /*
     * Converts the mouse pos from screen coordinates to world coordinates
     */
    public Vector2 GetTouchWorldPos()
    {
        //Returns mouse pos and modified z pos converted to world space
        return Camera.main.ScreenToWorldPoint(touch_pos);
    }

    private Sprite GenerateSprite()
    {
        Sprite sprite = null;
        string path = "Sprites/RobotReconstruction/";

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

        foreach(Sprite image in Resources.LoadAll<Sprite>(path))
        {
            random_range++;
        }

        //Used to randomly assign a sprite
        int random_number = (int)Random.Range(0, random_range);

        obj_name = GetColour(random_number);

        path += "/" + obj_name;

        //Load Sprite
        sprite = Resources.Load<Sprite>(path);

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
       BoxCollider box_collider = GetComponent<BoxCollider>();
       SphereCollider circle_collider = GetComponent<SphereCollider>();

        switch (part)
        {
            case RobotPartsEnum.PART_HEAD:
            {
                    box_collider.center = new Vector2(0f, -0.77f);
                    box_collider.size = new Vector2(7f, 2.8f);
                    break;
            }
            case RobotPartsEnum.PART_TORSO:
            {
                    box_collider.center = new Vector2(0f, -0.25f);
                    box_collider.size = new Vector2(5.8f, 4f);
                    break;
            }
            //case RobotPartsEnum.PART_LEGS:
            //    {
            //        circle_collider.center = new Vector2(-0.04f, -0.37f);
            //        circle_collider.radius = 1.25f;
            //        break;
            //}
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

    private string PartString()
    {
        if(part == RobotPartsEnum.PART_HEAD)
        {
            return "Head";
        }
        else if(part == RobotPartsEnum.PART_TORSO)
        {
            return "Torso";
        }
        else if(part == RobotPartsEnum.PART_LEGS)
        {
            return "Legs";
        }
        return null;
    }
}