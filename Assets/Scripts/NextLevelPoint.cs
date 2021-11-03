using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPoint : MonoBehaviour
{
    
    //public string lvlName;
   void OnCollisionEnter2D(Collision2D collision){
       if(collision.gameObject.tag == "Player"){
           GameController.instance.ShowNextLevel();
           //Player.instance.DestroyPlayer();
           Player.instance.Speed = 0;
           Player.instance.circleCollider2D.enabled = false;
           Player.instance.rig.bodyType = RigidbodyType2D.Static;
           Player.instance.rig.bodyType = RigidbodyType2D.Kinematic;
           if(Player.instance.ammomunicao){
                PlayerPrefs.SetInt("ammomunicao", 1);
           }
           else{
               PlayerPrefs.SetInt("ammomunicao", 0);
           }
           if(Player.instance.grenademunicao){
            PlayerPrefs.SetInt("grenademunicao", 1);
           }
           else{
               PlayerPrefs.SetInt("grenademunicao", 0);
           }
           PlayerPrefs.SetInt("tipeWeapon", Player.instance.tipeWeapon);

           Player.instance.canShoot = false;
           GameController.instance.canShowInventory = false;
           //SceneManager.LoadScene(lvlName);
       }
   }
}
