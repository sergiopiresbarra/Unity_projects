using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    [SerializeField] private UI_Inventory uiInventory;

    public float Speed;

    //private bool estaAtirando = false;

    private float tempoUltimoTiro;

    public Transform armaParado;

    public Transform armaAndando;

    public GameObject projetilPrefab;

    public float velocidadeProjetil;
    public Rigidbody2D rig; //padrao private
    private Animator anim;
    public float JumpForce;

    private int nroPulos =1;
    public bool isJumping = false;
    bool isOnFloor = false;

    private bool isTouchingWall;
    public Transform wallCheck;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    private bool isWallSliding;

    private int facingDirection = 1; //usar 1 se o personagem inicializa ja olhando pra direita

    public float wallJumpForce;
    public Vector2 wallJumpDirection;

    public float wallSlideSpeed;

    private bool canMove = true;

    public bool canShoot = true;

    private bool facingRight=true;
    public float radius = 0.2f;
    public Slider life;
    //public bool doubleJumping;
    //int extraJumps = 1;

    private float movement;

    public float wallCheckDistance;

    public CircleCollider2D circleCollider2D;

    //bool isBlowing;

    public static Player instance;

    private Inventory inventory; //padrao private

    public GameObject gun;
    public GameObject blaster;
    public GameObject grenadeLauncher;

    public int tipeWeapon = 0;

    public int dano = 2;

    public bool ammomunicao = false;
    public bool grenademunicao = false;

    public GameObject messageText;

    private SpriteRenderer sprite; 

    private float nextSpawn = 0f;
    private float spawnRate = 6f;

    private float auxiliar  = 0;

    PlayerInput playerInput;
    
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        life.value = PlayerPrefs.GetFloat("vida");
        canShoot = true;
        instance = this;
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        Speed = PlayerPrefs.GetFloat("playerSpeedValue");
        wallJumpForce = PlayerPrefs.GetFloat("wallJumpValue");

        inventory = new Inventory(UseItem);
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);

        if(PlayerPrefs.HasKey("ammomunicao")){}
        if(PlayerPrefs.HasKey("grenademunicao")){}
        if(PlayerPrefs.GetInt("ammomunicao") == 1){
            ammomunicao = true;
        }
        if(PlayerPrefs.GetInt("grenademunicao") == 1){
            grenademunicao = true;
        }
        if(PlayerPrefs.HasKey("tipeWeapon")){}
        tipeWeapon = PlayerPrefs.GetInt("tipeWeapon");
        switch(tipeWeapon){
            default:
                break;
            case 0:
                gun.SetActive(false);
                blaster.SetActive(false);
                grenadeLauncher.SetActive(false);
                break;
            case 1:
                gun.SetActive(true);
                blaster.SetActive(false);
                grenadeLauncher.SetActive(false);
                break;
            case 2:
                gun.SetActive(false);
                blaster.SetActive(true);
                grenadeLauncher.SetActive(false);
                dano = 10;
                break;
            case 3:
                gun.SetActive(false);
                blaster.SetActive(false);
                grenadeLauncher.SetActive(true);
                break;
        }
       // ItemWorld.SpawnItemWorld(new Vector3(2, 2), new Item {itemType = Item.ItemType.apple, amount =1});
       // ItemWorld.SpawnItemWorld(new Vector3(-2, 2), new Item {itemType = Item.ItemType.banana, amount =1});
       // ItemWorld.SpawnItemWorld(new Vector3(0, -2), new Item {itemType = Item.ItemType.kiwi, amount =1});
    }

    // Update is called once per frame
    void Update()
    {
        CheckSurroundings();
        CheckWallSliding();
        CheckShoot();
        CheckDropItem();

        CheckInput();
        CheckJump();

        if(SceneManager.GetActiveScene().buildIndex ==1){
            Tutorial();
        }
        

    }

    public Inventory GetInventory(){
        return inventory;
    }

    public Vector3 GetPosition(){
        return transform.position;
    }
