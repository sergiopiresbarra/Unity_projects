using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Item[] itemList;
    private Animator anim;

    private bool collected= false;

    PlayerInput playerInput;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInput != null){
            if(playerInput.Grab()){
                if(anim.GetBool("open")==true){
                    if(!collected){
                        gameObject.GetComponent<EnemyAI>().ShowText("collected!");
                        foreach (Item item in itemList)
                        {
                            ItemWorld.DropItemUpPosition(transform.position+new Vector3(0,1), item);
                        }
                        collected = true;
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
                gameObject.GetComponent<EnemyAI>().ShowText("Enter");
                anim.SetBool("open", true);
                SoundManager.PlaySound(SoundManager.Sound.ChestOpen);
        }

    }
    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
               anim.SetBool("open", false);
        }
    }
}
