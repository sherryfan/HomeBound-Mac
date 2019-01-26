using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attraction : MonoBehaviour
{
    [SerializeField]
    GameObject anotherCircle;

    [SerializeField]
    float velocity = 0.1f;

    [SerializeField]
    float distance;

    [SerializeField]
    Vector2 currentPostion;

    private Vector2 direction;


    void Start()
    {
        direction = anotherCircle.transform.position - transform.position;
        currentPostion = transform.position;
    }

    void Update()
    {
        distance = Vector2.Distance(anotherCircle.transform.position, transform.position);
        direction = anotherCircle.transform.position - transform.position;
    }

    void FixedUpdate()
    {
        if(distance > 3f)
        {
            this.gameObject.GetComponent<SpringJoint2D>().enabled = false;
            if(Input.GetKey(KeyCode.K))
            {
                anotherCircle.GetComponent<Rigidbody2D>().AddForce(-direction * velocity);
            }
        }
        else
        {
            this.gameObject.GetComponent<SpringJoint2D>().enabled = true;
        }
    }

}