//=================================Efeito dos Itens=========================================
    private void UseItem(Item item){
        switch(item.itemType){
            case Item.ItemType.apple:
                Player.instance.SubLife(5);
                inventory.RemoveItem(new Item {itemType = Item.ItemType.apple, amount = 1});
                break;
            case Item.ItemType.goiaba:
                Player.instance.SubLife(100);
                SoundManager.PlaySound(SoundManager.Sound.goiaba);
                inventory.RemoveItem(new Item {itemType = Item.ItemType.goiaba, amount = 1});
                break;
            case Item.ItemType.banana:
                //inventory.RemoveItem(new Item {itemType = Item.ItemType.banana, amount = 1});
                inventory.RemoveItem(new Item {itemType = Item.ItemType.banana, amount = 1}); //nao empilhavel item
                Player.instance.Jump();
                break;
            case Item.ItemType.pizza:
                inventory.RemoveItem(new Item {itemType = Item.ItemType.pizza, amount = 1});
                Player.instance.AddLife(5);
                break;
            case Item.ItemType.gun:
                //canShoot = true;
                velocidadeProjetil = 5;
                dano = 2;
                switch(tipeWeapon){
                    case 0:
                        inventory.RemoveItem(item);
                        gun.SetActive(true);
                        blaster.SetActive(false);
                        grenadeLauncher.SetActive(false);
                        tipeWeapon = 1;
                        break;
                    case 1:
                        break;
                    case 2:
                        inventory.RemoveItem(item);
                        gun.SetActive(true);
                        blaster.SetActive(false);
                        grenadeLauncher.SetActive(false);
                        inventory.AddItem(new Item{ itemType = Item.ItemType.blaster, amount = 1});
                        tipeWeapon = 1;
                        break;
                    case 3:
                        inventory.RemoveItem(item);
                        gun.SetActive(true);
                        blaster.SetActive(false);
                        grenadeLauncher.SetActive(false);
                        inventory.AddItem(new Item{ itemType = Item.ItemType.grenadeLauncher, amount = 1});
                        tipeWeapon = 1;
                        break;
                }
                break;
            case Item.ItemType.blaster:
                //canShoot = true;
                velocidadeProjetil = 15;
                dano = 10;
                switch(tipeWeapon){
                    case 0:
                        inventory.RemoveItem(item);
                        gun.SetActive(false);
                        blaster.SetActive(true);
                        grenadeLauncher.SetActive(false);
                        tipeWeapon = 2;
                        break;
                    case 1:
                        inventory.RemoveItem(item);
                        gun.SetActive(false);
                        blaster.SetActive(true);
                        grenadeLauncher.SetActive(false);
                        inventory.AddItem(new Item{ itemType = Item.ItemType.gun, amount = 1});
                        tipeWeapon = 2;
                        break;
                    case 2:
                        break;
                    case 3:
                        inventory.RemoveItem(item);
                        gun.SetActive(false);
                        blaster.SetActive(true);
                        grenadeLauncher.SetActive(false);
                        inventory.AddItem(new Item{ itemType = Item.ItemType.grenadeLauncher, amount = 1});
                        tipeWeapon = 2;
                        break;
                }
                break;
            case Item.ItemType.grenadeLauncher:
                //canShoot = true;
                velocidadeProjetil = 10;
                switch(tipeWeapon){
                    case 0:
                        inventory.RemoveItem(item);
                        gun.SetActive(false);
                        blaster.SetActive(false);
                        grenadeLauncher.SetActive(true);
                        tipeWeapon = 3;
                        break;
                    case 1:
                        inventory.RemoveItem(item);
                        gun.SetActive(false);
                        blaster.SetActive(false);
                        grenadeLauncher.SetActive(true);
                        inventory.AddItem(new Item{ itemType = Item.ItemType.gun, amount = 1});
                        tipeWeapon = 3;
                        break;
                    case 2:
                        inventory.RemoveItem(item);
                        gun.SetActive(false);
                        blaster.SetActive(false);
                        grenadeLauncher.SetActive(true);
                        inventory.AddItem(new Item{ itemType = Item.ItemType.blaster, amount = 1});
                        tipeWeapon = 3;
                        break;
                    case 3:
                        break;
                }
                break;

        }
    }

    IEnumerator StopMove(){
        //retira o controlhe do personagem
        canMove = false;
        //inverte o lado do transform
        transform.localScale = transform.localScale.x == 1 ? new Vector2(-3, 3) : new Vector2(3, 3);

        yield return new WaitForSeconds(.3f);

        //normaliza o lado do transform
        transform.localScale = new Vector2(3, 3);
        //devolve o controlhe do personagem
        canMove = true;
    }

    void Jump(){
        //pulo normal
        if(nroPulos > 0 && !isWallSliding){
            nroPulos--;
            rig.velocity = Vector2.zero;
            rig.AddForce(Vector2.up * JumpForce);
            //anim.SetBool("jump", true);
            SoundManager.PlaySound(SoundManager.Sound.PlayerJump);
        }

        //wall jump
        if(isWallSliding){
            //x = força *x* (-1 ou 1  - esquerda ou direita)
            //y = força * y (sempre pra cima)
            Vector2 force = new Vector2(wallJumpForce * wallJumpDirection.x * -facingDirection, wallJumpForce * wallJumpDirection.y);

            //para a velocidade antes de atribuir, para nao ocorrer acumulo de velocidades
            rig.velocity = Vector2.zero;

            //adiciona a força do wall jump
            rig.AddForce(force, ForceMode2D.Impulse);
            
            SoundManager.PlaySound(SoundManager.Sound.PlayerJump);

            //retira temporariamente o controlhe do personagem
            StartCoroutine("StopMove");
        }
    }

    private void CheckWallSliding(){
        if(isTouchingWall && !isOnFloor && rig.velocity.y < 0 && movement !=0)
        {
            isWallSliding = true;
        }
        else{
            isWallSliding = false;
        }
    }

    void CheckSurroundings(){
        //verifica se esta no chão
        isOnFloor = Physics2D.OverlapCircle(groundCheck.position, radius, whatIsGround);
        anim.SetBool("estaNoChao", isOnFloor);
        //verifica se está encostado na parede
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }

    void CheckShoot(){
        if(canShoot){
            if(tipeWeapon == 1 || tipeWeapon ==2){
                if(ammomunicao){
                    Atirar();
                }
                else{
                     if(playerInput.IsShooting()){
                        SoundManager.PlaySound(SoundManager.Sound.offammo);
                     }
                }
            }
            if(tipeWeapon == 3){
                if(grenademunicao){
                    Atirar();
                    
                }
                else{
                     if(playerInput.IsShooting()){
                        SoundManager.PlaySound(SoundManager.Sound.offammo);
                     }
                }
            }
            

        }
    }

    void CheckDropItem(){
        if(Input.GetKeyDown(KeyCode.Q)){
            switch (tipeWeapon)
            {
                default:
                    break;
                case 1:
                    ItemWorld.DropItem(transform.position, new Item{itemType = Item.ItemType.gun, amount = 1});
                    tipeWeapon = 0;
                    gun.SetActive(false);
                    blaster.SetActive(false);
                    grenadeLauncher.SetActive(false);
                    break;
                case 2:
                    ItemWorld.DropItem(transform.position, new Item{itemType = Item.ItemType.blaster, amount = 1});
                    tipeWeapon = 0;
                    gun.SetActive(false);
                    blaster.SetActive(false);
                    grenadeLauncher.SetActive(false);
                    break;
                case 3:
                    ItemWorld.DropItem(transform.position, new Item{itemType = Item.ItemType.grenadeLauncher, amount = 1});
                    tipeWeapon = 0;
                    gun.SetActive(false);
                    blaster.SetActive(false);
                    grenadeLauncher.SetActive(false);
                    break;
            }
        }
    }

    void CheckInput(){
        if(canMove){
            Move();
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }
        if(playerInput.IsJumpButtonDown()){
            Jump();
        }
    }

    void CheckJump(){
        if(isOnFloor){
            //anim.SetBool("jump", false);
            nroPulos = 1;
        }
    }

    /*
    void FixedUpdate() {
        if(isJumping){
            rig.velocity = new Vector2 (rig.velocity.x, 0f);
            rig.AddForce(new Vector2(0f, JumpForce));
            isJumping = false;
        }
    }*/

    void Move(){
        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);

        //move o personagem em uma posição
        //transform.position += movement * Time.deltaTime * Speed;
        //movement = Input.GetAxisRaw("Horizontal");
        

        Vector2 movementInput = playerInput.GetMovementInput();//Adicionado!
        movement = movementInput.x;

        rig.velocity = movementInput;//new Vector2(movement * Speed, rig.velocity.y);
        anim.SetFloat("velocidade", Mathf.Abs(movement));
        anim.SetFloat("velocidadeY", rig.velocity.y);

        /* if(movementInput.x > 0){
            sprite.flipX = false;
            
        }else if(movementInput.x <0){
            sprite.flipX = true;
            
        } */
        /*
        if(movement > 0f){
            if(isWallSliding)
                anim.SetBool("jump", true);
            else
                anim.SetBool("walk", true);
            //transform.eulerAngles = new Vector3(0f,0f,0f);
            //facingRight =true;
            //facingDirection = 1;//flip
        }
        if(movement < 0f){
            if(isWallSliding)
                anim.SetBool("jump", true);
            else
                anim.SetBool("walk", true);
            //transform.eulerAngles = new Vector3(0f,180f,0f);
            //facingRight = false;
            //facingDirection = -1;//flip
        }
        if(movement == 0f){
            //if(isWallSliding){
            //    anim.SetBool("jump", true);
            //}
            //else{
                anim.SetBool("walk", false);
            //}
        }
        */

        if(isWallSliding){
            if(rig.velocity.y < -wallSlideSpeed){
                rig.velocity = new Vector2(rig.velocity.x, -wallSlideSpeed);
            }
        }

        if((movement < 0 && facingRight)|| (movement >0 && !facingRight)){
            Flip();
        }
    }

    void Flip(){
        facingDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void Knockback(){
        rig.AddForce(new Vector2(10 * -facingDirection, 10), ForceMode2D.Impulse);
        StartCoroutine("Freeze");
    }

    IEnumerator Freeze(){
        //retira o controlhe do personagem
        canMove = false;

        yield return new WaitForSeconds(.5f);

        //devolve o controlhe do personagem
        canMove = true;
    }

    /*
    void Jump(){
        if(Input.GetButtonDown("Jump") && !isBlowing){
            if(!isJumping){
                 rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                 doubleJumping = true;
                 anim.SetBool("jump", true);
            }
            else{
                if(doubleJumping){
                    rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                    doubleJumping = false;
                }
            }
           
        }
    }*/
    
    void OnCollisionEnter2D(Collision2D collision){
        //circleCollider2D.enabled = false;
        //rig.bodyType = RigidbodyType2D.Kinematic;

        /*
        if(collision.gameObject.layer == 8){
            //isJumping = false;
            if(!isWallSliding){
                anim.SetBool("jump", false);
            }
            else{
                anim.SetBool("jump", true);
            }
        }
        */

        if(collision.gameObject.tag == "spike"){
            Player.instance.inventory.GetInventarioInicialFase();
            GameController.instance.ShowGameOver();
            SoundManager.PlaySound(SoundManager.Sound.PlayerDie);
            Destroy(gameObject, 0.1f);
        }
        if(collision.gameObject.tag == "saw"){
            Player.instance.inventory.GetInventarioInicialFase();
            GameController.instance.ShowGameOver();
            Saw.instance.SoundSaw();
            SoundManager.PlaySound(SoundManager.Sound.PlayerDie);
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "boss"){
            //gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 20, ForceMode2D.Impulse);
            //GameController.instance.ShowGameOver();
            ShimaBoss.instance.ShimaSound();
            //Destroy(gameObject);
        }

    }

    /*
    void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.layer == 8){
            isJumping = true;
        }
    }*/

    public void DestroyPlayer(){
        Destroy(gameObject);
    }
     public void AddLife(float vida){
         if(life.value < 100){
            life.value += vida;
         }
    }
    public void SubLife(float vida){
        if(life.value > 0){
        life.value -= vida;
        StartCoroutine(blinkRED());
        }
        if(life.value <=0){
            Player.instance.inventory.GetInventarioInicialFase();
            GameController.instance.ShowGameOver();
            SoundManager.PlaySound(SoundManager.Sound.PlayerDie);
            Destroy(gameObject);
        }
    }

    IEnumerator blinkRED(){
        sprite.color = Color.red;
        yield return new WaitForSeconds (0.05f);
        sprite.color = Color.white;
    }

    /*
    void OnTriggerStay2D(Collider2D collider){
        if(collider.gameObject.layer == 11){
          isBlowing = true;
        }
    }*/
    //=================================Coletando Itens=========================================
    void OnTriggerEnter2D(Collider2D collider){
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if(itemWorld !=null){
            //touching item
            inventory.AddItem(itemWorld.GetItem());
            if(itemWorld.GetItem().itemType == Item.ItemType.ammo){
                ammomunicao = true;
            }
            if(itemWorld.GetItem().itemType == Item.ItemType.grenade){
                grenademunicao = true;
            }
            if(itemWorld.GetItem().itemType == Item.ItemType.pizza){
                GameController.instance.totalScore += 1;
                GameController.instance.UpdateScoreText();
            }
            itemWorld.DestroySelf();
            SoundManager.PlaySound(SoundManager.Sound.CollectingItem);
        }
    }


    /*
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, radius);
    }*/

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        if(facingRight)
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        else
            Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x - wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }

    void Atirar(){
        if(playerInput.IsShooting())
        {
            if(tipeWeapon == 1 || tipeWeapon ==2){
                List<Item> inv = inventory.GetItemList();
                    foreach (Item inventoryItem in inv)
                    {
                            if(inventoryItem.itemType == Item.ItemType.ammo){
                                    if(inventoryItem.amount > 1){
                                        inventory.RemoveItem(new Item {itemType = Item.ItemType.ammo, amount = 1});
                                        continue;
                                    }
                                    if(inventoryItem.amount == 1){
                                        inventory.RemoveItem(new Item {itemType = Item.ItemType.ammo, amount = 1});
                                        ammomunicao = false;
                                        break;
                                    }
                            }
                    }
                if(tipeWeapon == 1){
                    SoundManager.PlaySound(SoundManager.Sound.GunShoot);
                }else{
                    SoundManager.PlaySound(SoundManager.Sound.blaster);
                }
            }
            if(tipeWeapon == 3){
                List<Item> inv = inventory.GetItemList();
                    foreach (Item inventoryItem in inv)
                    {
                            if(inventoryItem.itemType == Item.ItemType.grenade){
                                    if(inventoryItem.amount > 1){
                                        inventory.RemoveItem(new Item {itemType = Item.ItemType.grenade, amount = 1});
                                        continue;
                                    }
                                    if(inventoryItem.amount == 1){
                                        inventory.RemoveItem(new Item {itemType = Item.ItemType.grenade, amount = 1});
                                        grenademunicao = false;
                                        break;
                                    }
                            }
                    }
                    SoundManager.PlaySound(SoundManager.Sound.grenadeLauncher);
            }

            Transform shotPoint;

            if(Mathf.Abs(rig.velocity.x) > 0)
                shotPoint = armaAndando;
            else
                shotPoint = armaParado;
            
            GameObject projectile = Instantiate(projetilPrefab, shotPoint.position, transform.rotation);

            //estaAtirando = true;
            tempoUltimoTiro = .7f;
            if(tipeWeapon == 3){
                projectile.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                
                projectile.GetComponent<SpriteRenderer>().sprite = Item.GetSprite(Item.ItemType.grenade);

                projectile.GetComponent<Projetil>().isGrenade = true;
            }
            else{
                projectile.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            }
            if(Input.GetAxisRaw("Vertical") <= 0){
                if(facingRight)
                    projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadeProjetil, 0);
                else
                    projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(-velocidadeProjetil, 0);
            }
            else{
                 projectile.GetComponent<Transform>().transform.Rotate(0f, 0f, 90f);
                 projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, velocidadeProjetil);
            }
            
        }
        tempoUltimoTiro -= Time.deltaTime;

        //if(tempoUltimoTiro <= 0) estaAtirando = false;
    }

bool b = true;
    void Tutorial(){
        if(Time.time > nextSpawn){
            nextSpawn = Time.time + spawnRate;
            auxiliar++;
            b = false;
        }
        if(messageText != null && !b){
            
            switch(auxiliar)
            {
                //var hit = Instantiate(messageText, transform.position, Quaternion.identity);
                case 2:
                    var hit = Instantiate(messageText, transform.position, Quaternion.identity);
                    hit.SendMessage("SetText", "Use the arrow keys on the keyboard or the WASD keys to move");
                    b = true;
                    break;
                case 3:
                    var hit2 = Instantiate(messageText, transform.position, Quaternion.identity);
                    hit2.SendMessage("SetText", "Use Space key to jump");
                    b = true;
                    break;
                case 4:
                    var hit3 = Instantiate(messageText, transform.position, Quaternion.identity);
                    hit3.SendMessage("SetText", "Use E button for inventory, Q button to drop items from hand");
                    b = true;
                    break;
            }
        }
    }

}
