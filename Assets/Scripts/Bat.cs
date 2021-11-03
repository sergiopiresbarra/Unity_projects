using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public Transform leftCol;
    public Transform rightCol;
    private Rigidbody2D rig;
    public LayerMask layer;
    private Animator anim;

    public float speed;

    private bool colliding;

    private bool grito = false;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        if(gameObject.GetComponent<FlyEnemy>().isMoving){
            anim.SetBool("sleep", false);
            if(!grito){
                SoundManager.PlaySound(SoundManager.Sound.bat, 0.1f);
                grito = true;
            }
        }
        else{
            anim.SetBool("sleep", true);
            grito = false;
        }

        colliding = Physics2D.Linecast(rightCol.position, leftCol.position, layer);
        
        if(!colliding){
            rig.velocity = new Vector2(rig.velocity.x, speed);
            //anim.SetBool("sleep", false);
            //speed *= -1f;
        }
        else{
            rig.velocity = new Vector2(0f, 0f);
            //anim.SetBool("sleep", true);
        }

    }

    // Update is called once per frame
    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            Player.instance.SubLife(1);
        }
    }
}
