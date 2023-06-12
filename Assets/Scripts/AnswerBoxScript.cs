using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerBoxScript : MonoBehaviour
{
    public static AnswerBoxScript instance;
    [SerializeField] private GameObject messageBox;
    [SerializeField] public Text _messageText;
    [SerializeField] private Button _a1Button;
    [SerializeField] private Button _a2Button;
    [SerializeField] private Text _a1Text;
    [SerializeField] private Text _a2Text;
    [SerializeField] IntVariable _redFlowers;
    [SerializeField] IntVariable _blueFlowers;
    [SerializeField] IntVariable _mushrooms;
    [SerializeField] IntVariable _currentCoins;
    [SerializeField] IntVariable _bluePotion;
    [SerializeField] IntVariable _redPotion;
    [SerializeField] IntVariable _enemyScoreShoot;
    [SerializeField] IntVariable _playerScoreShoot;
    [SerializeField] BoolVariable _drankBlue;
    [SerializeField] BoolVariable _gaveBlue;
    [SerializeField] BoolVariable _gaveRed;
    [SerializeField] FloatVariable _enemyScoreRace;
    [SerializeField] FloatVariable _playerScoreRace;
    public bool _answerMessageFirst = false;

    private bool _alreadyGaveTea;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        // Removes all listeners at the beginning of a scene
        _a1Button.onClick.RemoveAllListeners();
        _a2Button.onClick.RemoveAllListeners();
    }
    private void Update()
    {
        ChooseAction();
    }

    private void ChooseAction()
    {
        if (MessageScript._messageScript == null)
        {
            _a1Button.onClick.RemoveAllListeners();
            _a2Button.onClick.RemoveAllListeners();
        }
        if (MessageScript._messageScript == "Laura1")
        {
            _a1Button.onClick.RemoveAllListeners();
            _a1Button.onClick.AddListener(Laura1_A);

            _a2Button.onClick.RemoveAllListeners();
            _a2Button.onClick.AddListener(Laura1_B);
        }

        if (MessageScript._messageScript == "Anna1")
        {
            _a1Button.onClick.RemoveAllListeners();
            _a1Button.onClick.AddListener(Anna1_A);

            _a2Button.onClick.RemoveAllListeners();
            _a2Button.onClick.AddListener(Anna1_B);
        }

        if (MessageScript._messageScript == "Eric2")
        {
            _a1Button.onClick.RemoveAllListeners();
            _a1Button.onClick.AddListener(Eric2_A);

            _a2Button.onClick.RemoveAllListeners();
            _a2Button.onClick.AddListener(Eric2_B);
        }

        if (MessageScript._messageScript == "Laura2")
        {
            Laura2_speech();
            _a1Button.onClick.RemoveAllListeners();
            _a1Button.onClick.AddListener(Laura2_A);

            _a2Button.onClick.RemoveAllListeners();
            _a2Button.onClick.AddListener(Laura2_B);
        }
        if (MessageScript._messageScript == "Max2")
        {
            // No tea at all
            if (_bluePotion.Value == 0 && _redPotion.Value == 0)
            {
                _a1Button.onClick.RemoveAllListeners();
                _a1Button.onClick.AddListener(Max2_A);

                _a2Button.onClick.RemoveAllListeners();
                _a2Button.onClick.AddListener(Max2_B);
            }

            // Only blue
            if (_bluePotion.Value > 0 && _redPotion.Value == 0)
            {
                _a1Text.text = "Take this blue tea";
                _a1Button.onClick.RemoveAllListeners();
                _a1Button.onClick.AddListener(Max2_A_OnlyBlue);

                _a2Text.text = "'Drink it in front of him.";
                _a2Button.onClick.RemoveAllListeners();
                _a2Button.onClick.AddListener(Max2_B_OnlyBlue);
            }

            // Only Red
            if (_bluePotion.Value == 0 && _redPotion.Value > 0)
            {
                _a1Text.text = "Take this red tea";
                _a1Button.onClick.RemoveAllListeners();
                _a1Button.onClick.AddListener(Max2_A_OnlyRed);

                _a2Text.text = "No, get it yourself.";
                _a2Button.onClick.RemoveAllListeners();
                _a2Button.onClick.AddListener(Max2_B_OnlyRed);
            }
            // Both tea
            if (_bluePotion.Value > 0 && _redPotion.Value > 0)
            {
                _a1Text.text = "Take this red tea";
                _a1Button.onClick.RemoveAllListeners();
                _a1Button.onClick.AddListener(Max2_A_OnlyRed);

                _a2Text.text = "Take this blue tea";
                _a2Button.onClick.RemoveAllListeners();
                _a2Button.onClick.AddListener(Max2_A_OnlyBlue);
            }
            if (_alreadyGaveTea)
            {
                AlreadyGaveTea();
            }
        }
        if (MessageScript._messageScript == "Anna2")
        {
            if (_bluePotion.Value == 0)
            {
                gameObject.SetActive(false);
                Anna2_DontHaveBlue();
            }
            if (_bluePotion.Value > 0)
            {
                _messageText.text = "If i were you, i would poison his tea just a little bit so he can't run that fast in the next game." +
                    " You just need to add some red mushrooms. Do you want to go for it ?";

                _a1Text.text = "Alright, go ahead";
                _a1Button.onClick.RemoveAllListeners();
                _a1Button.onClick.AddListener(Anna2_A_GotBlue);

                _a2Text.text = "No, i'll just drink it.";
                _a2Button.onClick.RemoveAllListeners();
                _a2Button.onClick.AddListener(Anna2_B_GotBlue);
            }
        }
        if (MessageScript._messageScript == "Laura3")
        {
            Laura3_speech();
            _a1Button.onClick.RemoveAllListeners();
            _a1Button.onClick.AddListener(Laura3_A);

            _a2Button.onClick.RemoveAllListeners();
            _a2Button.onClick.AddListener(Laura3_B);
        }

    }

    private void Laura1_A()
    {
        StartCoroutine(NextLevel.instance.LoadShooter());
    }

    private void Laura1_B()
    {
        TalkScript._talkingBoxOn = false;
    }

    private void Laura2_A()
    {
        StartCoroutine(NextLevel.instance.LoadRace());
        _answerMessageFirst = false;
    }

    private void Laura2_B()
    {
        TalkScript._talkingBoxOn = false;
        _answerMessageFirst = false;
    }

    private void Laura2_speech()
    {
        if (_playerScoreShoot.Value > _enemyScoreShoot.Value)
        {
            _messageText.text = "Congratulations! You beat Max with " + _playerScoreShoot.Value + " points, against "
                + _enemyScoreShoot.Value + " points for him. The next game is a race, are you ready ?";
        }
        if (_playerScoreShoot.Value == _enemyScoreShoot.Value)
        {
            _messageText.text = "First game was a tie ! You both got " + _playerScoreShoot.Value + ". The next game is a race," +
                " are you ready ?";
        }
        if (_playerScoreShoot.Value < _enemyScoreShoot.Value)
        {
            _messageText.text = "Unfortunately Max was better in the first game. He got " + _enemyScoreShoot.Value +
                " and you only got " + _playerScoreShoot.Value + ". The next game is a race, are you ready ?";
        }
    }

    private void Laura3_speech()
    {
        if (_playerScoreRace.Value < _enemyScoreRace.Value)
        {
            _messageText.text = "Congratulations! You beat Max with " + _playerScoreRace.Value + " , against "
                + _enemyScoreRace.Value + " for him. The next game is a baseball game, are you ready ?";
        }
        if (_playerScoreRace.Value == _enemyScoreRace.Value)
        {
            _messageText.text = "Second game was a tie ! You both got " + _playerScoreRace.Value + ". The next game is a baseball game," +
                " are you ready ?";
        }
        if (_playerScoreRace.Value > _enemyScoreRace.Value)
        {
            _messageText.text = "Unfortunately Max was better in the second game. He did " + _enemyScoreRace.Value +
                " and you only did " + _playerScoreRace.Value + ". The next game is a baseball game, are you ready ?";
        }
    }

    private void Laura3_A()
    {
        StartCoroutine(NextLevel.instance.LoadBaseball());
        _answerMessageFirst = false;
    }

    private void Laura3_B()
    {
        TalkScript._talkingBoxOn = false;
        _answerMessageFirst = false;
    }
    private void Anna1_A()
    {
        LevelManaging.instance.SpendCoins(40);
        if (LevelManaging.instance._didBuy == false)
        {
            _answerMessageFirst = true;
            _messageText.text = "Come back when you have 40 gold.";
            gameObject.SetActive(false);
        }
        else
        {
            _answerMessageFirst = true;
            _messageText.text = "It will be done before the contest starts";
            DataManager.instance._enemyScoreShooting.Value -= 5;
            gameObject.SetActive(false);
        }
        LevelManaging.instance._didBuy = false;
    }

    private void Anna1_B()
    {
        TalkScript._talkingBoxOn = false;
    }

    private void Anna2_DontHaveBlue()
    {
        _answerMessageFirst = true;
        _messageText.text = "If you bring me some blue tea i could spice it up a little. Think about it.";
    }
    private void Anna2_A_GotBlue()
    {
        _bluePotion.Value -= 1;
        _redPotion.Value += 1;

        _answerMessageFirst = true;
        _messageText.text = "You did the right choice.";
        gameObject.SetActive(false);
    }
    private void Anna2_B_GotBlue()
    {
        _bluePotion.Value -= 1;
        _drankBlue.Value = true;

        _answerMessageFirst = true;
        _messageText.text = "As you wish.";
        gameObject.SetActive(false);

        Spawn.instance._staminaGainSpeed.Value = 4;
    }
    private void Max2_A()
    {
        _answerMessageFirst = true;
        _messageText.text = "Thank you, I'll wait for you here.";
        gameObject.SetActive(false);
    }

    private void Max2_B()
    {
        _answerMessageFirst = true;
        _messageText.text = "I'm not surprised.";
        gameObject.SetActive(false);
    }

    private void Max2_A_OnlyBlue()
    {
        _bluePotion.Value -= 1;
        _gaveBlue.Value = true;
        _answerMessageFirst = true;
        _messageText.text = "Thank you.";
        gameObject.SetActive(false);

        _alreadyGaveTea = true;
        _enemyScoreRace.Value -= 4f;
    }
    private void Max2_B_OnlyBlue()
    {
        _bluePotion.Value -= 1;
        _drankBlue.Value = true;
        _answerMessageFirst = true;
        _messageText.text = "I'll remember it.";
        gameObject.SetActive(false);

        Spawn.instance._staminaGainSpeed.Value = 4;
    }

    private void Max2_A_OnlyRed()
    {
        _redPotion.Value -= 1;
        _gaveRed.Value = true;
        _answerMessageFirst = true;
        _messageText.text = "Thank you.";
        gameObject.SetActive(false);

        _alreadyGaveTea = true;
        _enemyScoreRace.Value += 10f;
    }

    private void Max2_B_OnlyRed()
    {
        _answerMessageFirst = true;
        _messageText.text = "I'm not surprised.";
        gameObject.SetActive(false);
    }

    private void AlreadyGaveTea()
    {
        _answerMessageFirst = true;
        _messageText.text = "Thanks for the tea";
        gameObject.SetActive(false);
    }
    private void Eric2_A()
    {
        if (_redFlowers.Value >= 3 && _blueFlowers.Value >= 3 && _currentCoins.Value >= 10)
        {
            LevelManaging.instance.SpendCoins(10);
            _blueFlowers.Value -= 3;
            _redFlowers.Value -= 3;
            _bluePotion.Value++;
            _answerMessageFirst = true;
            _messageText.text = "I hope you'll like it !";
            gameObject.SetActive(false);
        }
        else
        {
            _answerMessageFirst = true;
            _messageText.text = "Come back when you have what I asked you.";
            gameObject.SetActive(false);
        }
    }

    private void Eric2_B()
    {
        TalkScript._talkingBoxOn = false;
    }

}
