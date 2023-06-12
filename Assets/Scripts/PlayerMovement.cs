using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    #region Show in Inspector

    //[SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _maxForwardSpeed = 8f;
    [SerializeField] private float _turnSpeed = 200f;
    [SerializeField] private float _jumpSpeed = 3000f;
    [SerializeField] private CapsuleCollider _mainCollider;
    [SerializeField] private BoxCollider _kickCollider;

    [SerializeField] private float _forwardJump;
    [SerializeField] private Transform _spine;
    [SerializeField] private float xSensibility;
    [SerializeField] private float ySensibility;

    [SerializeField] private LineRenderer _laser;
    [HideInInspector] public GameObject _crosshair;
    [SerializeField] private GameObject _crossLight;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _cannon;
    [SerializeField] private float _bulletSpeed = 5f;
    [SerializeField] private float _delayBetweenShots = 0.17f;
    [SerializeField] private float _destroyTime = 2f;

    [SerializeField] BaseballThrow _baseballThrow;
    [SerializeField] private Baseball_ball _ballPrefab;
    [SerializeField] private Transform _bat;

    private float _ballSpeed;

    #endregion

    #region Public variables
    public static bool canMove = true;
    public static bool moving = false;
    public bool hasStamina = true;

    #endregion


    #region Unity Lifecycle
    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _anim = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _nextShotTime = Time.time;

        mySpawn = GameObject.Find("PlayerSpawn").GetComponent<Spawn>();
        _mainCollider.enabled = true;
        _kickCollider.enabled = false;
    }

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Baseball_scene")
        {
            _bat = GameObject.Find("Bat_transform").transform;
            _baseballThrow = GameObject.Find("Eric_Baseball").GetComponent<BaseballThrow>();
        }
    }

    void Update()
    {
        // Enables moving
        if (canMove)
        {
            Move(moveDirection);
            Jump(jumpDirection);
        }

        // Jump Handling
        RaycastHit hit;
        Ray ray = new Ray(_transform.position + Vector3.up * groundRayDist * 0.5f, -Vector3.up);
        if (Physics.Raycast(ray, out hit, groundRayDist))
        {
            if (!onGround && Mathf.Approximately(forwardSpeed, 0) && hit.distance <= 1.2f && _rigidbody.velocity.y < -1)
            {
                onGround = true;
                readyJump = false;
                _anim.SetBool("Land", true);
                _anim.SetBool("ReadyJump", false);
            }
            else if (!onGround && forwardSpeed > 0)
            {
                onGround = true;
                _anim.SetBool("Land", true);
                readyJump = false;
                _anim.SetBool("ReadyJump", false);
            }
        }
        else
        {
            onGround = false;
        }
        Debug.DrawRay(_transform.position + Vector3.up * groundRayDist * 0.5f, -Vector3.up * groundRayDist, Color.red);

        // Shooting system
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Shotter_scene")
        {
            RaycastHit laserHit;
            Ray laserRay = new Ray(_laser.transform.position, _laser.transform.forward);
            if (Physics.Raycast(laserRay, out laserHit))
            {
                // Setup of the crosslight
                _laser.SetPosition(1, _laser.transform.InverseTransformPoint(laserHit.point));
                _crossLight.transform.localPosition = new Vector3(0, 0, _laser.GetPosition(1).z * 0.9f);
            }
        }
    }

    private void LateUpdate()
    {
        // Override animator animation, so the player rotates towards aiming point
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Shotter_scene")
        {
            lastLookDirection += new Vector3(-lookDirection.x * xSensibility, 0, lookDirection.y * ySensibility);
            lastLookDirection.x = Mathf.Clamp(lastLookDirection.x, -60, 40);
            lastLookDirection.z = Mathf.Clamp(lastLookDirection.z, -30, 30);
            _spine.localEulerAngles = lastLookDirection;
        }
    }

    #endregion

    #region Public Methods

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookDirection = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumpDirection = context.ReadValue<float>();
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        rollDirection = context.ReadValue<float>();
        if ((int)context.ReadValue<float>() == 1)
        {
            if (hasStamina)
            {
                if (IsRunning)
                    _anim.speed = 1.5f;
                else
                    _anim.speed = 1f;

                _anim.SetTrigger("Roll");
                _mainCollider.enabled = false;
            }

        }
    }

    public void OnKick(InputAction.CallbackContext context)
    {
        kickDirection = context.ReadValue<float>();
        if ((int)context.ReadValue<float>() == 1)
        {
            _anim.SetTrigger("Kick");
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        firing = false;
        if (_nextShotTime <= Time.time && (int)context.ReadValue<float>() == 1)
        { 
            _anim.SetTrigger("Fire");
            Firebullet();
            firing = true;
            _nextShotTime = Time.time + _delayBetweenShots;
            SFXManager.instance.PlaySFX(1);
        }
        
    }

    public void OnStrike()
    {
        _anim.SetBool("Strike", true);
    }

    public void OnArmed(InputAction.CallbackContext context)
    {
        _anim.SetBool("Armed", !_anim.GetBool("Armed"));
    }

    public void OnEsc(InputAction.CallbackContext context)
    {
        if ((int)context.ReadValue<float>() == 1f)
        {
            escapedPressed = true;
        }
        else
            escapedPressed = false;
    }

    public void UpdateCursorLock()
    {
        if (escapedPressed)
        {
            Debug.Log("pressed escape");
            cursorIsLocked = false;
        }

        if (cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    #endregion

    #region Private Methods

    private void Move(Vector2 direction)
    {
        float turnAmount = direction.x;
        float forwardDirection = direction.y;
        if (direction.sqrMagnitude > 1f)
        {
            direction.Normalize();
        }
        float going = IsForwardInput ? 1 : 0;
        _maxForwardSpeed = IsRunning && hasStamina ? 7 : 4;
        desiredSpeed = _maxForwardSpeed * going * Mathf.Sign(forwardDirection); ;
        float acceleration = IsMoveInput ? groundAccel : groundDecel;

        forwardSpeed = Mathf.MoveTowards(forwardSpeed, desiredSpeed, acceleration * Time.deltaTime);
        _anim.SetFloat("ForwardSpeed", forwardSpeed);
        _anim.SetFloat("Turning", direction.x);

        _transform.Rotate(0, turnAmount * _turnSpeed * Time.deltaTime, 0);

        if (UIController.instance._staminaBar.fillAmount == 0)
            hasStamina = false;
        else
            hasStamina = true;
    }

    private void Jump(float direction)
    {
        if (hasStamina)
        {
            if (direction > 0)
            {
                _anim.SetBool("ReadyJump", true);
                readyJump = true;
            }
            else if (readyJump)
            {
                _anim.SetBool("Launch", true);

            }
        }
    }

    private void Firebullet()
    {
        Bullet newBullet = Instantiate(_bulletPrefab, _cannon.position, _cannon.rotation);
        newBullet.Shoot(_bulletSpeed);

        Destroy(newBullet.gameObject, _destroyTime);
    }

    #endregion

    #region Anim Events

    public void Launch()
    {
        _rigidbody.AddForce(transform.forward * forwardSpeed * _forwardJump, ForceMode.Force);
        _rigidbody.AddForce(0, _jumpSpeed, 0, ForceMode.Force);
        _anim.SetBool("Launch", false);
        _anim.applyRootMotion = false;
    }

    public void Land()
    {
        _anim.SetBool("Land", false);
        _anim.applyRootMotion = true;
        _anim.SetBool("Launch", false);
        StartCoroutine(LeavingGround());
    }

    public void JustRolled()
    {
        mySpawn.rollBeenInvoked = false;
        _anim.speed = 1f;
        _mainCollider.enabled = true;
    }

    public void IsKicking()
    {
        _kickCollider.enabled = true;
    }
    public void Kicked()
    {
        _kickCollider.enabled = false;
    }

    public void JustStriked()
    {
        _anim.SetBool("Strike", false);
    }

    #endregion
    bool IsMoveInput
    {
        get { return !Mathf.Approximately(moveDirection.sqrMagnitude, 0f); }
    }

    bool IsForwardInput
    {
        get { return !Mathf.Approximately(moveDirection.y, 0f); }
    }

    public bool IsRunning
    {
        get { return Input.GetButton("Run"); }
    }

    public IEnumerator LeavingGround()
    {
        yield return new WaitForSeconds(_waitToGround);
        onGround = false;
    }


    public bool readyJump = false;

    #region Private

    private Transform _transform;
    private Animator _anim;
    private Rigidbody _rigidbody;

    private float jumpDirection;
    private float rollDirection;
    private float kickDirection;
    private bool onGround = true;
    private float groundRayDist = 1.3f;
    private float _waitToGround = 0.5f;

    private Vector2 moveDirection;
    private Vector3 lookDirection;
    private Vector3 lastLookDirection;

    private bool escapedPressed = false;
    private bool cursorIsLocked = true;

    private bool firing = false;
    private float _nextShotTime;

    private float forwardSpeed;
    private float desiredSpeed;
    private const float groundAccel = 10f;
    private const float groundDecel = 25f;

    private Spawn mySpawn;

    #endregion
}
