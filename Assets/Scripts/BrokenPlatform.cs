using System.Collections;
using UnityEngine;

public class BrokenPlatform : MonoBehaviour
{
    public AudioSource SoundEffect;
    public AudioClip Crumble, Break;

    public GameObject[] Platforms;

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
        SoundEffect.PlayOneShot(Crumble);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        StartCoroutine(JudgeExit(other.gameObject));
    }

    private IEnumerator JudgeExit(GameObject other)
    {
        if(other.GetComponent<Movement>().state == Movement.State.flying)
        {
            SoundEffect.PlayOneShot(Break);

            foreach (GameObject seg in Platforms)
            {
                seg.SetActive(false);
            }

            yield return new WaitForSeconds(2f);

            this.gameObject.SetActive(false);
            print("ExitPlatform");
            StopAllCoroutines();
        }
        yield return new WaitForSeconds(0.05f);
        StartCoroutine(JudgeExit(other));
    }

}
