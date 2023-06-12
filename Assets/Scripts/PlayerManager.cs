using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        // If we are in the main scene, the player can move
        if (sceneName == "Main_scene" || sceneName == "Main_scene 2" || sceneName == "Main_scene 3")
            PlayerMovement.canMove = true;

        // If we are in shooter scene, the player can't move
        if (sceneName == "Shotter_scene" || sceneName == "Race_scene" || sceneName == "Baseball_scene" || sceneName == "Ending scene win"
            || sceneName == "Ending scene loose" || sceneName == "Ending scene tie")
            PlayerMovement.canMove = false;
    }
}
