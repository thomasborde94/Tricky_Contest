using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    #region Show in inspector
    public static AudioManager instance;
    [SerializeField] AudioClip mainLoop;
    [SerializeField] AudioClip encounterLoop;
    [SerializeField] AudioClip winJingle;
    [SerializeField] AudioClip looseJingle;
    [SerializeField] AudioClip launchLoop;

    public int musicState = 1;
    [HideInInspector] public bool canPlay = true;

    #endregion

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    void Update()
    {
        ChooseLoop();
        // Plays the correct music accordingly to the musicState
        if (canPlay == true)
        {
            canPlay = false;
            if (musicState == 1)
            {
                audioPlayer.clip = mainLoop;
                audioPlayer.Play();
                audioPlayer.loop = true;
            }
            if (musicState == 2)
            {
                audioPlayer.clip = encounterLoop;
                audioPlayer.Play();
                audioPlayer.loop = true;
            }
            if (musicState == 3)
            {
                audioPlayer.clip = launchLoop;
                audioPlayer.Play();
                audioPlayer.loop = true;
            }
            if (musicState == 4)
            {
                audioPlayer.clip = winJingle;
                audioPlayer.Play();
                audioPlayer.loop = false;
            }
            if (musicState == 5)
            {
                audioPlayer.clip = looseJingle;
                audioPlayer.Play();
                audioPlayer.loop = false;
            }
        }
    }
    /// <summary>Changes musicState accordingly to the scene</summary>
    private void ChooseLoop()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Main_scene" || sceneName == "Main_scene 2" || sceneName == "Main_scene 3")
        {
            musicState = 1;
        }
        if (sceneName == "Shotter_scene" || sceneName == "Race_scene" || sceneName == "Baseball_scene")
        {
            musicState = 2;
        }
        if (sceneName == "Ending scene loose" || sceneName == "Ending scene tie")
        {
            musicState = 5;
        }
        if (sceneName =="Ending scene win")
        {
            musicState = 4;
        }
    }

    #region Private

    private AudioSource audioPlayer;

    #endregion
}
