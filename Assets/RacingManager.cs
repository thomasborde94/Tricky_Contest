using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RacingManager : MonoBehaviour
{
    #region Show in Inspector
    [SerializeField] FloatVariable _racingScore;
    public static RacingManager instance;

    [SerializeField] public Text _stopText;
    [SerializeField] private Text _countdownText;
    [SerializeField] private float _startingTime = 3f;
    [SerializeField] public Text _timeText;
    [SerializeField] private NextLevel _nextLevel;

    public float _currentScore;
    public float _finalRacingScore;
    public bool _getScore;

    #endregion
    #region Private

    [HideInInspector] public float _currentTime;
    

    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        _stopText.gameObject.SetActive(false);
        _currentTime = _startingTime;
        _timeText.text = "Time : 0";
    }

    void Update()
    {
        _currentTime -= 1 * Time.deltaTime;
        _countdownText.text = _currentTime.ToString("0");
        if (_currentTime <= 0 && !_nextLevel.FinishedRace)
        {
            _countdownText.gameObject.SetActive(false);
            _timeText.text = "Time : " + _currentScore.ToString("F1");
            _currentScore += Time.deltaTime;   
        }
        
        _racingScore.Value = Mathf.Round(_finalRacingScore * 10f) / 10f;
    }
    #endregion
}
