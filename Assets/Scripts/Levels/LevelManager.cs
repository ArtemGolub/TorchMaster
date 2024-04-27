using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager current { get; private set; }
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (current != null)
        {
            Debug.LogError("More than one DataPersistanceManager");
            if (current != this)
            {
                Destroy(this);
            }
        }
        current = this;
    }
    
    public void TryLoadLevel(string levelName)
    {
        
        SceneManager.LoadScene(levelName);
    }
    
}