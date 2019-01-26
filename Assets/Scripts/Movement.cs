﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isPlayerA = false;
    public Animator m_Anim;

    public float speed = 1f;
    public bool stop = false;
    public GameObject spaceman, // the sprite of player
    fart,
    direction, // the sprite for direction indication
    directionEnd, //for calculation of the movement direction
    EndGameUI;

    public enum State { idle = 0, crouch, flying };
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
                else if (state != State.crouch)
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
                    Fly();
                }
                else
                {
                    StartCoroutine(Launch());
                }

            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Return))
            {
                //print("space down");
                isRotating = true;
                if (state != State.crouch)
                {
                    state = State.crouch;
                    //trigger animation
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
                    Fly();
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

    void Fly()
    {
        StartCoroutine(Fart());
        Vector2 flyDirection = spaceman.transform.position - directionEnd.transform.position;
        rb.AddForce(flyDirection * speed);
        if (state != State.flying)
        {
            state = State.flying;
            //trigger animation
            m_Anim.SetBool("Crouch", false);
            m_Anim.SetBool("Jump", true);
        }
        if (flyDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(flyDirection.y, flyDirection.x) * Mathf.Rad2Deg;
            spaceman.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }
        print("previous velocity: " + GetComponent<Rigidbody2D>().velocity.magnitude);
    }

    IEnumerator Fart(){
        fart.SetActive(true);
        fart.GetComponent<Animator>().SetTrigger("Fart");
        direction.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1.2f);
        fart.SetActive(false);
        direction.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        yield return null;
    }

    IEnumerator Launch()
    {
        rb.AddForce(spaceman.transform.up * speed);

        state = State.flying;
        //trigger animation
        m_Anim.SetBool("Crouch", false);
        m_Anim.SetBool("Jump", true);

        yield return new WaitForSeconds(1f);
        direction.SetActive(true);
        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Station")
        {
            rb.velocity = Vector2.zero;
            state = State.idle;
            m_Anim.SetBool("Jump", false);
            m_Anim.SetBool("Land", true);

            // adjust to perpendicular position
            if (other.contacts[0].normal.y >= 0.9f)
            {
                Vector3 angle = new Vector3(0f, 0f, 0f);
                spaceman.transform.rotation = Quaternion.Euler(angle);
            }
            else if (other.contacts[0].normal.y <= -0.9f)
            {
                Vector3 angle = new Vector3(0f, 0f, 180f);
                spaceman.transform.rotation = Quaternion.Euler(angle);
            }
            else if (other.contacts[0].normal.x >= 0.9f)
            {
                Vector3 angle = new Vector3(0f, 0f, 270f);
                spaceman.transform.rotation = Quaternion.Euler(angle);
            }
            else if (other.contacts[0].normal.x <= -0.9f)
            {
                Vector3 angle = new Vector3(0f, 0f, 90f);
                spaceman.transform.rotation = Quaternion.Euler(angle);
            }



        }

        if (other.gameObject.tag == "Death")
        {
            rb.velocity = Vector2.zero;
            state = State.idle;
            //Game Over
            EndGameUI.SetActive(true);
        }

    }
}
