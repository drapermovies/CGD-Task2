using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{
    private void OnTouchUp()
    {
        Debug.Log("START GAME");
        ScoreManager.ResetScores();
        Loader.SetGameFin(false);
        Loader.Load(Loader.SceneID.Game); //TEMP need to change to gameplay scene
    }
}