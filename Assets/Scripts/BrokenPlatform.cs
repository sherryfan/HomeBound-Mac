using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
       print("Collision");
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        StartCoroutine(JudgeExit(other.gameObject));
    }

    private IEnumerator JudgeExit(GameObject other)
    {
        if(other.GetComponent<Movement>().state != Movement.State.idle && other.GetComponent<Movement>().state!=Movement.State.landing)
        {
            this.gameObject.SetActive(false);
            print("ExitPlatform");
            StopAllCoroutines();
        }
        yield return new WaitForSeconds(0.05f);
        StartCoroutine(JudgeExit(other));
    }

}
