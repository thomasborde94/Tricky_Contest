using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baseball_ball : MonoBehaviour
{
    #region Private

    private Rigidbody _rigidbody;
    private Transform _transform;
    private float _ballSpeed;

    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        Vector3 ballVelocity = transform.forward * _ballSpeed;
        Vector3 currentBallPosition = _transform.position;
        Vector3 ballPosition = currentBallPosition + ballVelocity * Time.fixedDeltaTime;
        _rigidbody.MovePosition(ballPosition);
    }
    #endregion

    public void Shoot(float Speed)
    {
        _ballSpeed = Speed;
    }
}
