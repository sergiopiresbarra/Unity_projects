using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBird : MonoBehaviour
{
    //private AudioSource sound;
    public static BlueBird instance;

    public Transform headPoint;
    // Start is called before the first frame update
    void Start()
    {
        //sound = GetComponent<AudioSource>();
        instance = this;
    }

    // Update is called once per frame

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Player"){
            float height = col.contacts[0].point.y - headPoint.position.y;
            //Debug.Log(height);
            if(height > 0){
                //CameraShake.instance.ShakeCamera(5f, 0.1f);
                SoundManager.PlaySound(SoundManager.Sound.Bemtevi);
            }
        }
    }
}
