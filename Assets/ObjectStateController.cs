using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStateController : MonoBehaviour
{
    #region Private

    private const string ObjectStateKeyPrefix = "ObjectState_";
    private string objectStateKey;

    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        // Generate a unique key for each object based on its name
        objectStateKey = ObjectStateKeyPrefix + gameObject.name;
    }
    void Start()
    {
        // Check if the object was previously set to inactive
        bool isObjectInactive = PlayerPrefs.GetInt(objectStateKey, 0) == 1;

        // Set the object's active state based on the saved value
        gameObject.SetActive(!isObjectInactive);
    }
    #endregion

    private void OnDestroy()
    {
        // Save the current active state of the object
        int objectState = gameObject.activeSelf ? 0 : 1;
        PlayerPrefs.SetInt(objectStateKey, objectState);
        PlayerPrefs.Save();
    }
}
