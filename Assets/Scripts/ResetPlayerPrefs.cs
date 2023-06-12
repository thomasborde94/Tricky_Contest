using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetPlayerPrefs : MonoBehaviour
{
    [SerializeField] private string sceneNameToResetPrefs = "Launching Scene";

    private void Start()
    {
        // Check if the initial scene is the one to reset PlayerPrefs
        if (SceneManager.GetActiveScene().name == sceneNameToResetPrefs)
        {
            Debug.Log("PlayerPrefs resetted");
            PlayerPrefs.DeleteAll();
        }

        // Subscribe to the sceneLoaded event for subsequent scene changes
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == sceneNameToResetPrefs)
        {
            Debug.Log("PlayerPrefs resetted");
            PlayerPrefs.DeleteAll();
        }
    }
}
