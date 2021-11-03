using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShimaBoss : MonoBehaviour
{
    private Rigidbody2D rig;
    public float speed;
    public Transform rightCol;
    public Transform leftCol;
    public LayerMask layer;
    private bool colliding;
    //private AudioSource sound;
    public static ShimaBoss instance;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        instance = this;
        //sound = GetComponent<AudioSource>();
    }

    void Update()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);

        colliding = Physics2D.Linecast(rightCol.position, leftCol.position, layer);
        
        if(colliding){
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed *= -1f;
        }
    }

    public void ShimaSound(){
        SoundManager.PlaySound(SoundManager.Sound.HarukoShima);
    }
}
