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
                    part.is_fading = true;
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
                switch (child.part)
                {
                    case RobotPartsEnum.PART_HEAD:
                    {
                        parts.TryGetValue("Head", out test);

                        ChangeValue("Head", test, child);

                        break;
                    }
                    case RobotPartsEnum.PART_TORSO:
                    {
                        parts.TryGetValue("Torso", out test);

                        ChangeValue("Torso", test, child);

                        break;
                    }
                    case RobotPartsEnum.PART_LEGS:
                    {
                        parts.TryGetValue("Legs", out test);

                        ChangeValue("Legs", test, child);

                        break;
                    }
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

        RobotPart r_b = new RobotPart();

        int length = r_b.colours.Count;

        AssignPart("Head", r_b.colours, ref length);
        AssignPart("Torso", r_b.colours, ref length);
        parts.Add("Legs", r_b.colours[0]);

        //Used to determine if we have the part
        part_success.Add("Head", false);
        part_success.Add("Torso", false);
        part_success.Add("Legs", false);

        FindObjectOfType<RobotGameUI>().UpdateUI();
    }
}
