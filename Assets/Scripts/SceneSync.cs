using UnityEngine;
using UnityEngine.SceneManagement;
using Coherence.Toolkit;

[RequireComponent(typeof(CoherenceSync))]
public class SceneSync : MonoBehaviour
{
    private CoherenceSync sync;

    [Sync] private string currentScene; 

    private bool isLoading = false;

    void Awake()
    {
        sync = GetComponent<CoherenceSync>();
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        if (sync.HasStateAuthority)
            currentScene = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        if (!sync.HasStateAuthority)
        {
            string activeScene = SceneManager.GetActiveScene().name;
            if (!isLoading && activeScene != currentScene)
            {
                isLoading = true;
                SceneManager.LoadScene(currentScene);
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (sync.HasStateAuthority)
        {
            currentScene = scene.name;
        }
        isLoading = false;
    }
}
