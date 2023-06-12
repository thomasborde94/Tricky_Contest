using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballThrow : MonoBehaviour
{
    [SerializeField] private Baseball_ball _ballPrefab;
    [SerializeField] private Transform _hand;
    [SerializeField] private Transform _empty;

    private float _ballSpeed;
    [SerializeField] private float _destroyTime = 5f;

    private float _timeBetweenThrows = 3.5f;
    private float _currentTime;
    private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _currentTime = 0f;
    }

    void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _timeBetweenThrows)
        {
            _anim.SetTrigger("Throw");
            _currentTime = 0f;
        }
    }

    public void Fireball()
    {
        Baseball_ball newBall = Instantiate(_ballPrefab, _hand.position, _empty.rotation);
        _ballSpeed = Random.Range(15, 25);
        newBall.Shoot(_ballSpeed);

        Destroy(newBall.gameObject, _destroyTime);
    }
}
