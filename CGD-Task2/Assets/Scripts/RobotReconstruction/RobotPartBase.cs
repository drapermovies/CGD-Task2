using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class RobotPartBase
{
    
    public List<string> colours;

     /*
     * Called on class creation
     */
    public RobotPartBase(string path = "Sprites/RobotReconstruction/Head")
    {
        //A list of colours for retrieving later
        colours = new List<string>();
        AssignColours(path);
    }

    /*
     * Allows us to get colour names to retrieve in other classes
     */
    void AssignColours(string path)
    {
        foreach (Sprite image in Resources.LoadAll<Sprite>(path))
        {
            colours.Add(image.name);
        }
    }
}