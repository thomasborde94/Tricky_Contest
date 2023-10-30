using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    #region Show in Inspector

    [SerializeField] AudioSource[] _sfx;

    #endregion

    private void Awake()
    {
        instance = this;
    }
    // Plays the sfxToplay
    public void PlaySFX(int sfxToplay)
    {
        _sfx[sfxToplay].Play();
    }
}
