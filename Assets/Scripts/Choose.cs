using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Choose : MonoBehaviour
{

    [SerializeField] GameObject[] characters;
    public Text playerName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    #region Public Functions
    public void Next()
    {
        if (p < characters.Length - 1)
        {
            characters[p].SetActive(false);
            p++;
            characters[p].SetActive(true);
        }
    }

    public void Back()
    {
        if (p > 0)
        {
            characters[p].SetActive(false);
            p--;
            characters[p].SetActive(true);
        }
    }

    public void Accept()
    {
        SaveScript.pchar = p;
        SaveScript.pname = playerName.text;
        SceneManager.LoadScene(1);
    }

    #endregion

    #region Private

    private int p = 0;

    #endregion
}
