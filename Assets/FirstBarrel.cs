using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBarrel : MonoBehaviour
{
    [SerializeField] private GameObject _pressA;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _pressA.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _pressA.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        if (_pressA != null)
            _pressA.SetActive(false);
    }
}
