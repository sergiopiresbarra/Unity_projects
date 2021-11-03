using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour
{
    public int dano;
    public float tempoDeVida;
    public float distancia;
    public LayerMask layerInimigo;

    public bool isGrenade = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestruirProjetil", tempoDeVida);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.forward, distancia, layerInimigo);

        if(hitInfo.collider != null){
            if(hitInfo.collider.CompareTag("Enemy")){
                hitInfo.collider.GetComponent<EnemyAI>().TakeDamage(Player.instance.dano);
            }
            DestruirProjetil();
        }
    }

    void DestruirProjetil(){
        if(!isGrenade){
        Destroy(gameObject);
        }
        else{
            gameObject.GetComponent<bombScript>().explode();
        }
    }

    public void finishProjetil(){
        Destroy(gameObject);
    }

}
