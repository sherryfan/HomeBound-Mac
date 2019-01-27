using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeAttract : MonoBehaviour
{
    public GameObject blackhole;

    public Vector2 direction;

    public float blackhole_velocity = 0.5f;

    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.name == "SpacemanSpriteA")
        {
            direction = blackhole.transform.position - other.gameObject.transform.position;
            other.gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * blackhole_velocity);
        }
        else if(other.gameObject.name == "SpacemanSpriteB")
        {
            direction = blackhole.transform.position - other.gameObject.transform.position;
            other.gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * blackhole_velocity);
        }
    }
}
