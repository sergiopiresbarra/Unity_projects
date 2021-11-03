using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //public int health;
    public float distanceAttack;
    public float speed;
    public bool isMoving = false;
    protected Rigidbody2D rb2d;
    protected Animator anim;
    protected Transform player;
    protected SpriteRenderer sprite;
    // Start is called before the first frame update
   void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
        anim  = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<Transform>();    
   }

    // Update is called once per frame
   protected float PlayerDistance(){
       if(player != null){
       return Vector2.Distance(player.position, transform.position);
       }
       return distanceAttack+1f;
   }

   protected void Flip(){
       sprite.flipX = !sprite.flipX;
       speed *= -1;
   }

    protected virtual void Update(){
        float distance = PlayerDistance();
        isMoving = (distance <= distanceAttack);

        if(isMoving){
            if((player.position.x > transform.position.x && sprite.flipX) || 
            (player.position.x < transform.position.x && !sprite.flipX)){
                Flip();
            }
        }
    }

}
