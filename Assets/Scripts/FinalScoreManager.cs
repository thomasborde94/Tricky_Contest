using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScoreManager : MonoBehaviour
{
    public static FinalScoreManager instance;
    [SerializeField] IntVariable _playerScoreShoot;
    [SerializeField] IntVariable _enemyScoreShoot;
    [SerializeField] FloatVariable _playerScoreRace;
    [SerializeField] FloatVariable _enemyScoreRace;
    [SerializeField] IntVariable _playerScoreBaseball;
    [SerializeField] IntVariable _enemyScoreBaseball;
    [SerializeField] IntVariable _playerFinalScore;
    [SerializeField] IntVariable _enemyFinalScore;

    [SerializeField] BoolVariable _shootUpdated;
    [SerializeField] BoolVariable _raceUpdated;
    [SerializeField] BoolVariable _baseballUpdated;

    private void Start()
    {
        instance = this;
    }
    /*private void Update()
    {
        if (!_shootUpdated.Value)
        {
            if (_playerScoreShoot.Value > _enemyScoreShoot.Value)
                _playerFinalScore.Value++;

            if (_playerScoreShoot.Value == _enemyScoreShoot.Value)
            {
                _playerFinalScore.Value++;
                _enemyFinalScore.Value++;
            }

            if (_playerScoreShoot.Value < _enemyScoreShoot.Value)
                _enemyFinalScore.Value++;
            _shootUpdated.Value = true;
        }

        if (!_raceUpdated.Value)
        {
            if (_playerScoreRace.Value < _enemyScoreRace.Value)
                _playerFinalScore.Value++;

            if (_playerScoreRace.Value == _enemyScoreRace.Value)
            {
                _playerFinalScore.Value++;
                _enemyFinalScore.Value++;
            }

            if (_playerScoreRace.Value > _enemyScoreRace.Value)
                _enemyFinalScore.Value++;
            _raceUpdated.Value = true;
        }

        if (!_baseballUpdated.Value)
        {
            if (_playerScoreBaseball.Value > _enemyScoreBaseball.Value)
                _playerFinalScore.Value++;

            if (_playerScoreBaseball.Value == _enemyScoreBaseball.Value)
            {
                _playerFinalScore.Value++;
                _enemyFinalScore.Value++;
            }

            if (_playerScoreBaseball.Value < _enemyScoreBaseball.Value)
                _enemyFinalScore.Value++;
            _baseballUpdated.Value = true;
        }
    }*/

    public void UpdateShootScore()
    {
        if (!_shootUpdated.Value)
        {
            if (_playerScoreShoot.Value > _enemyScoreShoot.Value)
                _playerFinalScore.Value++;

            if (_playerScoreShoot.Value == _enemyScoreShoot.Value)
            {
                _playerFinalScore.Value++;
                _enemyFinalScore.Value++;
            }

            if (_playerScoreShoot.Value < _enemyScoreShoot.Value)
                _enemyFinalScore.Value++;
            _shootUpdated.Value = true;
        }
    }

    public void UpdateRaceScore()
    {
        if (!_raceUpdated.Value)
        {
            if (_playerScoreRace.Value < _enemyScoreRace.Value)
                _playerFinalScore.Value++;

            if (_playerScoreRace.Value == _enemyScoreRace.Value)
            {
                _playerFinalScore.Value++;
                _enemyFinalScore.Value++;
            }

            if (_playerScoreRace.Value > _enemyScoreRace.Value)
                _enemyFinalScore.Value++;
            _raceUpdated.Value = true;
        }
    }

    public void UpdateBaseballScore()
    {
        if (!_baseballUpdated.Value)
        {
            if (_playerScoreBaseball.Value > _enemyScoreBaseball.Value)
                _playerFinalScore.Value++;

            if (_playerScoreBaseball.Value == _enemyScoreBaseball.Value)
            {
                _playerFinalScore.Value++;
                _enemyFinalScore.Value++;
            }

            if (_playerScoreBaseball.Value < _enemyScoreBaseball.Value)
                _enemyFinalScore.Value++;
            _baseballUpdated.Value = true;
        }
    }


}
