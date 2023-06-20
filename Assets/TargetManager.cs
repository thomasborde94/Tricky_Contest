using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    #region Show in Inspector

    [SerializeField] private float _maxTimeBetweenTargets = 2f;
    [SerializeField] private float _minTimeBetweenTargets = 0.5f;
    [SerializeField] private ShootingManager shootingManager;

    #endregion

    #region Private

    private float _timeSinceLastAnim;
    private TargetScript[] _targets;

    #endregion

    #region Unity Lifecycle
    void Start()
    {
        _targets = GetComponentsInChildren<TargetScript>();
    }

    void Update()
    {
        if (!LevelManaging.instance._isPaused)
            Invoke("Targets", shootingManager._delay);
    }
    #endregion

    private void Targets()
    {
        _timeSinceLastAnim += Time.deltaTime;

        // Enables a target randomly
        if (_timeSinceLastAnim >= _timeBetweenTargets && shootingManager._currentTimeRemaining > 0)
        {
            int randomIndex = Random.Range(0, _targets.Length);
            TargetScript target = _targets[randomIndex];

            if (!target.isEnabled)
                target.EnableTarget();
            _timeSinceLastAnim = 0f;

        }
    }

    float _timeBetweenTargets
    {
        get { return Random.Range(_minTimeBetweenTargets, _maxTimeBetweenTargets); }
    }
}
