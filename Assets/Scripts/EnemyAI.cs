using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;

    private SpriteRenderer sprite;

    public GameObject damageText;

    public enum EnemyType{
        Slime,
        Frog,
        Bat,
    }

    public EnemyType enemyType;
    public int vida;

    //private bool destroido=false;


    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(vida <= 0 && !destroido){
            //Invoke("DestroyBody", .5f);
            Kill(enemyType);
            destroido = true;
            Invoke("DestroyBody", .5f);

        }
        */
    }

    public void TakeDamage(int damage){
        vida -= damage;
        EnemyHit(damage);
        StartCoroutine(blinkRED());
        if(vida <= 0){
            //Invoke("DestroyBody", .5f);
            Kill(enemyType);
            Invoke("DestroyBody", .1f);
        }

    }

    IEnumerator blinkRED(){
        sprite.color = Color.red;
        yield return new WaitForSeconds (0.05f);
        sprite.color = Color.white;
    }

    private void DestroyBody(){
        if(enemyType == EnemyType.Slime){Player.instance.Speed = PlayerPrefs.GetFloat("playerSpeedValue");}
        Destroy(gameObject);
    }

    public void EnemyHit(int damage){
        if(damageText != null){
            var hit = Instantiate(damageText, transform.position, Quaternion.identity);
            hit.SendMessage("SetText", "-"+damage.ToString());
        }
    }
    public void ShowText(string text){
        if(damageText != null){
            var hit = Instantiate(damageText, transform.position, Quaternion.identity);
            hit.SendMessage("SetText", text);
        }
    }

    public void Kill(EnemyType enemyType){
        switch(enemyType){
            default:
                break;
            case EnemyType.Slime:
                animator.SetTrigger("die");
                Player.instance.Speed = PlayerPrefs.GetFloat("playerSpeedValue");
                break;
            case EnemyType.Frog:
                SoundManager.PlaySound(SoundManager.Sound.frog, gameObject.transform.position);
                animator.SetTrigger("die");
                break;
        }
    }
}
