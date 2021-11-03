using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombScript : MonoBehaviour
{
    public float fieldofImpact;

    public float force;

    public LayerMask layerToHit;

    public GameObject ExplosionEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void explode(){
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldofImpact, layerToHit);

        foreach (Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;

            if(obj.GetComponent<EnemyAI>() != null){
            obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
            obj.GetComponent<EnemyAI>().TakeDamage(30);
            }else{
                if(obj.GetComponent<Rigidbody2D>() !=null){
                    obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
                }
            }
            
        }
        CameraShake.instance.ShakeCamera(5f, 0.1f);
        SoundManager.PlaySound(SoundManager.Sound.GrenadeExplosion,transform.position);
        GameObject ExplosionEffectIns = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        Destroy(ExplosionEffectIns, 5);
        gameObject.GetComponent<Projetil>().finishProjetil();
        Destroy(gameObject);
    }


    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldofImpact);    
    }
}
