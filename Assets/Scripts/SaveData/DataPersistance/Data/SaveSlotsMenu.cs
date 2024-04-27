using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSlotsMenu : MonoBehaviour
{
    private SaveSlot[] _saveSlots;
    private bool _isLoadingGame;
    private void Awake()
    {
        _saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }

    private void ActivateMenu(bool isLoadingGame)
    {
        Dictionary<string, GameData> profilesGameData = DataPersistanceManager.current.GetAllProfilesData();
        _isLoadingGame = isLoadingGame;
        foreach (SaveSlot saveSlot in _saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileID(), out profileData);
            saveSlot.SetData(profileData);

            if (profileData == null && _isLoadingGame)
            {
                saveSlot.SetInteractible(false);
            }
            else
            {
                saveSlot.SetInteractible(true);
            }
        }
    }

    public void OnSaveSlotClicked(SaveSlot slot)
    {
        DisableMenuButtons();
        
        
        DataPersistanceManager.current.ChangeSelectedProfileID(slot.GetProfileID());
        if (!_isLoadingGame)
        {
            DataPersistanceManager.current.NewGame();
        }
       
        SceneManager.LoadSceneAsync("Level1");
    }

    private void DisableMenuButtons()
    {
        foreach (SaveSlot slot in _saveSlots)
        {
            slot.SetInteractible(false);
        }
    }
}