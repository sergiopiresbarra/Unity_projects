using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushRoom : MonoBehaviour
{
    private Animator anim;
    private Animator collectedAnim;
    public float TimeEffect;
    private float aux=0;
    public float totalAmountEffect;
    private float timer;
    //private AudioSource sound;
    private bool colliding;

    private SpriteRenderer sr;

    private bool c=false;
    // Start is called before the first frame update

    void Start()
    {
        //rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //collectedAnim = GetComponentInChildren<Animator>();
        collectedAnim =gameObject.transform.GetChild(0).GetComponent<Animator>();
        sr = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        //transform.eulerAngles = new Vector3(0f, 180f, 0f);
        //sound = GetComponent<AudioSource>();
        sr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if((timer >= TimeEffect) && (aux < totalAmountEffect)){
            if(c){
            Player.instance.SubLife(2);
            CameraShake.instance.ShakeCamera(5,0.1f);
            timer = 0f;
            sr.enabled = false;
            //anim.SetTrigger("die");
            //Destroy(gameObject, 0.5f);
            aux++;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        if(c==false){
            if(col.gameObject.tag == "Player"){
                //Debug.Log(height);
                //Player.instance.Speed = 1f;
                timer = 0f;
                c = true;
                aux = 0f;
                sr.enabled = true;
                collectedAnim.SetTrigger("hit");
                //anim.SetTrigger("hit");
                //Player.instance.SubLife(1f);
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3, ForceMode2D.Impulse);
            }
        }
        else{
            if(col.gameObject.tag == "Player"){
                timer = 0f;
                aux = 0f;
                sr.enabled = true;
                collectedAnim.SetTrigger("hit");
                //anim.SetTrigger("hit");
                //Player.instance.SubLife(1f);
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3, ForceMode2D.Impulse);
            }
        }
    }

    void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
                //timer = 0f;
                //Player.instance.SubLife(1f);
                //other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3, ForceMode2D.Impulse);
            }
    }
}
