using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public static NextLevel instance;
    #region Show in Inspector
    [SerializeField] IntVariable _shootingPoints;
    [SerializeField] FloatVariable _playerRaceScore;

    [SerializeField] float _waitToLoad;
    [SerializeField] ShootingManager shootingManager;
    [SerializeField] private bool IsRace = false;
    [SerializeField] IntVariable _enemyFinalScore;
    [SerializeField] IntVariable _playerFinalScore;

    public BoolVariable _goLaunching;

    #endregion

    #region Private

    private float _waitBeforeLoadShooter = 0f;
    private bool isLoading;

    #endregion

    public bool FinishedRace = false;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        // Changes scene when shooter game is finished
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Shotter_scene")
        {
            if (shootingManager._currentTimeRemaining <= 0)
            {
                _waitBeforeLoadShooter += Time.deltaTime;
                if (_waitBeforeLoadShooter >= 2 && !isLoading)
                {
                    StartCoroutine(LoadSceneAfterShooter());
                    PlayerMovement.canMove = true;
                    Cursors.cursorIsLocked = true;
                }
            }
        }
        // Changes scene when baseball game is finished
        if (sceneName == "Baseball_scene")
        {
            if (BaseballManager.instance._currentTimeRemaining <= 0)
            {
                _waitBeforeLoadShooter += Time.deltaTime;
                if (_waitBeforeLoadShooter >= 2 && !isLoading)
                {
                    StartCoroutine(LoadEndingScene());
                }
            }
        }
    }
    // Changes scene when race is finished
    private void OnTriggerEnter(Collider other)
    {
        if (IsRace && other.CompareTag("Player"))
        {
            FinishedRace = true;
            if (FinishedRace && !RacingManager.instance._getScore)
            {
                RacingManager.instance._finalRacingScore = RacingManager.instance._currentScore;
                RacingManager.instance._getScore = true;
                RacingManager.instance._timeText.text = RacingManager.instance._finalRacingScore.ToString("F1");
            }
            Spawn.instance._staminaGainSpeed.Value = 2;
            RacingManager.instance._stopText.gameObject.SetActive(true);
            StartCoroutine(LoadAfterRace());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        FinishedRace = true;
    }

    public IEnumerator LoadAfterRace()
    {
        isLoading = true;
        UIController.instance.StartFadeToBlack();
        yield return new WaitForSeconds(_waitToLoad);
        if (_goLaunching.Value)
        {
            SceneManager.LoadScene("Launching Scene");
            _goLaunching.Value = false;
        }
        else
        {
            FinalScoreManager.instance.UpdateRaceScore();
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main_scene 3");
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            isLoading = false;
        }
    }

    public IEnumerator LoadEndingScene()
    {
        isLoading = true;
        UIController.instance.StartFadeToBlack();
        yield return new WaitForSeconds(_waitToLoad);
        if (_goLaunching.Value)
        {
            SceneManager.LoadScene("Launching Scene");
            _goLaunching.Value = false;
        }
        else
        {
            FinalScoreManager.instance.UpdateBaseballScore();
            if (_enemyFinalScore.Value < _playerFinalScore.Value)
            {
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Ending scene win");
                while (!asyncLoad.isDone)
                {
                    yield return null;
                }
                isLoading = false;
            }
            if (_enemyFinalScore.Value > _playerFinalScore.Value)
            {
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Ending scene loose");
                while (!asyncLoad.isDone)
                {
                    yield return null;
                }
                isLoading = false;
            }
            else
            {
                {
                    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Ending scene tie");
                    while (!asyncLoad.isDone)
                    {
                        yield return null;
                    }
                    isLoading = false;
                }
            }
        }
    }

    public IEnumerator LoadBaseball()
    {
        UIController.instance.StartFadeToBlack();

        yield return new WaitForSeconds(_waitToLoad);

        _shootingPoints.Value = 0;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Baseball_scene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public IEnumerator LoadShooter()
    {
        UIController.instance.StartFadeToBlack();

        yield return new WaitForSeconds(_waitToLoad);

        _shootingPoints.Value = 0;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Shotter_scene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public IEnumerator LoadRace()
    {
        UIController.instance.StartFadeToBlack();

        _playerRaceScore.Value = 0f;

        yield return new WaitForSeconds(_waitToLoad);

        Cursors.cursorIsLocked = true;
        Cursors.isInDialogue = false;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Race_scene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public IEnumerator LoadSceneAfterShooter()
    {
        isLoading = true;
        UIController.instance.StartFadeToBlack();
        yield return new WaitForSeconds(_waitToLoad);
        if (_goLaunching.Value)
        {
            SceneManager.LoadScene("Launching Scene");
            _goLaunching.Value = false;
        }
        else
        {
            FinalScoreManager.instance.UpdateShootScore();
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main_scene 2");
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            isLoading = false;
        }
        
    }

    public IEnumerator LoadMainScene()
    {
        UIController.instance.StartFadeToBlack();

        _shootingPoints.Value = 0;
        Cursors.isInDialogue = false;
        Cursors.cursorIsLocked = true;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main_scene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
