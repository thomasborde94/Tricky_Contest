using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class Spawn : MonoBehaviour
{
    public static Spawn instance;
    [SerializeField] GameObject character;
    [SerializeField] GameObject crosshair;

    [SerializeField] private float _staminaReduceSpeed;
    [SerializeField] public IntVariable _staminaGainSpeed;
    [SerializeField] private float _timeBeforeGain = 2f;
    [SerializeField] private float _staminaLose;
    public CinemachineFreeLook _freeLookCamera;


    private bool IsDoneRegen = true;
    private float _timeElapsed = 0f;
    private bool jumpBeenInvoked = false;
    [HideInInspector] public bool rollBeenInvoked = false;
    private GameObject _weapon;
    private PlayerMovement myPlayer;
    private Animator _anim;

    [HideInInspector] public float topRigHeightBase = 5f;
    [HideInInspector] public float topRigRadiusBase = 8f;

    #region Unity Lifecycle
    private void Awake()
    {
        _spawnPoint = GetComponent<Transform>();
        instance = this;
    }
    void Start()
    {
        myPlayer = Instantiate(character, _spawnPoint.position, _spawnPoint.rotation).GetComponent<PlayerMovement>();
        myPlayer._crosshair = crosshair;
        _anim = myPlayer.GetComponent<Animator>();


        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Race_scene" || sceneName == "Main_scene" || sceneName == "Main_scene 2" || sceneName == "Main_scene 3")
        {
            _freeLookCamera = GameObject.Find("ThirdPersonCamera").GetComponent<CinemachineFreeLook>();
            _freeLookCamera.m_Orbits[0].m_Height = topRigHeightBase;
            _freeLookCamera.m_Orbits[0].m_Radius = topRigRadiusBase;
        }

    }

    private void Update()
    {
        Stamina();
        BuildingCamera();

        // Race
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Race_scene")
        {
            if (RacingManager.instance._currentTime <= 0)
                PlayerMovement.canMove = true;
        }

    }

    #endregion

    #region Private Methods
    private void Stamina()
    {
        // Gestion stamina Bar when running
        if (myPlayer.IsRunning && myPlayer.hasStamina)
        {
            if (!jumpBeenInvoked)
                UIController.instance._staminaBar.fillAmount -= 0.1f * _staminaReduceSpeed * Time.deltaTime;
        }

        // Gestion stamina Bar when jumping
        if (myPlayer.readyJump && myPlayer.hasStamina && !jumpBeenInvoked)
        {
            Invoke("JumpingOrRolling", 0f);
            jumpBeenInvoked = true;
        }
        if (!myPlayer.readyJump && !_anim.GetBool("Land"))
        {
            jumpBeenInvoked = false;
        }

        // Gestion stamina bar when rolling
        if (_anim.GetBool("Roll") && !rollBeenInvoked)
        {
            Invoke("JumpingOrRolling", 0f);
            rollBeenInvoked = true;
        }


        // Gestion quand la stamina tombe a 0
        if (UIController.instance._staminaBar.fillAmount <= 0)
        {
            UIController.instance._staminaBar.fillAmount = 0;
            IsDoneRegen = false;
        }
        if (UIController.instance._staminaBar.fillAmount == 0)
        {
            _timeElapsed += Time.deltaTime;
            if (_timeElapsed >= _timeBeforeGain)
            {
                IsDoneRegen = true;
                _timeElapsed = 0f;
            }
        }

        // Gestion récupération stamina
        if (IsDoneRegen == true && myPlayer.IsRunning == false && !rollBeenInvoked)
        {
            if (!jumpBeenInvoked)
                UIController.instance._staminaBar.fillAmount += 0.1f * _staminaGainSpeed.Value * Time.deltaTime;
            if (UIController.instance._staminaBar.fillAmount >= 1)
                UIController.instance._staminaBar.fillAmount = 1;
        }
    }

    private void BuildingCamera()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Main_scene" || sceneName == "Main_scene 2" || sceneName == "Main_scene 3")
        {
            float topRigHeightBuilding = 9.45f;
            float topRigRadiusBuilding = 2.55f;
            bool inAnyBuilding = false;

            foreach (RoofScript roofScript in RoofScript.buildingInstances.Values)
            {
                if (roofScript.isInBuilding)
                {
                    inAnyBuilding = true;
                    break;
                }
            }

            float targetHeight = inAnyBuilding ? topRigHeightBuilding : topRigHeightBase;
            float targetRadius = inAnyBuilding ? topRigRadiusBuilding : topRigRadiusBase;

            // Use Lerping for a smooth transition
            float lerpSpeed = 3f;
            float lerpFactor = Mathf.Lerp(_freeLookCamera.m_Orbits[0].m_Height, targetHeight, lerpSpeed * Time.deltaTime);
            _freeLookCamera.m_Orbits[0].m_Height = lerpFactor;

            lerpFactor = Mathf.Lerp(_freeLookCamera.m_Orbits[0].m_Radius, targetRadius, lerpSpeed * Time.deltaTime);
            _freeLookCamera.m_Orbits[0].m_Radius = lerpFactor;
        }
    }
    private void JumpingOrRolling()
    {
        UIController.instance._staminaBar.fillAmount -= _staminaLose;
    }

    #endregion
    #region Private

    private Transform _spawnPoint;

    #endregion

}
