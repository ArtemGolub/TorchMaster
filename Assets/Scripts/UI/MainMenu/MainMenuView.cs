using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    public Canvas menuCanvas;
    public Canvas levelsCanvas;
    
    public Button btnLevels;
    public Button btnExit;

    public List<Button> LevelButtons;
    
    private MainMenuModel _model;
    private MainMenuViewModel _viewModel;
    public LoadingScreenController LoadingScreenController = null;

    private void Awake()
    {
        // Переместите инициализацию LoadingScreenController сюда из Start
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
    }

    private void Init()
    {
        _model = new MainMenuModel();
        _viewModel = new MainMenuViewModel(_model, menuCanvas, levelsCanvas);
    }

    private void AddListeners()
    {
        btnLevels.onClick.AddListener(_viewModel.OnLevelsClick);
        btnExit.onClick.AddListener(_viewModel.OnExitClick);

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
}
