using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManaging : MonoBehaviour
{
    public static LevelManaging instance;

    #region Show in Inspector

    public int _currentCoins = 0;
    public bool _isPaused;
    public bool _didBuy;

    public IntVariable _currentCoinsVar;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        instance = this;
        _currentCoins = _currentCoinsVar.Value;
    }
    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName != "Launching Scene")
        {
            UIController.instance.coinText.text = "X " + _currentCoinsVar.Value.ToString();

            if (Input.GetKeyDown(KeyCode.P))
            {
                PauseUnpause();
            }
        }
    }
    #endregion

    #region Public Methods
    public void GetCoin()
    {
        _currentCoinsVar.Value++;
        UIController.instance.coinText.text = _currentCoinsVar.Value.ToString();
    }

    public void GetGold()
    {
        _currentCoinsVar.Value = _currentCoinsVar.Value + 5;
        UIController.instance.coinText.text = _currentCoinsVar.Value.ToString();
    }

    public void Get50()
    {
        _currentCoinsVar.Value = _currentCoinsVar.Value + 50;
        UIController.instance.coinText.text = _currentCoinsVar.Value.ToString();
    }

    public void SpendCoins(int amount)
    {
        if (_currentCoinsVar.Value >= amount)
        {
            _currentCoinsVar.Value -= amount;
            _didBuy = true;
        }
        if (_currentCoins < 0)
        {
            _currentCoins = 0;

        }
        UIController.instance.coinText.text = _currentCoinsVar.Value.ToString();
    }

    public void PauseUnpause()
    {
        if (!_isPaused)
        {
            UIController.instance._pauseMenu.SetActive(true);

            _isPaused = true;
            Cursors.isInDialogue = true;
            Time.timeScale = 0f;
            ////
            Cursor.visible = false;
            Cursors.cursorIsLocked = false;
        }
        else
        {
            UIController.instance._pauseMenu.SetActive(false);

            Cursors.cursorIsLocked = true;
            _isPaused = false;
            Time.timeScale = 1f;
        }
    }

    #endregion

    #region OnClick Events

    public void ReturnToGame()
    {
        PauseUnpause();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Launching Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion
}
