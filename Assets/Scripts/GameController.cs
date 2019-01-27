using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public float howLongIsADay = 5f;
    public int day = 0;
    public TextMeshPro tmp;

    public bool isPlayerAHit = false;

    public bool isPlayerBHit = false;
    void Start()
    {
        day = GameStats.day;
        //howLongIsADay -= GameStats.remainTime;
    }

    // Update is called once per frame
    void Update()
    {
        howLongIsADay -= Time.deltaTime;
        if (howLongIsADay < 0)
        {
            day++;
            howLongIsADay = 5;
        }

        tmp.text = "DAY " + day;

        if (isPlayerAHit && isPlayerBHit)
        {
            GameStats.day = day;
            GameStats.remainTime = howLongIsADay;
            StartCoroutine(EndScene());
        }
    }

    IEnumerator EndScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(4);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        print("Planet hits" + other.gameObject.name);
        if (other.gameObject.name == "PlayerA")
        {
            isPlayerAHit = true;
            other.gameObject.GetComponent<Movement>().RevealFlag();
            other.gameObject.GetComponent<Movement>().enabled = false;
        }
        else if (other.gameObject.name == "PlayerB")
        {
            isPlayerBHit = true;
            other.gameObject.GetComponent<Movement>().RevealFlag();
            other.gameObject.GetComponent<Movement>().enabled = false;
        }
    }
}
