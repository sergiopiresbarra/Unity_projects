using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound{
        PlayerJump,
        PlayerDie,
        GunShoot,
        GrenadeExplosion,
        Eating,
        CollectingItem,
        ChestOpen,
        Bemtevi,
        Saw,
        HarukoShima,
        frog,
        grenadeLauncher,
        blaster,
        offammo,
        SongEmergenceLvl3,
        goiaba,
        slime,
        bat,
        stoneOnStone,
    }

    private static Dictionary<Sound, float> soundTimerDictionary;
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;

    public static void Initialize(){
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.Bemtevi] = 0f;
        soundTimerDictionary[Sound.HarukoShima] = 0f;
        soundTimerDictionary[Sound.slime] = 0f;
        soundTimerDictionary[Sound.stoneOnStone] = 0f;
    }
    public static void PlaySound(Sound sound, Vector3 position, float t=1f, bool typeAudio=true){
        if(CanPlaySound(sound, t)){
            GameObject soundGameObject = new GameObject("Sound");
           
            soundGameObject.transform.position = position;
            
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            
            audioSource.clip = GetAudioClip(sound);
            //audioSource.volume = GetVolume(sound);
            audioSource.maxDistance = 25f;
            audioSource.spatialBlend = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.dopplerLevel = 0f;
            
            if(soundGameObject.tag == "Effect" || soundGameObject.tag == "Music"){
                
            }else{
                if(typeAudio){soundGameObject.tag="Effect"; audioSource.PlayOneShot(GetAudioClip(sound), GetVolume(sound)*PlayerPrefs.GetFloat("volumeFx"));}else{soundGameObject.tag="Music"; audioSource.PlayOneShot(GetAudioClip(sound), GetVolume(sound)*PlayerPrefs.GetFloat("volumeMusic"));}
            }

            Object.Destroy(soundGameObject, audioSource.clip.length);
        }
    }

    
    public static void PlaySound(Sound sound, float t=1f, bool typeAudio=true){
        if(CanPlaySound(sound, t)){
            GameObject soundGameObject = new GameObject("Sound");
           
            //soundGameObject.transform.position = position;
            
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            
            audioSource.clip = GetAudioClip(sound);
            //audioSource.volume = GetVolume(sound);
            //audioSource.maxDistance = 25f;
            //audioSource.spatialBlend = 1f;
            //audioSource.rolloffMode = AudioRolloffMode.Linear;
            //audioSource.dopplerLevel = 0f;
            
            if(soundGameObject.tag == "Effect" || soundGameObject.tag == "Music"){
                
            }else{
                if(typeAudio){soundGameObject.tag="Effect"; audioSource.PlayOneShot(GetAudioClip(sound), GetVolume(sound)*PlayerPrefs.GetFloat("volumeFx"));}else{soundGameObject.tag="Music"; audioSource.PlayOneShot(GetAudioClip(sound), GetVolume(sound)*PlayerPrefs.GetFloat("volumeMusic"));}
            }

            Object.Destroy(soundGameObject, audioSource.clip.length);
        }
        /*if(CanPlaySound(sound, t)){
            if(oneShotGameObject == null){
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
                
                //oneShotAudioSource.volume = GetVolume(sound);
            }

            if(oneShotGameObject.tag == "Effect" || oneShotGameObject.tag == "Music"){
                oneShotAudioSource.PlayOneShot(GetAudioClip(sound),GetVolume(sound)*PlayerPrefs.GetFloat(oneShotGameObject.tag));
            }else{
                if(typeAudio){oneShotAudioSource.PlayOneShot(GetAudioClip(sound),GetVolume(sound)*PlayerPrefs.GetFloat("volumeFx"));oneShotGameObject.tag="Effect";}else{oneShotAudioSource.PlayOneShot(GetAudioClip(sound),GetVolume(sound)*PlayerPrefs.GetFloat("volumeMusic"));oneShotGameObject.tag="Music";}
            }
        }*/
    }
    

    private static bool CanPlaySound(Sound sound, float t){
        switch(sound){
            default:
                return true;
            case Sound.Bemtevi:
            case Sound.HarukoShima:
            case Sound.slime:
            case Sound.stoneOnStone:
                if(soundTimerDictionary.ContainsKey(sound)){
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = t;
                    if(lastTimePlayed + playerMoveTimerMax < Time.time){
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }else{
                        return false;
                    }
                }else{
                    return true;
                }
        }
    }

    private static AudioClip GetAudioClip(Sound sound){
        foreach(GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray){
            if(soundAudioClip.sound == sound){
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound "+ sound + " not found!");
        return null;
    }

    private static float GetVolume(Sound sound){
        foreach(GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray){
            if(soundAudioClip.sound == sound){
                return soundAudioClip.volume;
            }
        }
        Debug.LogError("Volume of "+ sound + " sound not found!");
        return 1;
    }
}
