using System.Collections;
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

    public AudioSource SoundEffect;

    public AudioClip SoundOnLandingNormal, SoundOnDeath, SoundWhenThrust;

    public enum State { idle = 0, crouch, flying, landing };
    public State state;
    public bool isRotating = false;
    public float min_velocity = 1f;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = State.idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude < min_velocity)
        {
            Vector2 temp_velocity = rb.velocity;
            if (rb.velocity.magnitude >= 0.001f || rb.velocity.magnitude <= -0.001)
            {
                temp_velocity = min_velocity / rb.velocity.magnitude * temp_velocity;
                rb.velocity = temp_velocity;
            }
        }

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

            if (Input.GetKeyUp(KeyCode.Return))
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
        SoundEffect.PlayOneShot(SoundWhenThrust);
        direction.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1.2f);
        fart.SetActive(false);
        yield return null;
    }

    IEnumerator Launch()
    {
        state = State.flying;
        togglePositionFreeze(false);

        rb.AddForce(spaceman.transform.up * speed);
        direction.SetActive(true);
        direction.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

        SoundEffect.PlayOneShot(SoundWhenThrust);

        //trigger animation
        m_Anim.SetBool("Crouch", false);
        m_Anim.SetBool("Jump", true);

        yield return new WaitForSeconds(1f);
        yield return null;
    }

    private void togglePositionFreeze(bool freeze)
    {
        if (freeze)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        print(other.gameObject.name);
        if (other.gameObject.tag == "Station")
        {
            state = State.landing;
            StartCoroutine(Land(other));
            SoundEffect.PlayOneShot(SoundOnLandingNormal);

        }

        if (other.gameObject.tag == "Death")
        {
            rb.velocity = Vector2.zero;
            state = State.idle;
            m_Anim.SetTrigger("Death");
            SoundEffect.PlayOneShot(SoundOnDeath);
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
        togglePositionFreeze(true);
        yield return null;
    }
}
