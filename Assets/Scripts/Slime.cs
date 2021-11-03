using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private Rigidbody2D rig;
    //private Animator anim;
    public float speed;
    public Transform rightCol;
    public float TimeEffect;
    private float timer;
    public Transform leftCol;
    //private AudioSource sound;

    private bool colliding;

    private bool c=false;
    // Start is called before the first frame update
    private Animator anim;
    public LayerMask layer;

    public static Slime instance;

    private bool tocou = false;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        instance = this;
        //transform.eulerAngles = new Vector3(0f, 180f, 0f);
        //sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        rig.velocity = new Vector2(-speed, rig.velocity.y);

        colliding = Physics2D.Linecast(rightCol.position, leftCol.position, layer);
        
        if(colliding){
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed *= -1f;
            if(!tocou){
                SoundManager.PlaySound(SoundManager.Sound.slime, transform.position ,0.1f);
            }
            tocou = !tocou;
        }

        timer += Time.deltaTime;
        if(timer >= TimeEffect){
            if(c){
            Player.instance.Speed = PlayerPrefs.GetFloat("playerSpeedValue");
            timer = 0f;
            anim.SetTrigger("die");
            Destroy(gameObject, 0.5f);
            }
        }

    }

    void OnCollisionEnter2D(Collision2D col){
        if(c==false){
            if(col.gameObject.tag == "Player"){
                //Debug.Log(height);
                Player.instance.Speed = 1f;
                timer = 0f;
                c = true;
                Player.instance.SubLife(1f);
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3, ForceMode2D.Impulse);
            }
        }
        else{
            if(col.gameObject.tag == "Player"){
                timer = 0f;
                Player.instance.SubLife(1f);
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3, ForceMode2D.Impulse);
            }
        }

    }

    void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
                //timer = 0f;
                Player.instance.SubLife(1f);
                //other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3, ForceMode2D.Impulse);
            }
    }

}
