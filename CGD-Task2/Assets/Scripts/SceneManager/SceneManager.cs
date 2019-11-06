using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    private static bool Gamefin = false;

    public enum SceneID
    {
        //Names of all the scenes used for the final build
        MainMenu,
        Game,
        EndScreen,
        DroneRunner
    }

    public static void Load(SceneID scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public static Scene GetCurrentScene()
    {
        return SceneManager.GetActiveScene();
    }

    public static bool GetGameFin()
    {
        return Gamefin;
    }

    public static void SetGameFin(bool nuwGameFin)
    {
        Gamefin = nuwGameFin;
    }
}

//to load a new scene: Loader.Load(Loader.SceneID.'Scene Name')