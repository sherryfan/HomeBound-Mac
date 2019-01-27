using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool isPlayerA = false;
    public Animator m_Anim;

    public float speed = 1f;
    public GameObject spaceman, // the sprite of player
    fart,
    direction, // the sprite for direction indication
    directionEnd, //for calculation of the movement direction
    EndGameUI;

    public enum State { idle = 0, crouch, flying, landing };
    public State state;
    public bool isRotating = false;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = State.idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerA)
        {
            if (Input.GetKey("space"))
            {
                //print("space down");
                if (state == State.flying)
                {
                    isRotating = true;
                }
                else if (state == State.idle)
                {
                    state = State.crouch;
                    //trigger animation
                    print("crouch");
                    m_Anim.SetBool("Land", false);
                    m_Anim.SetBool("Crouch", true);
                }
            }

            if (Input.GetKeyUp("space"))
            {
                //print("space up");
                if (state == State.flying)
                {
                    isRotating = false;
                    FlyOneTime();
                }
                else if (state == State.crouch)
                {
                    StartCoroutine(Launch());
                }
                print("current velocity: " + rb.velocity.magnitude);
            }

        }
        else
        {
            if (Input.GetKey(KeyCode.Return))
            {
                if (state == State.flying)
                {
                    isRotating = true;
                }
                else if (state == State.idle)
                {
                    state = State.crouch;
                    //trigger animation
                    print("crouch");
                    m_Anim.SetBool("Land", false);
                    m_Anim.SetBool("Crouch", true);
                }
            }

            if (Input.GetKeyUp(KeyCode.Return))
            {
                //print("return up");
                if (state == State.flying)
                {
                    isRotating = false;
                    FlyOneTime();
                }
                else
                {
                    StartCoroutine(Launch());
                }
            }
        }


    }
    void FixedUpdate()
    {
        if (isRotating)
        {
            direction.transform.Rotate(0, 0, 5f);
        }

    }

    void FlyOneTime()
    {
        StartCoroutine(Fart());
        state = State.landing;

        Vector2 flyDirection = spaceman.transform.position - directionEnd.transform.position;
        rb.AddForce(flyDirection * speed);
        if (flyDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(flyDirection.y, flyDirection.x) * Mathf.Rad2Deg;
            spaceman.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }

    }

    IEnumerator Fart()
    {
        fart.SetActive(true);
        fart.GetComponent<Animator>().SetTrigger("Fart");
        direction.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1.2f);
        fart.SetActive(false);
        yield return null;
    }

    IEnumerator Launch()
    {
        rb.AddForce(spaceman.transform.up * speed);
        direction.SetActive(true);
        direction.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

        state = State.flying;
        //trigger animation
        m_Anim.SetBool("Crouch", false);
        m_Anim.SetBool("Jump", true);

        yield return new WaitForSeconds(1f);
        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Station")
        {
            state = State.landing;
            StartCoroutine(Land(other));
        }

        if (other.gameObject.tag == "Death")
        {
            rb.velocity = Vector2.zero;
            state = State.idle;
            //Game Over
            EndGameUI.SetActive(true);
        }
    }

    IEnumerator Land(Collision2D other)
    {
        rb.velocity = Vector2.zero;

        m_Anim.SetBool("Jump", false);
        m_Anim.SetBool("Land", true);

        direction.SetActive(true);
        direction.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

        float angle = Vector2.Angle(other.contacts[0].normal, new Vector2(0f, 1f));
        if (other.contacts[0].normal.x > 0f)
        {
            angle = -angle;
        }
        spaceman.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        yield return new WaitForSeconds(0.5f);
        state = State.idle;
        yield return null;
    }
}
