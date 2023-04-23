using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Show in Inspector

    //[SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _maxForwardSpeed = 8f;
    [SerializeField] private float _turnSpeed = 200f;
    [SerializeField] private float _jumpSpeed = 3000f;

    public static bool canMove = true;
    public static bool moving = false;

    #endregion

    #region Unity Lifecycle
    // Start is called before the first frame update
    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _anim = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(moveDirection);
        Jump(jumpDirection);

        RaycastHit hit;
        Ray ray = new Ray(_transform.position + Vector3.up * groundRayDist * 0.5f, -Vector3.up);
        if (Physics.Raycast(ray, out hit, groundRayDist))
        {
            Debug.Log(hit.distance);
            if (!onGround && Mathf.Approximately(forwardSpeed, 0) && hit.distance <= 1.2f)
            {
                onGround = true;
                _anim.SetBool("Land", true);
                readyJump = false;
                _anim.SetBool("ReadyJump", false);
                Debug.Log("Passé par landStill");
            }
            else if (!onGround && forwardSpeed > 0)
            {
                onGround = true;
                _anim.SetBool("Land", true);
                readyJump = false;
                _anim.SetBool("ReadyJump", false);
                Debug.Log("passé par là");
            }
        }
        else
        {
            onGround = false;
        }
        Debug.DrawRay(_transform.position + Vector3.up * groundRayDist * 0.5f, -Vector3.up * groundRayDist, Color.red);
    }



    #endregion

    #region Public Methods

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumpDirection = context.ReadValue<float>();
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
        desiredSpeed = _maxForwardSpeed * going * Mathf.Sign(forwardDirection); ;
        // Est-ce qu'on appuye sur la Key ? Si oui accélère, sinon décélère
        float acceleration = IsMoveInput ? groundAccel : groundDecel;

        forwardSpeed = Mathf.MoveTowards(forwardSpeed, desiredSpeed, acceleration * Time.deltaTime);
        _anim.SetFloat("ForwardSpeed", forwardSpeed);
        _anim.SetFloat("Turning", direction.x);

        _transform.Rotate(0, turnAmount * _turnSpeed * Time.deltaTime, 0);
    }

    private void Jump(float direction)
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

    #endregion

    #region Anim Events

    public void Launch()
    {
        _rigidbody.AddForce(0, _jumpSpeed, 0, ForceMode.Force);
        _anim.SetBool("Launch", false);
        _anim.applyRootMotion = false;
    }

    public void Land()
    {
        _anim.SetBool("Land", false);
        _anim.applyRootMotion = true;
        _anim.SetBool("Launch", false);
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

    

    #region Private

    private Transform _transform;
    private Animator _anim;
    private Rigidbody _rigidbody;

    private float jumpDirection;
    private bool readyJump = false;
    private bool onGround = true;
    private float groundRayDist = 2.2f;

    private Vector2 moveDirection;
    private float forwardSpeed;
    private float desiredSpeed;
    private const float groundAccel = 10f;
    private const float groundDecel = 25f;

    #endregion
}
