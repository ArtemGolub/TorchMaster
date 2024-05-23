using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TorchCanvas : MonoBehaviour
{
    public static TorchCanvas current;
    public Slider slider;
    public TextMeshProUGUI torchCountText;
    
    private void Awake()
    {
        current = this;
    }
    
    public void UpdateSlider(float value)
    {
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
            var player = FindObjectOfType<Player>();
            if(player == null) return;
            if(player.Character == null) return;
            if(player.Character.Components == null) return;
            if(player.Character.Components.animator == null) return;
            player.Character.Components.animator.SetLayerWeight(1, 0);
        }
    }
}
