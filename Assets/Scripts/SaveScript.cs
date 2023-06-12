using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour
{
    #region Public properties

    public static int pchar = 0;
    public static string pname = "player";
    public static GameObject spawnPoint;
    public static GameObject theTarget;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this); 
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
}
