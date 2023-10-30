using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class Cursors : MonoBehaviour
{
    public static Cursors instance;

    [SerializeField] GameObject CursorObject;
    [SerializeField] Sprite CursorBasic;
    [SerializeField] Image CursorImage;

    public static bool cursorIsLocked = true;
    public static bool isInDialogue = false;
    #region Unity Lifecycle
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Launching Scene" || sceneName == "Ending scene win" || sceneName == "Ending scene loose" 
            || sceneName == "Ending scene tie")
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
        CursorImage.sprite = CursorBasic;
        LaunchingCursor();

    }

    void Update()
    {
        CursorUpdate();
        Debug.Log("cursor is locked is " + cursorIsLocked);
        Debug.Log("is in dialogue is " + isInDialogue);
        Debug.Log("cursor is visible is " + Cursor.visible);
    }
    #endregion

    #region Private Methods
    /// <summary>Displays or don't display the mouse cursor accordingly to the situation</summary>
    private void CursorUpdate()
    {
        if (/*Input.GetKeyDown(KeyCode.Escape) || */LevelManaging.instance._isPaused)
        {
            cursorIsLocked = false;
        }

        if (Input.GetMouseButtonDown(0) && isInDialogue == false || (!LevelManaging.instance._isPaused) && isInDialogue == false)
        {
            cursorIsLocked = true;
        }

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            if (sceneName == "Race_scene" || sceneName == "Main_scene" || sceneName == "Main_scene 2" || sceneName == "Main_scene 3")
            {
                CursorObject.transform.position = new Vector3(-1000f, -1000f, 0f);
                Spawn.instance._freeLookCamera.m_XAxis.m_InputAxisName = cursorIsLocked ? "Mouse X" : "";
                Spawn.instance._freeLookCamera.m_YAxis.m_InputAxisName = cursorIsLocked ? "Mouse Y" : "";
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            if (sceneName == "Race_scene" || sceneName == "Main_scene" || sceneName == "Main_scene 2" || sceneName == "Main_scene 3")
            {
                CursorObject.transform.position = Input.mousePosition;
                Spawn.instance._freeLookCamera.m_XAxis.m_InputAxisName = cursorIsLocked ? "Mouse X" : "";
                Spawn.instance._freeLookCamera.m_YAxis.m_InputAxisName = cursorIsLocked ? "Mouse Y" : "";
            }
            if (sceneName == "Launching Scene" || sceneName == "Ending scene win" || sceneName == "Ending scene loose"
                || sceneName == "Ending scene tie")
            {
                CursorObject.transform.position = Input.mousePosition;
                Cursor.visible = false;
            }
        }
        if (sceneName == "Baseball_scene" && !LevelManaging.instance._isPaused)
        {
            CursorObject.transform.position = new Vector3(-1000f, -1000f, 0f);
        }
        if (sceneName == "Baseball_scene" && LevelManaging.instance._isPaused)
            CursorObject.transform.position = Input.mousePosition;

        if (sceneName == "Shotter_scene")
        {
            if (LevelManaging.instance._isPaused)
            {
                cursorIsLocked = false;
                CursorObject.transform.position = Input.mousePosition;
            }
            else
            {
                CursorObject.transform.position = new Vector3(-1000f, -1000f, 0f);
                cursorIsLocked = true;
            }
        }
    }

    private void LaunchingCursor()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Launching Scene" || sceneName == "Ending scene win" || sceneName == "Ending scene loose"
            || sceneName == "Ending scene tie")
        {
            isInDialogue = true;
            cursorIsLocked = false;
        }
    }


    #endregion
}
