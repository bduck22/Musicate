using FMOD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class tmp : MonoBehaviour
{
    data_script data;
    public AudioMixer MasterMixer;
    public Slider audioSlider;
    float sound;
    void Awake()
    {
                data = GameObject.FindWithTag("save").GetComponent<data_script>();
        sound = data.sound;
        audioSlider.value = sound;
    }
    public void AudioControl()
    {
        sound = audioSlider.value;
        if (sound == -40f) MasterMixer.SetFloat("Master", -80f);
        else MasterMixer.SetFloat("Master", sound);
        data.sound = audioSlider.value;
        StartCoroutine(GameObject.FindFirstObjectByType<save_test>().save_key_setting());
    }
}