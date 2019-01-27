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

    [SerializeField]
    GameObject range;

    [SerializeField]
    GameObject spacemanA;

    [SerializeField]
    GameObject spacemanB;

    public Movement movement;

    private Movement anothermovement;

    private LineRenderer linerender;

    private Rigidbody2D thisrigidbody;

    private Rigidbody2D anotherrigidbody;

    void Start()
    {
        anothermovement = anotherPlayer.GetComponent<Movement>();
        linerender = this.gameObject.GetComponent<LineRenderer>();
        thisrigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        anotherrigidbody = anotherPlayer.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        distance = Vector2.Distance(anotherPlayer.transform.position, transform.position);
        direction = anotherPlayer.transform.position - transform.position;
    }

    void FixedUpdate()
    {
        Attract();
        Release_attract();
    }

    private void Attract()
    {
        if (this.gameObject.name == "PlayerA")
        {
            if(Input.GetKey(KeyCode.A))
            {
                Onattract(this.gameObject.name);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.L))
            {
                Onattract(this.gameObject.name);
            }
        }
    }

    private void Release_attract()
    {
        if (this.gameObject.name == "PlayerA")
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                Onreleaseattract();
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.L))
            {
                Onreleaseattract();
            }
        }
    }

    private void Onattract(string Objectname)
    {
        range.SetActive(true);
        if (movement.state == Movement.State.idle && anothermovement.state != Movement.State.idle && distance <= 6.5f)
        {
            linerender.enabled = true;
            linerender.SetPosition(0, spacemanA.transform.position + new Vector3(0f, 0f, -1f));
            linerender.SetPosition(1, spacemanB.transform.position + new Vector3(0f, 0f, -1f));
            anotherrigidbody.AddForce(-direction * velocity);
            thisrigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            print(Objectname +  "attracts");
        }
    }

    private void Onreleaseattract()
    {
        range.SetActive(false);
        linerender.enabled = false;
        thisrigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
