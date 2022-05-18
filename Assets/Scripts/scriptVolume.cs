using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scriptVolume : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;

    void Start(){
        slider.value = PlayerPrefs.GetFloat("volum", 0.5f);
        AudioListener.volume = slider.value;
    }

    public void ChangeSlider(float value){
        PlayerPrefs.SetFloat("volum", value);
        AudioListener.volume = slider.value;

        Debug.Log(slider.value);
    }

}
