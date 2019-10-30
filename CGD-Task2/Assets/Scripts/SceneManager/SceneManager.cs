using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum SceneID
    {
        //Names of all the scenes used for the final build
        MainMenu,
        SampleScene,
        StressTester,
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
}

//to load a new scene: Loader.Load(Loader.SceneID.'Scene Name')