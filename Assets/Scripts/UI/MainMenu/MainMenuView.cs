using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    public Canvas menuCanvas;
    public Canvas levelsCanvas;
    public Canvas upgradeCanvas;
    public Canvas returnCanvas;
    public Canvas settingsCanvas;
    public Canvas purchaseCanvas;
    
    public Button btnLevels;
    public Button btnExit;
    public Button btnUpgrades;
    public Button btnSettings;
    public Button btnPurchase;
        
    public Button btnReturn;
    
    public List<Button> LevelButtons;
    
    private MainMenuModel _model;
    private MainMenuViewModel _viewModel;
    public LoadingScreenController LoadingScreenController = null;
    
    
    private void Awake()
    {
        this.LoadingScreenController = FindObjectOfType<LoadingScreenController>();
    }
    private void Start()
    {
        Init();
        AddListeners();
        if (!DataPersistanceManager.current.HasGameData())
        {
            // No data
        }
        
        AudioManager.current.PlayMainTheme();
    }

    private void Init()
    {
        _model = new MainMenuModel();
        _viewModel = new MainMenuViewModel(_model, 
            menuCanvas, levelsCanvas, upgradeCanvas, returnCanvas, settingsCanvas, purchaseCanvas);
    }

    private void AddListeners()
    {
        btnLevels.onClick.AddListener(_viewModel.OnLevelsClick);
        btnExit.onClick.AddListener(_viewModel.OnExitClick);
        btnUpgrades.onClick.AddListener(_viewModel.OnUpgradesClick);
        btnReturn.onClick.AddListener(_viewModel.OnReturnClick);
        btnSettings.onClick.AddListener(_viewModel.OnSettingsClick);
        btnPurchase.onClick.AddListener(_viewModel.OnPurchaseClick);
        
        foreach (var button in LevelButtons)
        {
            button.onClick.AddListener(() =>
            {
                var levelName = button.GetComponent<LevelProgress>().GetLevelName();
                var levelState = button.GetComponent<LevelProgress>().GetState();
                _viewModel.OnLevelClick(levelName, levelState, LoadingScreenController);
            });
        }
    }

    private void RemoveListeners()
    {
        btnLevels.onClick.RemoveListener(_viewModel.OnLevelsClick);
        btnExit.onClick.RemoveListener(_viewModel.OnExitClick);
        btnUpgrades.onClick.RemoveListener(_viewModel.OnUpgradesClick);
        btnReturn.onClick.RemoveListener(_viewModel.OnReturnClick);
        btnSettings.onClick.RemoveListener(_viewModel.OnSettingsClick);
        btnPurchase.onClick.RemoveListener(_viewModel.OnPurchaseClick);
    }
}
