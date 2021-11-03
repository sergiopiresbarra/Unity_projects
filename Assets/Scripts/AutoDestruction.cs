using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruction : MonoBehaviour
{
    public float TimeDestruction;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, TimeDestruction);
    }
}
