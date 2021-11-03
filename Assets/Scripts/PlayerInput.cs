using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInput : MonoBehaviour
{

    private struct PlayerInputConstants{
        public const string Horizontal = "Horizontal";
        public const string Vertical = "Vertical";
        public const string Jump = "Jump";

        public const string Attack = "Attack";
        public const string Inventario = "Inventario";

        public const string Grab = "Grab";
    }
   public Vector2 GetMovementInput(){
       //input teclado
       float horizontalInput = Input.GetAxisRaw(PlayerInputConstants.Horizontal);

        //se o input do teclado for zero, tentamos ler o input do celular
       if(Mathf.Approximately(horizontalInput, 0.0f)){
           horizontalInput = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Horizontal);
       }
       return new Vector2(horizontalInput * Player.instance.Speed, Player.instance.rig.velocity.y);
   }

   public bool IsJumpButtonDown(){
       bool isKeyboardButtondown = Input.GetKeyDown(KeyCode.Space);
       bool isMobileButtonDown = CrossPlatformInputManager.GetButtonDown(PlayerInputConstants.Jump);
       return isKeyboardButtondown || isMobileButtonDown;
       
   }
   public bool IsShooting(){
       //bool isMouseClick = Input.GetMouseButtonDown(0);
       bool isMobileButtonDown = CrossPlatformInputManager.GetButtonDown(PlayerInputConstants.Attack);
       return isMobileButtonDown; // || isMouseClick;
   }
    public bool Inventario(){
       bool isMobileButtonDown = CrossPlatformInputManager.GetButtonDown(PlayerInputConstants.Inventario);
       return isMobileButtonDown;
   }
   public bool Grab(){
       bool isMobileButtonDown = CrossPlatformInputManager.GetButtonDown(PlayerInputConstants.Grab);
       bool isKeyboardButtondown = Input.GetKeyDown(KeyCode.Return);
       return isMobileButtonDown || isKeyboardButtondown;
   }


   public bool IsJumpButtonHeld(){
       bool isKeyboardButtonHeld = Input.GetKey(KeyCode.Space);
       bool isMobileButtonHeld = CrossPlatformInputManager.GetButton(PlayerInputConstants.Jump);
       return isKeyboardButtonHeld || isMobileButtonHeld;
       
   }
   public bool IsButtonDown(){
       bool isKeyboardButtondown = Input.GetKeyDown(KeyCode.S);
       bool isMobileButtonDown = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Vertical) < 0;
       return isKeyboardButtondown || isMobileButtonDown;
   }
   public bool IsButtonUp(){
       bool isKeyboardButtonUp = Input.GetKey(KeyCode.S) == false;
       bool isMobileButtonUp = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Vertical) >=0;
       return isKeyboardButtonUp && isMobileButtonUp;
   }

}
