using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofScript : MonoBehaviour
{
    public static Dictionary<GameObject, RoofScript> buildingInstances = new Dictionary<GameObject, RoofScript>();
    #region Show in inspector

    [SerializeField] GameObject roof;
    public bool isInBuilding;

    #endregion
    private void Awake()
    {
        buildingInstances.Add(gameObject, this);
    }
    void Start()
    {
        roof.SetActive(true);
    }

    #region Private functions

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            roof.SetActive(false);
            isInBuilding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        roof.SetActive(true);
        isInBuilding = false;
    }

    #endregion
}
