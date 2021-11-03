using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAudio : MonoBehaviour
{
    public float volumeMaster, volumeFx, volumeMusic;

    public Text masterText, fxText, musicText;

    public Slider sliderMaster, sliderFx, sliderMusic;

    private void Start() {
        if(PlayerPrefs.HasKey("volumeMaster")){
            sliderMaster.value = PlayerPrefs.GetFloat("volumeMaster");
            AudioListener.volume = PlayerPrefs.GetFloat("volumeMaster");
            masterText.text = ((float)(Math.Truncate((double)PlayerPrefs.GetFloat("volumeMaster")*100.0))).ToString();
            }else{
                PlayerPrefs.SetFloat("volumeMaster", 1f);
                sliderMaster.value = PlayerPrefs.GetFloat("volumeMaster");
                AudioListener.volume = 1f;
                masterText.text = ((float)(Math.Truncate((double)PlayerPrefs.GetFloat("volumeMaster")*100.0))).ToString();
            }

        if(PlayerPrefs.HasKey("volumeFx")){
            sliderFx.value = PlayerPrefs.GetFloat("volumeFx");
            GameObject[] Fxs = GameObject.FindGameObjectsWithTag("Effect");
            for(int i=0; i<Fxs.Length; i++){
                Fxs[i].GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("volumeFx");
            }
             fxText.text =  ((float)(Math.Truncate((double)PlayerPrefs.GetFloat("volumeFx")*100.0))).ToString();
        }else{
            PlayerPrefs.SetFloat("volumeFx", 1f);
            sliderFx.value = PlayerPrefs.GetFloat("volumeFx");
            GameObject[] Fxs = GameObject.FindGameObjectsWithTag("Effect");
            for(int i=0; i<Fxs.Length; i++){
                Fxs[i].GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("volumeFx");
            }
            fxText.text =  ((float)(Math.Truncate((double)PlayerPrefs.GetFloat("volumeFx")*100.0))).ToString();
        }
        if(PlayerPrefs.HasKey("volumeMusic")){
            sliderMusic.value = PlayerPrefs.GetFloat("volumeMusic");
            GameObject[] Music = GameObject.FindGameObjectsWithTag("Music");
            for(int i=0; i<Music.Length; i++){
                Music[i].GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("volumeMusic");
            }
            musicText.text =  ((float)(Math.Truncate((double)PlayerPrefs.GetFloat("volumeMusic")*100.0))).ToString();
        }else{
            PlayerPrefs.SetFloat("volumeMusic", 1f);
            sliderMusic.value = PlayerPrefs.GetFloat("volumeMusic");
            GameObject[] Music = GameObject.FindGameObjectsWithTag("Music");
            for(int i=0; i<Music.Length; i++){
                Music[i].GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("volumeMusic");
            }
            musicText.text =  ((float)(Math.Truncate((double)PlayerPrefs.GetFloat("volumeMusic")*100.0))).ToString();
        }
    }
    public void VolumeMaster(float volume){
        volumeMaster = volume;
        AudioListener.volume = volumeMaster;
        PlayerPrefs.SetFloat("volumeMaster", volumeMaster);
        masterText.text = ((float)(Math.Truncate((double)volumeMaster*100.0))).ToString();
    }
    public void VolumeFx(float volume){
        volumeFx = volume;
        PlayerPrefs.SetFloat("volumeFx", volumeFx);
        GameObject[] Fxs = GameObject.FindGameObjectsWithTag("Effect");
        for(int i=0; i<Fxs.Length; i++){
            Fxs[i].GetComponent<AudioSource>().volume = volumeFx;
        }
        fxText.text =  ((float)(Math.Truncate((double)volumeFx*100.0))).ToString();
    }
    public void VolumeMusic(float volume){
        volumeMusic = volume;
        PlayerPrefs.SetFloat("volumeMusic", volumeMusic);
        GameObject[] Music = GameObject.FindGameObjectsWithTag("Music");
        for(int i=0; i<Music.Length; i++){
            Music[i].GetComponent<AudioSource>().volume = volumeMusic;
        }
        musicText.text =  ((float)(Math.Truncate((double)volumeMusic*100.0))).ToString();
    }

}
