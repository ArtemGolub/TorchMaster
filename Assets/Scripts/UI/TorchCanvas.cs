using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TorchCanvas : MonoBehaviour, IInitialize
{
    public static TorchCanvas current;
    public Slider slider;
    public TextMeshProUGUI torchCountText;
    private Player player;
    public bool isInit { get; set; }
    private void Awake()
    {
        current = this;
    }
    public void Init()
    {
        isInit = true;
        player = FindObjectOfType<Player>();
    }
    public void UpdateSlider(float value)
    {
        if(!isInit) return;
        if(player.Character.SM.StateCondition(CharacterStateType.Death)) return;
        ActivateSlider();
        slider.value = value;
        if (slider.value == 0)
        {
            DeactivateSlider();
        }
    }

    public void ActivateSlider()
    {
        if(GetComponent<Canvas>().enabled) return;
        GetComponent<Canvas>().enabled = true;
    }

    public void DeactivateSlider()
    {
        if(!GetComponent<Canvas>().enabled) return;
        GetComponent<Canvas>().enabled = false;
    }
    
    public void InitSlider(float torchValue)
    {
        slider.minValue = 0;
        slider.maxValue = torchValue;
        slider.value = torchValue;
    }
    
    public void UpdateTorchCount(int torchCount, int inventoryCapacity)
    {
        torchCountText.text = $"{torchCount} / {inventoryCapacity}";
        if (torchCount == 0)
        {
            if(player == null) return;
            if(player.Character == null) return;
            if(player.Character.Components == null) return;
            if(player.Character.Components.animator == null) return;
            player.Character.Components.animator.SetLayerWeight(1, 0);
        }
    }

}
