using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    public CanvasGroup canvasGroup;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    public Text Description;

    private Player player;

    private bool checkinventory = false;

    private bool auxiliar = true;
    PlayerInput playerInput;

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        Hide();
        auxiliar = true;
    }

    public void SetPlayer(Player player){
        this.player = player;
    }

    public void SetInventory(Inventory inventory){
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e){
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems(){
        foreach (Transform child in itemSlotContainer)
        {
            if(child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 110f;
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            
            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () => {
                // Use item
                inventory.UseItem(item);
            };
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () => {
                //drop item
                Item duplicateItem = new Item{itemType = item.itemType, amount = item.amount};
                if(item.itemType == Item.ItemType.ammo){
                    Player.instance.ammomunicao = false;
                }
                if(item.itemType == Item.ItemType.grenade){
                    Player.instance.grenademunicao = false;
                }
                inventory.RemoveItem(item);
                ItemWorld.DropItem(player.GetPosition(), duplicateItem);
            };
            itemSlotRectTransform.GetComponent<Button_UI>().MouseOverOnceFunc = () =>{
                Description.text = item.GetDescription();
            };
            
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
           
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI uiText = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();
            if(item.amount > 1){
                uiText.SetText(item.amount.ToString());
            }else{
                uiText.SetText("");
            }

            x++;
            if(x > 4){
                x = 0;
                y--;
            }
        }
    }

       private void Update() {
        if((Input.GetKeyDown(KeyCode.E) || playerInput.Inventario()) && GameController.instance.canShowInventory){
            if(!checkinventory){
                //transform.position = new Vector3(0,0);
                Show();
                checkinventory = true;
                Player.instance.canShoot = false;
            }
            else{
                //transform.position = new Vector3(240,550);
                Hide();
                checkinventory = false;
                Player.instance.canShoot = true;
            }
        }
        if(!GameController.instance.canShowInventory && auxiliar){
            Hide();
            auxiliar = false;
        }
    }

    void Hide() {
        canvasGroup.alpha = 0f; //this makes everything transparent
        canvasGroup.blocksRaycasts = false; //this prevents the UI element to receive input events
    }
    void Show() {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

}
