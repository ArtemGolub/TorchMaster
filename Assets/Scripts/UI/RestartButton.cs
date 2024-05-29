using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    public static RestartButton current;
    
    public Canvas restartCanvas;
    public Button restartButton;
    
    public Button toMainMenu;

    public LoadingScreenController LoadingScreenController = null;

    
    [SerializeField] private SettingsCanvas _settingsCanvas;

    private void Start()
    {
        current = this;
        restartButton.onClick.AddListener(RestartGame);
        toMainMenu.onClick.AddListener(ToMainMenu);
        
        this.LoadingScreenController = FindObjectOfType<LoadingScreenController>();

    }

    public void OpenRestartCanvas()
    {
        restartCanvas.enabled = true;
        _settingsCanvas.EnableCanvas(false);
    }
    
    private void RestartGame()
    {
        AudioManager.current.PlaySFX(SoundType.ButtonClick);
        // Проверяем наличие LoadingScreenController перед загрузкой сцены
        if (LoadingScreenController != null)
        {
            LoadingScreenController.LoadNewScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.LogWarning("LoadingScreenController is not initialized. Skipping scene reload.");
        }
    }

    private void ToMainMenu()
    {
        AudioManager.current.PlaySFX(SoundType.ButtonClick);
        // Проверяем наличие LoadingScreenController перед загрузкой сцены
        if (LoadingScreenController != null)
        {
            LoadingScreenController.LoadNewScene("MainMenu");
        }
        else
        {
            Debug.LogWarning("LoadingScreenController is not initialized. Skipping scene reload.");
        }
    }


}
