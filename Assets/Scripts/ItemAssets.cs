using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake() {
        Instance = this;    
    }


    public Transform pfItemWorld;

    public Sprite pizzaSprite;
    public Sprite bananaSprite;
    public Sprite kiwiSprite;
    public Sprite appleSprite;

    public Sprite ammoSprite;
    public Sprite armorSprite;
    public Sprite grenadeSprite;
    public Sprite blasterSprite;
    public Sprite grenadeLauncherSprite;
    public Sprite knifeSprite;
    public Sprite gunSprite;

    public Sprite goiabaSprite;

    //================================

    public string pizzaDescription;
    public string bananaDescription;
    public string kiwiDescription;
    public string appleDescription;

    public string ammoDescription;

    public string armorDescription;
    public string grenadeDescription;

    public string blasterDescription;
    public string grenadeLauncherDescription;
    public string knifeDescription;
    public string gunDescription;

    public string goiabaDescription;

    //====================

    public string pizzaAnimator;
    public string bananaAnimator;
    public string kiwiAnimator;
    public string appleAnimator;

    public string ammoAnimator;

    public string armorAnimator;
    public string grenadeAnimator;

    public string blasterAnimator;
    public string grenadeLauncherAnimator;
    public string knifeAnimator;
    public string gunAnimator;
    
}
