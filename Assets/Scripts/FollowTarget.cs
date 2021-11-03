using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public float Speed;
    public float StoppingDistance;

    private Transform Target;
    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Target !=null){
            if(Vector2.Distance(transform.position, Target.position) > StoppingDistance){
                transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
            }
            if(transform.position.x > Target.position.x){
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }else{
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
        }
    }
}
