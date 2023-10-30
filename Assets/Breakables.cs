using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    #region Show in Inspector

    [SerializeField] private bool _shouldDropPieces;
    [SerializeField] private bool _shouldDropGold;
    [SerializeField] private bool _shouldDrop50;
    [SerializeField] private float _piecesDropPercent;
    [SerializeField] private GameObject _piece;
    [SerializeField] private GameObject _goldBar;
    [SerializeField] private ParticleSystem _particles;


    #endregion

    #region Private

    private Transform _transform;

    #endregion
    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }
    #region Private Methods

    private void OnTriggerEnter(Collider other)
    {
        // Triggers Smash when players kicks gameobject
        if (other.CompareTag("Kick"))
        {
            Smash();
            SFXManager.instance.PlaySFX(2);
        }
    }

    /// <summary>Destroys gameobject and gives loot to player </summary>
    private void Smash()
    {
        // Instantiates particles, then destroy them
        ParticleSystem particleInstance =  Instantiate(_particles, _transform.position, _transform.rotation);
        Destroy(particleInstance.gameObject, 1.5f);

        gameObject.SetActive(false);
        Destroy(gameObject);

        if (_shouldDropPieces)
        {
            float dropChance = Random.Range(0f, 100f);
            if (dropChance < _piecesDropPercent)
            {
                Instantiate(_piece, _transform.position, _transform.rotation);
            }
        }

        if (_shouldDropGold)
        {
            float dropChance = Random.Range(0f, 100f);
            if (dropChance < _piecesDropPercent)
            {
                Instantiate(_goldBar, _transform.position, _goldBar.transform.rotation);
            }
        }

        if (_shouldDrop50)
        {
            LevelManaging.instance.Get50();
        }
    }
    #endregion
}
