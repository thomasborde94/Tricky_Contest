using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    #region Show in Inspector

    [SerializeField] Image _fadeScreen;
    [SerializeField] float _fadeSpeed;
    [SerializeField] GameObject _shootingCanvas;
    [SerializeField] GameObject _UIBar;
    [SerializeField] GameObject _racingCanvas;
    [SerializeField] public GameObject _pauseMenu;

    [SerializeField] GameObject _targetManager;

    public Text coinText;

    #endregion

    #region Public

    public Image _staminaBar;

    #endregion

    #region Private

    private bool _fadeToBlack;
    private bool _fadeOutBlack;

    #endregion
    #region Unity Lifecycle

    private void Awake()
    {
        instance = this;
        _fadeScreen.gameObject.SetActive(true);
    }

    void Start()
    {
        _fadeOutBlack = true;
        _fadeToBlack = false;

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName != "Shotter_scene" && sceneName != "Launching Scene")
        {
            _shootingCanvas.SetActive(false);
            _targetManager.gameObject.SetActive(false);
        }
        if (sceneName != "Race_scene")
            _racingCanvas.SetActive(false);
        if (sceneName == "Shotter_scene" || sceneName == "Baseball_scene")
            _UIBar.SetActive(false);
    }

    void Update()
    {
        // FadeScreen disabling
        if (_fadeOutBlack)
        {
            _fadeScreen.color = new Color(_fadeScreen.color.r, _fadeScreen.color.g, _fadeScreen.color.b, Mathf.MoveTowards(_fadeScreen.color.a, 0f, _fadeSpeed * Time.deltaTime));

            if (_fadeScreen.color.a == 0f)
            {
                _fadeOutBlack = false;
            }
        }

        // Fading out of the fadescreen
        if (_fadeToBlack)
        {
            _fadeScreen.color = new Color(_fadeScreen.color.r, _fadeScreen.color.g, _fadeScreen.color.b, Mathf.MoveTowards(_fadeScreen.color.a, 1f, _fadeSpeed * Time.deltaTime));

            if (_fadeScreen.color.a == 1f)
            {
                _fadeToBlack = false;
            }
        }
    }
    #endregion

    #region Public Methods

    public void StartFadeToBlack()
    {
        _fadeToBlack = true;
        _fadeOutBlack = false;
    }



    #endregion
}
