using UnityEngine;
using UnityEngine.UI;

public class MadnessCanvas: MonoBehaviour
{
    public static MadnessCanvas current;
    public Slider slider;
    private void Start()
    {
        current = this;
        InitSlider(100);
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
    
    public void InitSlider(float madnessValue)
    {
        slider.minValue =0;
        slider.maxValue = madnessValue;
        slider.value = madnessValue;
    }
}