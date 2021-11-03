using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item){
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    public static ItemWorld DropItem(Vector3 dropPosition, Item item){
        Vector3 randomDir = UtilsClass.GetRandomDir();
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir * 1f, item);
        itemWorld.GetComponent<Rigidbody2D>().AddForce(randomDir * 1f, ForceMode2D.Impulse);
        itemWorld.GetComponent<Rigidbody2D>().drag = 10;
        return itemWorld;
    }

    public static ItemWorld DropItemUpPosition(Vector3 dropPosition, Item item){
        //Vector3 randomDir = UtilsClass.GetRandomDir();
        ItemWorld itemWorld = SpawnItemWorld(dropPosition, item);
        itemWorld.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1f, ForceMode2D.Impulse);
        itemWorld.GetComponent<Rigidbody2D>().drag = 10;
        return itemWorld;
    }
    private Item item;
    private SpriteRenderer spriteRenderer;

    private Animator anim;

    private TextMeshPro textMeshPro;

    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }
    public void SetItem(Item item){
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        if(item.amount > 1){
            textMeshPro.SetText(item.amount.ToString());
        }else{
            textMeshPro.SetText("");
        }
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(item.GetAnimator());
    }

    public Item GetItem(){
        return item;
    }

    public void DestroySelf(){
        Destroy(gameObject);
    }
}
