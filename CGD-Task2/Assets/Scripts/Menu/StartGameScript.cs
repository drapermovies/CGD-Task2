using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("START GAME");
        Loader.Load(Loader.Scene.SampleScene); //TEMP need to change to gameplay scene
    }
}
