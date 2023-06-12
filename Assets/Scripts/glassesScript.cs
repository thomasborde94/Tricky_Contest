using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class glassesScript : MonoBehaviour
{
    public static glassesScript instance;
    public BoolVariable _glassesCollected;
    [SerializeField] private GameObject _hintBox;
    [SerializeField] private Text _hintMessage;
    private void Awake()
    {
        instance = this;
        _hintBox.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerPickup"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CollectedGlasses();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerPickup"))
        {
            _hintBox.SetActive(false);
            Destroy(gameObject);
        }
    }


    private void CollectedGlasses()
    {
        _glassesCollected.Value = true;
        DataManager.instance._enemyScoreShooting.Value -= 5;
        _hintBox.SetActive(true);
        _hintMessage.text = "You stole Max's glasses.";
    }
}
