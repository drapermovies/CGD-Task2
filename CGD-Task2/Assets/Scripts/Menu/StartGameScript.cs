using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{
    private void OnMouseUp()
    {
        Debug.Log("START GAME");
        Loader.Load(Loader.SceneID.SampleScene); //TEMP need to change to gameplay scene
    }
}