using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attraction : MonoBehaviour
{
    [SerializeField]
    GameObject anotherPlayer;

    [SerializeField]
    float velocity = 0.1f;

    [SerializeField]
    float distance;

    [SerializeField]
    Vector2 currentPostion;

    [SerializeField]
    Vector2 direction;

    public Movement movement;

    void Update()
    {
        if (distance <= 3f)
        {
            this.gameObject.GetComponent<SpringJoint2D>().enabled = true;
        }
        distance = Vector2.Distance(anotherPlayer.transform.position, transform.position);
        direction = anotherPlayer.transform.position - transform.position;
        release_attract();
    }

    void FixedUpdate()
    {
        if(distance > 3f)
        {
            this.gameObject.GetComponent<SpringJoint2D>().enabled = false;
            attract();
        }
    }

    private void attract()
    {
        if (this.gameObject.name == "Circle1")
        {
            if (Input.GetKey(KeyCode.K) && movement.state == Movement.State.idle) //Only Could Press on Idle
            {
                anotherPlayer.GetComponent<Rigidbody2D>().AddForce(-direction * velocity);
                this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.J) && movement.state == Movement.State.idle) //Only Could Press on Idle
            {
                anotherPlayer.GetComponent<Rigidbody2D>().AddForce(-direction * velocity);
                this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }

    private void release_attract()
    {
        if (this.gameObject.name == "Circle1")
        {
            if (Input.GetKeyUp(KeyCode.K))
            {
                this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.J))
            {
                this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }
}
