using System;
using UnityEngine;
using UnityEngine.UI;

public class MadnessCanvas: MonoBehaviour, IInitialize
{
    public static MadnessCanvas current;
    

    public Slider slider;
    private Player player;
    public bool isInit { get; set; }

    private void Awake()
    {
        current = this;
    }

    public void Init()
    {
        player = FindObjectOfType<Player>();
        isInit = true;
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

    private void ActivateSlider()
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