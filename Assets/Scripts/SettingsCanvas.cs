using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsCanvas : MonoBehaviour, IPause
{
    [SerializeField]private GameManager GameManager;
    
    [SerializeField]private RectTransform settingsHolder;
    
    [SerializeField]private Button SettingsButton;
    [SerializeField]private Button RestartButton;
    [SerializeField]private Button MenuButton;
    [SerializeField] private Button SoundSettingsButton;
    [SerializeField] private Button ReturnButton;
    
    [SerializeField] private Canvas SettingCanvas;
    [SerializeField] private Canvas soundSettingCanvas;
    [SerializeField] private Canvas returnCanvas;
    
    public LoadingScreenController LoadingScreenController = null;
    
    public bool isPause { get; set; }
    public void Pause()
    {
        if (isPause)
        {
            settingsHolder.gameObject.SetActive(false);
            isPause = false;
        }
        else
        {
            settingsHolder.gameObject.SetActive(true);
            isPause = true;
        }
    }
    private void Start()
    {
        this.LoadingScreenController = LoadingScreenController.current;
        
        SettingsButton.onClick.AddListener(Settings);
        RestartButton.onClick.AddListener(Restart);
        MenuButton.onClick.AddListener(Menu);
        SoundSettingsButton.onClick.AddListener(Sounds);
        ReturnButton.onClick.AddListener(ReturnFromSounds);
        
        settingsHolder.gameObject.SetActive(false);
        SettingCanvas = transform.GetComponent<Canvas>();  
    }

    public void EnableCanvas(bool isEnable)
    {
        SettingCanvas.enabled = isEnable;
    }
    void Settings()
    {
        AudioManager.current.PlaySFX(SoundType.ButtonClick);
        GameManager.PauseGame();
    }


    void Restart()
    {
        AudioManager.current.PlaySFX(SoundType.ButtonClick);
        if (LoadingScreenController != null)
        {
            LoadingScreenController.LoadNewScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.LogWarning("LoadingScreenController is not initialized. Skipping scene reload.");
        }
    }

    void Sounds()
    {
        AudioManager.current.PlaySFX(SoundType.ButtonClick);
        soundSettingCanvas.enabled = true;
        EnableCanvas(false);
        returnCanvas.enabled = true;
        SettingsButton.gameObject.SetActive(false);
    }
    
    void ReturnFromSounds()
    {
        AudioManager.current.PlaySFX(SoundType.ButtonClick);
        soundSettingCanvas.enabled = false;
        EnableCanvas(true);
        returnCanvas.enabled = false;
        SettingsButton.gameObject.SetActive(true);
    }
    void Menu()
    {
        AudioManager.current.PlaySFX(SoundType.ButtonClick);
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
