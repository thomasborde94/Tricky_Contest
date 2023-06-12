using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BaseballManager : MonoBehaviour
{
    public static BaseballManager instance;
    [SerializeField] IntVariable _playerScoreBaseball;
    [SerializeField] private float _delayBeforeStart = 2f;
    [SerializeField] private Text _countdownText;
    [SerializeField] private float _startingTime = 3f;
    [SerializeField] Text _pointsCount;
    [SerializeField] Text _timeRemaining;
    [SerializeField] Text _stopText;

    private float _currentTime;
    public float _currentTimeRemaining;
    void Start()
    {
        instance = this;
        _stopText.gameObject.SetActive(false);
        _currentTime = _startingTime;
        _pointsCount.text = "0";
        _currentTimeRemaining = 40;
        _timeRemaining.text = "20";
    }

    void Update()
    {
        _currentTime -= 1 * Time.deltaTime;
        _countdownText.text = _currentTime.ToString("0");
        _timeRemaining.text = "Time remaining : " + _currentTimeRemaining.ToString("0");

        if (_currentTime <= 0)
        {
            // Starts Time remaining countdown
            _countdownText.gameObject.SetActive(false);
            _currentTimeRemaining -= 1 * Time.deltaTime;

            if (_currentTimeRemaining <= 0)
            {
                _stopText.gameObject.SetActive(true);
                _currentTimeRemaining = 0;
            }
        }
        // Counts the points
        _pointsCount.text = "Points : " + _playerScoreBaseball.Value.ToString("0");
    }
    public float _delay
    {
        get { return _delayBeforeStart; }
    }
}
