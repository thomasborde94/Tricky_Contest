using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkScript : MonoBehaviour
{
    #region Show in inspector

    [SerializeField] private GameObject messageBox;
    [SerializeField] private Text _messageText;
    [SerializeField] private string _message;
    [SerializeField] private GameObject _pressE;


    [SerializeField] private GameObject _npcName;
    [SerializeField] private Text _npcNameText;
    [SerializeField] private string _name;

    [SerializeField] private GameObject _answerBox;
    [SerializeField] private Text _answer1Text;
    [SerializeField] private string _answer1;
    [SerializeField] private Text _answer2Text;
    [SerializeField] private string _answer2;
    [SerializeField] private Button _button1;
    [SerializeField] private Button _button2;

    [SerializeField] private bool isEric;
    [SerializeField] private bool needAnswer;
    [SerializeField] private string _clickEventName;
    [SerializeField] private AnswerBoxScript _answerBoxScript;

    public static bool _talkingBoxOn;

    #endregion
    #region Private

    private bool _playerHere = false;
    private GameObject _gameObject;

    #endregion

    private void Start()
    {
        _answerBox.SetActive(false);
        _gameObject = GetComponent<GameObject>();
    }

    private void Update()
    {
        if (_playerHere && Input.GetKeyDown(KeyCode.E))
        {
            ResetAnswerColors();
            messageBox.SetActive(true);
            _npcName.SetActive(true);
            _pressE.SetActive(false);

            if (needAnswer)
            {
                Cursors.isInDialogue = true;
                _answerBox.SetActive(true);
                Cursors.cursorIsLocked = false;
                MessageScript._messageScript = _clickEventName;
            }
        }
        TalkingBox();
    }

    #region Private functions

    private void OnTriggerEnter(Collider other)
    {
        if (isEric)
            _pressE.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        _talkingBoxOn = true;
        if (other.CompareTag("Player"))
        {
            if (_answerBoxScript._answerMessageFirst == false)
                _messageText.text = _message;
            else
                _messageText.text = _answerBoxScript._messageText.text;
            _npcNameText.text = _name;
            _playerHere = true;

            // Answer Box
            _answer1Text.text = _answer1;
            _answer2Text.text = _answer2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageBox.SetActive(false);
            _npcName.SetActive(false);
            _playerHere = false;
            _pressE.SetActive(false);

            _answerBox.SetActive(false);
            Cursors.cursorIsLocked = true;
            Cursors.isInDialogue = false;
            MessageScript._messageScript = null;
            _answerBoxScript._answerMessageFirst = false;
        }
    }

    private void TalkingBox()
    {
        if (_talkingBoxOn == false)
        {
            messageBox.SetActive(false);
            _npcName.SetActive(false);
            _playerHere = false;
            _pressE.SetActive(false);

            _answerBox.SetActive(false);
            Cursors.cursorIsLocked = true;
            if (!LevelManaging.instance._isPaused)
                Cursors.isInDialogue = false;
            MessageScript._messageScript = null;
        }
    }

    private void ResetAnswerColors()
    {
        _answer1Text.color = Color.black;
        _answer2Text.color = Color.black;
    }

    #endregion
}
