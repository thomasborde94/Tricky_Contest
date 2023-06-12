using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Show in inspector

    [SerializeField] AudioClip mainLoop;

    public int musicState = 1;
    [HideInInspector] public bool canPlay = true;

    #endregion

    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canPlay == true)
        {
            canPlay = false;
            if (musicState == 1)
            {
                audioPlayer.clip = mainLoop;
                audioPlayer.Play();
            }
        }
    }

    #region Private

    private AudioSource audioPlayer;

    #endregion
}
