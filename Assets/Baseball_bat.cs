using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baseball_bat : MonoBehaviour
{
    public static Baseball_bat instance;
    [SerializeField] private Transform _empty;
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private IntVariable _playerScoreBaseball;

    private void Start()
    {
        instance = this;
    }

    /// <summary>Happens when the player hits the ball with the bat</summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball") && BaseballManager.instance._currentTimeRemaining > 0)
        {
            SFXManager.instance.PlaySFX(3);
            Destroy(other.gameObject);
            _playerScoreBaseball.Value++;
            ParticleSystem particleInstance = Instantiate(_particles, _empty.position, _empty.rotation);
            Destroy(particleInstance.gameObject, 1.5f);
        }
    }

}
