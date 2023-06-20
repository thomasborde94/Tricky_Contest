using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public BoolVariable _glassesCollected;
    public IntVariable _enemyScoreShooting;
    public IntVariable _blueflowerAmount;
    public IntVariable _bluePotion;
    public IntVariable _currentCoin;
    public BoolVariable _drankBlue;
    public IntVariable _enemyFinalScore;
    public FloatVariable _enemyRaceScore;
    public BoolVariable _gaveBlue;
    public BoolVariable _gaveRed;
    public IntVariable _mushroomAmount;
    public IntVariable _playerFinalScore;
    public FloatVariable _playerRacingScore;
    public IntVariable _redFlowerAmount;
    public IntVariable _redPotion;
    public IntVariable _playerScoreShooting;
    public IntVariable _playerScoreBaseball;
    public IntVariable _staminaGainSpeed;
    public BoolVariable _updatedShoot;
    public BoolVariable _updatedRace;
    public BoolVariable _updatedBaseball;
    public BoolVariable _goLaunching;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Launching Scene")
        {
            _glassesCollected.Value = false;
            _enemyScoreShooting.Value = 25;
            _blueflowerAmount.Value = 0;
            _bluePotion.Value = 0;
            _currentCoin.Value = 0;
            _drankBlue.Value = false;
            _enemyFinalScore.Value = 0;
            _enemyRaceScore.Value = 40f;
            _gaveBlue.Value = false;
            _gaveRed.Value = false;
            _mushroomAmount.Value = 0;
            _playerFinalScore.Value = 0;
            _playerRacingScore.Value = 0f;
            _playerScoreBaseball.Value = 0;
            _redFlowerAmount.Value = 0;
            _redPotion.Value = 0;
            _playerScoreShooting.Value = 0;
            _staminaGainSpeed.Value = 2;
            _updatedBaseball.Value = false;
            _updatedRace.Value = false;
            _updatedShoot.Value = false;
            _goLaunching.Value = false;
        }
    }
}
