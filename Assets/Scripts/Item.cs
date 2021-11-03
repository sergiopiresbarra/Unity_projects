using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType{
        pizza,
        apple,
        banana,
        kiwi,
        ammo,
        armor,
        blaster,
        grenade,
        gun,
        knife,
        grenadeLauncher,
        goiaba,
    }

    public ItemType itemType;
    public int amount = 1;

    public Sprite GetSprite(){
        return GetSprite(itemType);
    }

    public static Sprite GetSprite(ItemType itemType){
        switch(itemType){
            default:
            case ItemType.apple:                return ItemAssets.Instance.appleSprite;
            case ItemType.banana:               return ItemAssets.Instance.bananaSprite;
            case ItemType.kiwi:                 return ItemAssets.Instance.kiwiSprite;
            case ItemType.pizza:                return ItemAssets.Instance.pizzaSprite;
            case ItemType.ammo:                 return ItemAssets.Instance.ammoSprite;
            case ItemType.armor:                return ItemAssets.Instance.armorSprite;
            case ItemType.blaster:              return ItemAssets.Instance.blasterSprite;
            case ItemType.grenade:              return ItemAssets.Instance.grenadeSprite;
            case ItemType.grenadeLauncher:      return ItemAssets.Instance.grenadeLauncherSprite;
            case ItemType.gun:                  return ItemAssets.Instance.gunSprite;
            case ItemType.knife:                return ItemAssets.Instance.knifeSprite;
            case ItemType.goiaba:               return ItemAssets.Instance.goiabaSprite;
        }
    }

    public string GetDescription(){
        switch(itemType){
            default:
            case ItemType.apple:     return ItemAssets.Instance.appleDescription;
            case ItemType.banana:    return ItemAssets.Instance.bananaDescription;
            case ItemType.kiwi:      return ItemAssets.Instance.kiwiDescription;
            case ItemType.pizza:     return ItemAssets.Instance.pizzaDescription;
            case ItemType.ammo:                 return ItemAssets.Instance.ammoDescription;
            case ItemType.armor:                return ItemAssets.Instance.armorDescription;
            case ItemType.blaster:              return ItemAssets.Instance.blasterDescription;
            case ItemType.grenade:              return ItemAssets.Instance.grenadeDescription;
            case ItemType.grenadeLauncher:      return ItemAssets.Instance.grenadeLauncherDescription;
            case ItemType.gun:                  return ItemAssets.Instance.gunDescription;
            case ItemType.knife:                return ItemAssets.Instance.knifeDescription;
            case ItemType.goiaba:               return ItemAssets.Instance.goiabaDescription;
        }
    }

    public string GetAnimator(){
        switch(itemType){
            default:
            case ItemType.apple:     return ItemAssets.Instance.appleAnimator;
            case ItemType.banana:    return ItemAssets.Instance.bananaAnimator;
            case ItemType.kiwi:      return ItemAssets.Instance.kiwiAnimator;
            case ItemType.pizza:     return ItemAssets.Instance.pizzaAnimator;
            case ItemType.ammo:                 return ItemAssets.Instance.ammoAnimator;
            case ItemType.armor:                return ItemAssets.Instance.armorAnimator;
            case ItemType.blaster:              return ItemAssets.Instance.blasterAnimator;
            case ItemType.grenade:              return ItemAssets.Instance.grenadeAnimator;
            case ItemType.grenadeLauncher:      return ItemAssets.Instance.grenadeLauncherAnimator;
            case ItemType.gun:                  return ItemAssets.Instance.gunAnimator;
            case ItemType.knife:                return ItemAssets.Instance.knifeAnimator;
        }
    }

    public bool IsStackable(){
        switch(itemType){
            default:
            case ItemType.apple:
            case ItemType.kiwi:
            case ItemType.pizza:
            case ItemType.ammo:
            case ItemType.armor:
            case ItemType.grenade:
            case ItemType.banana:
            case ItemType.goiaba:
                return true;
            case ItemType.blaster:
            case ItemType.grenadeLauncher:
            case ItemType.gun:
            case ItemType.knife:
                return false;
        }
    }

}
