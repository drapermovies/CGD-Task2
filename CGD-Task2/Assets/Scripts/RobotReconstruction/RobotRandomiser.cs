using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotRandomiser : MonoBehaviour
{
    public Dictionary<string, string> parts;

    private Dictionary<string, bool> part_success;

    /*
     * Called when the object starts being active
     */
    private void Start()
    {
        GenerateRobot();
    }

    /*
     * Called every frame
     */
    private void Update()
    {
        CheckValues();

        uint successes = 0;

        foreach(bool result in part_success.Values)
        {
            if(result)
            {
                successes++;
            }
        }

        if(successes == 3)
        {
            GenerateRobot();
            GetComponent<RobotGameManager>().ChangeScore(20);

            foreach(RobotPart part in FindObjectsOfType<RobotPart>())
            {
                if(part.transform.parent == null)
                {
                    RobotGoal goal = FindObjectOfType<RobotGoal>();

                    if (goal != null)
                    {
                        foreach(Transform child in goal.transform.GetComponentInChildren<Transform>())
                        {
                            if(part.transform.position == child.transform.position)
                            {
                                part.is_fading = true;
                            }
                        }
                    }
                }
            }
        }
    }

    /*
     * Loops through the robot parts to find a match
     */
    private void CheckValues()
    {
        foreach (RobotPart child in FindObjectsOfType<RobotPart>())
        {
            string test = "";
            //Finds parts that aren't children of the conveyor
            if (child.transform.parent == null)
            {
                RobotGoal goal = FindObjectOfType<RobotGoal>();
                //Check we're in the goal zone
                if (goal.in_bound)
                {
                    switch (child.part)
                    {
                        case RobotPartsEnum.PART_HEAD:
                        {
                            parts.TryGetValue("Head", out test);

                            ChangeValue("Head", test, child);

                            if (test == child.obj_name)
                            {
                                //Get "Head" child and set transform position to that
                                child.transform.position = goal.transform.GetChild(0).position;

                                if (!child.played_audio)
                                {
                                    child.GetComponent<AudioSource>().Play();
                                    child.played_audio = true;
                                }
                            }

                            break;
                        }
                        case RobotPartsEnum.PART_TORSO:
                        {
                            parts.TryGetValue("Torso", out test);

                            ChangeValue("Torso", test, child);

                            if (test == child.obj_name)
                            {
                                //Get "Torso" child and set transform position to that
                                child.transform.position = goal.transform.GetChild(1).position;
                                if (!child.played_audio)
                                {
                                    child.GetComponent<AudioSource>().Play();
                                    child.played_audio = true;
                                }
                                }

                            break;
                        }
                        case RobotPartsEnum.PART_LEGS:
                        {
                            parts.TryGetValue("Legs", out test);

                            ChangeValue("Legs", test, child);

                            if (test == child.obj_name)
                            {
                                //Get "Legs" child and set transform position to that
                                child.transform.position = goal.transform.GetChild(2).position;
                                if (!child.played_audio)
                                {
                                    child.GetComponent<AudioSource>().Play();
                                    child.played_audio = true;
                                }
                            }
                            break;
                        }
                    }
                    //Removes any excess speed to prevent 'flying'
                    child.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    child.GetComponent<Rigidbody2D>().simulated = false;
                }
            }
        }
    }

    /*
     * Random finds a body part and assigns a random colour
     */
    private void AssignPart(string name, List<string> colours, ref int num)
    {
        int random = Random.Range(0, num);

        parts.Add(name, colours[random]);
    }

    /*
     * Assigns the dictionary that matches the key to the value
     * if the child is equal to the value
     */
    private void ChangeValue(string key, string value, RobotPart child)
    {
        if(value == child.obj_name)
        {
            if (!part_success[key])
            {
                FindObjectOfType<RobotGameManager>().ChangeScore(5);
                part_success[key] = true;
            }
        }
        else
        {
            Debug.Log("Key " + key + ", Value: " + value);
            child.is_fading = true;
        }
    }

    /*
     * Generates new colours for robot parts
     */
    private void GenerateRobot()
    {
        parts = new Dictionary<string, string>();
        part_success = new Dictionary<string, bool>();

        RobotPartBase r_b = new RobotPartBase();

        int length = r_b.colours.Count;

        AssignPart("Head", r_b.colours, ref length);
        AssignPart("Torso", r_b.colours, ref length);

        //Reset these variables because legs have different variables from torso/head
        r_b = new RobotPartBase("Sprites/RobotReconstruction/Legs");
        length = r_b.colours.Count;

        AssignPart("Legs", r_b.colours, ref length);

        //Used to determine if we have the part
        part_success.Add("Head", false);
        part_success.Add("Torso", false);
        part_success.Add("Legs", false);

        FindObjectOfType<RobotGameUI>().UpdateUI();
    }
}
