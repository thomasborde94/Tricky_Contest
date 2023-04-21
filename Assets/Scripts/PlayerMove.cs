using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class PlayerMove : MonoBehaviour
{
    #region Public properties

    //[SerializeField] CinemachineVirtualCamera playerCam;
    //[SerializeField] GameObject spawnPoint;
    //[SerializeField] CharacterController controller;
    [SerializeField] private float _movementSpeed = 6f;
    public static bool canMove = true;
    public LayerMask moveLayer;
    public static bool moving = false;

    #endregion

    

    #region Unity Lifecycle
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        //SaveScript.spawnPoint = spawnPoint;
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        /*if (velocitySpeed != 0)
        {
            anim.SetBool("sprinting", true);
            moving = true;
        }
        else
        {
            anim.SetBool("sprinting", false);
            moving = false;
        }
        */

    }

    private void FixedUpdate()
    {
        Rotate();
        Vector3 velocity =  _movementDirection* _movementSpeed;
        Vector3 translation = velocity * Time.fixedDeltaTime;
        Vector3 newPosition = _transform.position + translation;
        _rigidbody.MovePosition(newPosition);
    }

    #endregion

    #region Main

    private void GetInputs()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        _movementDirection = new Vector3(horizontal, 0, vertical);
        _movementDirection.Normalize();

        float targetAngle = Mathf.Atan2(_movementDirection.x, _movementDirection.z) * Mathf.Rad2Deg;
        _transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
    }

    private void Rotate()
    {
        if (Mathf.Approximately(_rotationDirection.sqrMagnitude, 0))
        {
            return;
        }
        Quaternion lookRotation = Quaternion.LookRotation(_rotationDirection);
        _rigidbody.rotation = lookRotation;
    }

    #endregion

    #region Private
    private NavMeshAgent nav;
    private Animator anim;

    private Rigidbody _rigidbody;
    private Transform _transform;

    private Vector3 _movementDirection;
    private Vector3 _rotationDirection;
    private float x;
    private float z;
    private float velocitySpeed;

    #endregion
}
