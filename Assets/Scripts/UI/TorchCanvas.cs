using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TorchCanvas : MonoBehaviour
{
    public static TorchCanvas current;
    public Slider slider;
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
}
