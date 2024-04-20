using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    public Canvas menuCanvas;
    public Canvas levelsCanvas;
    
    public Button btnLevels;
    public Button btnExit;

    public Button btnLevel1;
    public Button btnLevel2;
    
    private MainMenuModel _model;
    private MainMenuViewModel _viewModel;

    private void Start()
    {
        Init();
        AddListeners();
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
        
        btnLevel1.onClick.AddListener(() => _viewModel.OnLevelClick("Prototype2"));
        btnLevel2.onClick.AddListener(() => _viewModel.OnLevelClick("Prototype1"));
    }
}
