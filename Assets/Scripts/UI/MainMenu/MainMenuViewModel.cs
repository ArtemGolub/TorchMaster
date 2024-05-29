using System;
using UnityEngine;

public class MainMenuViewModel
{
    private MainMenuModel _model;
    private Canvas _menuCanvas;
    private Canvas _levelsCanvas;
    private Canvas _upgradesCanvas;
    private Canvas _returnCanvas;
    private Canvas _settingCanvas;
    private Canvas _purchaseCanvas;
    
    public MainMenuViewModel(MainMenuModel model, Canvas menuCanvas, Canvas levelsCanvas, Canvas upgradesCanvas
    , Canvas returnCanvas, Canvas settingCanvas, Canvas purchaseCanvas)
    {
        _model = model;
        _menuCanvas = menuCanvas;
        _levelsCanvas = levelsCanvas;
        _upgradesCanvas = upgradesCanvas;
        _returnCanvas = returnCanvas;
        _settingCanvas = settingCanvas;
        _purchaseCanvas = purchaseCanvas;
    }
    public void OnLevelsClick()
    {
        AudioManager.current.PlaySFX(SoundType.ButtonClick);
        _model.DisableCanvas(_menuCanvas);
        _model.EnableCanvas(_levelsCanvas);
        _model.EnableCanvas(_returnCanvas);
    }

    public void OnLevelClick(string levelName, LevelState state, LoadingScreenController loadingScreenController)
    {
        AudioManager.current.PlaySFX(SoundType.ButtonClick);
        if (state == LevelState.Locked)
        {
            return;
        }
        _model.DisableCanvas(_menuCanvas);
        _model.DisableCanvas(_levelsCanvas);
        _model.LoadLevel(levelName, loadingScreenController);
    }

    public void OnExitClick()
    {
        _model.ExitGame();
    }

    public void OnUpgradesClick()
    {
        AudioManager.current.PlaySFX(SoundType.ButtonClick);
        _model.DisableCanvas(_menuCanvas);
        _model.EnableCanvas(_upgradesCanvas);
        _model.EnableCanvas(_returnCanvas);
    }

    public void OnReturnClick()
    {
        AudioManager.current.PlaySFX(SoundType.ButtonClick);
        _model.EnableCanvas(_menuCanvas);
        _model.DisableCanvas(_upgradesCanvas);
        _model.DisableCanvas(_settingCanvas);
        _model.DisableCanvas(_returnCanvas);
        _model.DisableCanvas(_levelsCanvas);
        _model.DisableCanvas(_purchaseCanvas);
    }

    public void OnSettingsClick()
    {
        AudioManager.current.PlaySFX(SoundType.ButtonClick);
        _model.DisableCanvas(_menuCanvas);
        _model.EnableCanvas(_settingCanvas);
        _model.EnableCanvas(_returnCanvas);
    }

    public void OnPurchaseClick()
    {        
        AudioManager.current.PlaySFX(SoundType.ButtonClick);
        _model.DisableCanvas(_menuCanvas);
        _model.EnableCanvas(_purchaseCanvas);
        _model.EnableCanvas(_returnCanvas);
    }
}