using UnityEngine;

public class MainMenuViewModel
{
    private MainMenuModel _model;
    private Canvas _menuCanvas;
    private Canvas _levelsCanvas;

    public MainMenuViewModel(MainMenuModel model, Canvas menuCanvas, Canvas levelsCanvas)
    {
        _model = model;
        _menuCanvas = menuCanvas;
        _levelsCanvas = levelsCanvas;
    }
    public void OnLevelsClick()
    {
        _model.DisableCanvas(_menuCanvas);
        _model.EnableCanvas(_levelsCanvas);
    }

    public void OnLevelClick(string levelName)
    {
        _model.LoadLevel(levelName);
    }

    public void OnExitClick()
    {
        _model.ExitGame();
    }
}