using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPannel : MonoBehaviour {
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider effectSlider;
    private	void Start () {
        musicSlider.value = AudioManager.Instance.MusicValume;
        effectSlider.value = AudioManager.Instance.EffectValume;
	}
	
	     
    private	void OnMusicSlider (float valume)
    {
        AudioManager.Instance.MusicValume = valume;
	}
    private void OnEffectSlider(float valume)
    {
        AudioManager.Instance.EffectValume  = valume;
    }


}
