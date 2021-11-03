using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;
    public float speed;
    public Transform rightCol;
    public Transform leftCol;
    public Transform headPoint;
    //private AudioSource sound;

    private bool colliding;
    // Start is called before the first frame update

    public LayerMask layer;

    public int vida =10;

    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circleCollider2D;

    public static Frog instance;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        instance = this;
        //sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);

        colliding = Physics2D.Linecast(rightCol.position, leftCol.position, layer);
        
        if(colliding){
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed *= -1f;
        }

    }


    bool playerDestroyed = false;
    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Player"){
            float height = col.contacts[0].point.y - headPoint.position.y;
            //Debug.Log(height);
            if(height > 0 && !playerDestroyed){
                //CameraShake.instance.ShakeCamera(5f, 0.1f);
                SoundManager.PlaySound(SoundManager.Sound.frog, gameObject.transform.position);
                vida--;
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3, ForceMode2D.Impulse);
                if(vida <= 0){
                    speed = 0;
                    anim.SetTrigger("die");
                    boxCollider2D.enabled = false;
                    circleCollider2D.enabled = false;
                    rig.bodyType = RigidbodyType2D.Static;
                    rig.bodyType = RigidbodyType2D.Kinematic;
                    CameraShake.instance.ShakeCamera(5f, 0.1f);
                    col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3, ForceMode2D.Impulse);
                    Destroy(gameObject, 0.33f);
                }
            }
            else{
                //playerDestroyed = true;
                //GameController.instance.ShowGameOver();
                Player.instance.SubLife(30);
                //col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1,0) * 10, ForceMode2D.Impulse);
                Player.instance.Knockback();
                CameraShake.instance.ShakeCamera(5f, 0.1f);
                //Destroy(col.gameObject);
            }
        }
        if(col.gameObject.tag == "boss"){
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 4, ForceMode2D.Impulse);
        }
    }

    public void DestroyFrog(){
        speed = 0;
        anim.SetTrigger("die");
        SoundManager.PlaySound(SoundManager.Sound.frog, gameObject.transform.position);
        boxCollider2D.enabled = false;
        circleCollider2D.enabled = false;
        rig.bodyType = RigidbodyType2D.Static;
        rig.bodyType = RigidbodyType2D.Kinematic;
        CameraShake.instance.ShakeCamera(5f, 0.1f);
        Destroy(gameObject, 0.33f);
    }

}
