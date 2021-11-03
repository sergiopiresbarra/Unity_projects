using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_door : MonoBehaviour
{
    public Transform[] pos;
    public float speed = 2f;

    //bool abaixando = false;
    bool levantando = false;

    bool aux = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!aux && Button.pressed && !levantando){aux =true; SoundManager.PlaySound(SoundManager.Sound.stoneOnStone, transform.position, 7.8f);CameraShake.instance.ShakeCamera(0.3f, 7.9f);}
            if(aux || transform.position.y <= pos[1].position.y){
                transform.Translate(Vector2.up * Time.deltaTime * speed);
            }
        if(transform.position.y <= pos[1].position.y+0.05f){
            //Debug.Log("tocou fundo");
            levantando = false;
            
        }
        else{
            //Debug.Log("levantado");
            levantando =true;
        }

        if(transform.position.y >= pos[0].position.y-0.05f){
            //Debug.Log("tocou teto");
            //abaixando = false;
            if(!Button.pressed){
            aux = false;
            }
            //SoundManager.PlaySound(SoundManager.Sound.stoneOnStone, 8f);
        }
        else{
            //Debug.Log("abaixando");
            //abaixando = true;
        }

        //Debug.Log(levantando);
        //Debug.Log(tocouTeto);

        if(!aux || transform.position.y >= pos[0].position.y){
            transform.Translate(Vector2.down * Time.deltaTime * speed);
             
        }


    }
}
