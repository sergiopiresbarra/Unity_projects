using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public Text damage;
    public float timeOfDestruction = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeOfDestruction);
    }

    // Update is called once per frame
   public void SetText(string value){
       damage.text = value;
   }
}
