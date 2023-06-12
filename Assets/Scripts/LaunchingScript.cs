using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchingScript : MonoBehaviour
{
    [SerializeField] private GameObject _trainUI;

    private void Awake()
    {
        _trainUI.SetActive(false);
    }
    #region OnClick Events
    public void LaunchNewGame()
    {
        StartCoroutine(NextLevel.instance.LoadMainScene());
    }

    public void DisplayTrainUI()
    {
        _trainUI.SetActive(true);
    }

    public void HideTrainUI()
    {
        _trainUI.SetActive(false);
    }

    public void LoadRace()
    {
        Cursors.cursorIsLocked = true;
        Cursors.isInDialogue = false;
        NextLevel.instance._goLaunching.Value = true;
        StartCoroutine(NextLevel.instance.LoadRace());
        
        Cursors.cursorIsLocked = true;
        Cursors.isInDialogue = false;
    }

    public void LoadShoot()
    {
        NextLevel.instance._goLaunching.Value = true;
        StartCoroutine(NextLevel.instance.LoadShooter());
    }

    public void LoadBaseball()
    {
        NextLevel.instance._goLaunching.Value = true;
        StartCoroutine(NextLevel.instance.LoadBaseball());
    }
    #endregion
}
