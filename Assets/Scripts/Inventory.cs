using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Inventory
{

  public event EventHandler OnItemListChanged;  
  private List<Item> itemList;
  private Action<Item> useItemAction;

  int x = 0;
  string y;

  bool dadosCarregados = true;
  List<Item> InventarioInicioFase = new List<Item>();

  List<string> itemaux = new List<string> {
        "pizza",
        "apple",
        "banana",
        "kiwi",
        "ammo",
        "armor",
        "blaster",
        "grenade",
        "gun",
        "knife",
        "grenadeLauncher",
        "goiaba",
        };

  public Inventory(Action<Item> useItemAction){
      this.useItemAction = useItemAction;
      itemList = new List<Item>();

        if(SceneManager.GetActiveScene().buildIndex == 1){
            foreach (string i in itemaux)
            {
            if(PlayerPrefs.HasKey(i)){
                    PlayerPrefs.SetInt(i, 0);
                    //Debug.Log(i);
                }
            }
        }
        //Debug.Log("executou!!");

        //string foo = "bar";
        //var resources = ;
        //object o = resources.GetType().GetProperty(foo).GetValue(resources, null);
        
            
        
        for (int o = 0; o < itemaux.Count; o++)
            {
                string tipo = itemaux[o];
                switch(tipo){
                    case "blaster":
                    case "grenadeLauncher":
                    case "gun":
                    case "knife":
                        if(PlayerPrefs.HasKey(tipo)){
                            if(PlayerPrefs.GetInt(tipo) > 0){
                            int aux = PlayerPrefs.GetInt(tipo);
                                for (int i = 0; i < aux; i++)
                                {
                                AddItem(new Item{ itemType = (Item.ItemType)o, amount = 1});
                                }
                            }
                        }
                        break;
                    default:
                        if(PlayerPrefs.HasKey(tipo)){
                            if(PlayerPrefs.GetInt(tipo) > 0){ 
                            AddItem(new Item{ itemType = (Item.ItemType)o, amount = PlayerPrefs.GetInt(tipo)});
                            }
                        }
                        break;
                }
            }
        
        /*
        if (PlayerPrefs.HasKey("banana")){
         if(PlayerPrefs.GetInt("banana") > 0){
             int aux = PlayerPrefs.GetInt("banana");////////////!isStackable
             for (int i = 0; i < aux; i++)
             {
                 AddItem(new Item{ itemType = Item.ItemType.banana, amount = 1});
             }
         }
      }
      if(PlayerPrefs.HasKey("blaster")){
         if(PlayerPrefs.GetInt("blaster") > 0){
             int aux = PlayerPrefs.GetInt("blaster");
             for (int i = 0; i < aux; i++)
             {
                 AddItem(new Item{ itemType = Item.ItemType.blaster, amount = 1});
             }
         }
      }
      if(PlayerPrefs.HasKey("grenadeLauncher")){
         if(PlayerPrefs.GetInt("grenadeLauncher") > 0){
             int aux = PlayerPrefs.GetInt("grenadeLauncher");
             for (int i = 0; i < aux; i++)
             {
                 AddItem(new Item{ itemType = Item.ItemType.grenadeLauncher, amount = 1});
             }
         }
      }
      if(PlayerPrefs.HasKey("gun")){
         if(PlayerPrefs.GetInt("gun") > 0){
             int aux = PlayerPrefs.GetInt("gun");
             for (int i = 0; i < aux; i++)
             {
                 AddItem(new Item{ itemType = Item.ItemType.gun, amount = 1});
             }
         }
      }
      if(PlayerPrefs.HasKey("knife")){
         if(PlayerPrefs.GetInt("knife") > 0){
             int aux = PlayerPrefs.GetInt("knife");
             for (int i = 0; i < aux; i++)
             {
                 AddItem(new Item{ itemType = Item.ItemType.knife, amount = 1});
             }
         }
      }

      if(PlayerPrefs.HasKey("kiwi")){
         if(PlayerPrefs.GetInt("kiwi") > 0){ 
            AddItem(new Item{ itemType = Item.ItemType.kiwi, amount = PlayerPrefs.GetInt("kiwi")});
         }
      }
      if(PlayerPrefs.HasKey("apple")){
         if(PlayerPrefs.GetInt("apple") > 0){ 
            AddItem(new Item{ itemType = Item.ItemType.apple, amount = PlayerPrefs.GetInt("apple")});
         }
      }
      if(PlayerPrefs.HasKey("pizza")){
         if(PlayerPrefs.GetInt("pizza") > 0){ 
            AddItem(new Item{ itemType = Item.ItemType.pizza, amount = PlayerPrefs.GetInt("pizza")});
         }
      }
      if(PlayerPrefs.HasKey("ammo")){
         if(PlayerPrefs.GetInt("ammo") > 0){ 
            AddItem(new Item{ itemType = Item.ItemType.ammo, amount = PlayerPrefs.GetInt("ammo")});
         }
      }
      if(PlayerPrefs.HasKey("armor")){
         if(PlayerPrefs.GetInt("armor") > 0){ 
            AddItem(new Item{ itemType = Item.ItemType.armor, amount = PlayerPrefs.GetInt("armor")});
         }
      }
      if(PlayerPrefs.HasKey("grenade")){
         if(PlayerPrefs.GetInt("grenade") > 0){ 
            AddItem(new Item{ itemType = Item.ItemType.grenade, amount = PlayerPrefs.GetInt("grenade")});
         }
      }
    */

      dadosCarregados = false;
        //InventarioInicioFase = itemList;
        InventarioInicioFase = itemList.ToList();
        //InventarioInicioFase.AddRange(itemList);
        
        
        //InventarioInicioFase.AddRange(itemList);
        //Debug.Log("inventarioInicioFase:");
        foreach (Item item in InventarioInicioFase)
        {
            
            string a = item.itemType.ToString();
            if(PlayerPrefs.HasKey(a+"aux")){};
            int z = PlayerPrefs.GetInt(a);

            PlayerPrefs.SetInt(a+"aux",z);

            //Debug.Log(item.amount);
        }
        //InventarioInicioFase.AddRange(itemList);
        //AddItem(new Item{ itemType = Item.ItemType.apple, amount = 1});
        //AddItem(new Item{ itemType = Item.ItemType.banana, amount = 1});
        //AddItem(new Item{ itemType = Item.ItemType.banana, amount = 1});
        //AddItem(new Item{ itemType = Item.ItemType.banana, amount = 1});
  }


  public void AddItem(Item item){
      y = item.itemType.ToString();
      if(PlayerPrefs.HasKey(y)){}
      x = PlayerPrefs.GetInt(y);
      if(!dadosCarregados){
          x += item.amount;
          }
      //x += item.amount;
      //Debug.Log("Add");
      //Debug.Log(x);
      //Debug.Log(y);
      PlayerPrefs.SetInt(y,x);
      //---------------------


      if(item.IsStackable()){
        bool itemAlreadyInInventory = false;
        foreach (Item inventoryItem in itemList)
        {
            if(inventoryItem.itemType == item.itemType){
                inventoryItem.amount += item.amount;
                itemAlreadyInInventory = true;
            }
        }
        if(!itemAlreadyInInventory){
            itemList.Add(item);
        }
      }else{
        itemList.Add(item);
      }
      OnItemListChanged?.Invoke(this, EventArgs.Empty);
  }

  public List<Item> GetItemList(){
      //Debug.Log(itemList);
      return itemList;
  }

  public void RemoveItem(Item item){
      y = item.itemType.ToString();
      if(PlayerPrefs.HasKey(y)){}
      x = PlayerPrefs.GetInt(y);
      if(!dadosCarregados){
          x -= item.amount;
          }
      //x -= item.amount;
      //Debug.Log("Remove");
      //Debug.Log(x);
      //Debug.Log(y);
      PlayerPrefs.SetInt(y,x);

      if(item.IsStackable()){
        Item itemInInventory = null;
        foreach (Item inventoryItem in itemList)
        {
            if(inventoryItem.itemType == item.itemType){
                inventoryItem.amount -= item.amount;
                itemInInventory = inventoryItem;
            }
        }
        if(itemInInventory != null && itemInInventory.amount <= 0){
            itemList.Remove(itemInInventory);
        }
      }else{
        itemList.Remove(item);
      }
      OnItemListChanged?.Invoke(this, EventArgs.Empty);
  }
  public void UseItem(Item item) {
        useItemAction(item);
    }

    public void GetInventarioInicialFase(){
        //itemList = InventarioInicioFase;
        //=======zerar playerprefs========
            foreach (string i in itemaux)
            {
            if(PlayerPrefs.HasKey(i)){
                    PlayerPrefs.SetInt(i, 0);
                    //Debug.Log(i);
                }
            }
        //=================================
                //Debug.Log("entrou getInventarioFase");
        
        foreach (Item item in InventarioInicioFase)
        {
                y = item.itemType.ToString();
                //x = PlayerPrefs.GetInt(y);
                //if(!dadosCarregados){
                //x = item.amount;
                //}
                
                PlayerPrefs.SetInt(y,PlayerPrefs.GetInt(y+"aux"));
                //Debug.Log(PlayerPrefs.GetInt(y));
        }
            //Debug.Log("saiu getInventarioFase");

    }
}
