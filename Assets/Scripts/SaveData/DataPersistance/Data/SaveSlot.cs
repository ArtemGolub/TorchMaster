using UnityEngine;
using UnityEngine.UI;

public class SaveSlot: MonoBehaviour
{
    [Header("Profile")] 
    [SerializeField] private string ProfileID = "";

    [Header("Content")] 
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;

    private Button saveSlotButton;

    private void Awake()
    {
        saveSlotButton = GetComponent<Button>();
    }

    public void SetData(GameData data)
    {
        if (data == null)
        {
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
        }
        else
        {
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);
        }
    }

    public string GetProfileID()
    {
        return this.ProfileID;
    }

    public void SetInteractible(bool interactible)
    {
        saveSlotButton.interactable = interactible;
    }
}