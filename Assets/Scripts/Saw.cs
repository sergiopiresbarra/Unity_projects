using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public float speed;
    public float moveTime;
    private bool dirRight = true;
    private float timer;

    //private AudioSource sound;

    public static Saw instance;

    void Start() {
         //sound = GetComponent<AudioSource>();
         instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(dirRight)
        {
            //se verdadeiro, serra vai para direita
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else{
            //se falso, serra vai para a esquerda
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        timer += Time.deltaTime;
        if(timer >= moveTime){
            dirRight = !dirRight;
            timer = 0f;
        }
    }

    public void SoundSaw(){
        SoundManager.PlaySound(SoundManager.Sound.Saw);
    }
}
