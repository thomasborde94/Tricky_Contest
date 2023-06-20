using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    [SerializeField] IntVariable _shootingPoints;

    private Animator _anim;
    private float _maxEnabledTime = 3f;
    private float _timeEnabled = 0f;

    public bool isEnabled = false;

    #region Unity Lifecycle

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isEnabled)
        {
            // If not shot, target disables automatically
            _timeEnabled += Time.deltaTime;
            if (_timeEnabled >= _maxEnabledTime)
                DisableTarget();
        }
    }

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (isEnabled && other.CompareTag("Bullet") && ShootingManager.instance._currentTimeRemaining > 0)
        {
            Destroy(other.gameObject);
            DisableTarget();
            _shootingPoints.Value++;
            SFXManager.instance.PlaySFX(0);
        }
    }

    #region Public Methods

    public void EnableTarget()
    {
        isEnabled = true;
        _anim.SetBool("isEnabled", true);
        _timeEnabled = 0f;
    }

    public void DisableTarget()
    {
        isEnabled = false;
        _anim.SetBool("isEnabled", false);
        _timeEnabled = 0f;
    }

    #endregion
}
