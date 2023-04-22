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

    public static bool canMove = true;
    public static bool moving = false;

    #endregion

    #region Unity Lifecycle
    // Start is called before the first frame update
    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _anim = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(moveDirection);
        Jump(jumpDirection);
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
        Debug.Log(direction);
    }

    bool IsMoveInput
    {
        get { return !Mathf.Approximately(moveDirection.sqrMagnitude, 0f); }
    }

    bool IsForwardInput
    {
        get { return !Mathf.Approximately(moveDirection.y, 0f); }
    }

    #endregion

    #region Private

    private Transform _transform;
    private Animator _anim;
    private float jumpDirection;

    private Vector2 moveDirection;
    private float forwardSpeed;
    private float desiredSpeed;
    private const float groundAccel = 10f;
    private const float groundDecel = 25f;

    #endregion
}
