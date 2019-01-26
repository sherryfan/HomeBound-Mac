using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1f;
    public bool stop = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey("space")){
            print("space");
            GetComponent<Rigidbody2D>().AddForce(transform.right * speed);
        }
    }
}
