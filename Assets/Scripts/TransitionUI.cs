using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TransitionUI : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro tmpDay, tmp;
    public GameObject[] planets;
    public string[] text = {
        " REACH PLANET - OCCION IV.",
        " REACH PLANET - Zeimia 3H28.",
        " REACH THE MOON. WE ARE CLOSE NOW...",
        " REACH THE EARTH. WE ARE HOME..."
    };

    public string[] planetNames = {
        "OCCION IV","ZEIMIA-3H28","THE MOON","THE EARTH"
    };
    void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator LoadNextScene()
    {
        GameStats.level++;
        if (GameStats.level == 4)
        {
            for (int i = 0; i < GameStats.level; i++)
            {
                planets[i].gameObject.SetActive(true);
                tmpDay.text = "DAY " + GameStats.day + "  " + planetNames[i];
                tmp.text = text[i];
            }
            //reach earth, load ending credits
            yield return new WaitForSeconds(8f);
            SceneManager.LoadScene(6);
        }
        print(GameStats.level);
        for (int i = 0; i < GameStats.level; i++)
        {
            planets[i].gameObject.SetActive(true);
            tmpDay.text = "DAY " + GameStats.day + "  " + planetNames[i];
            tmp.text = text[i];
        }

        yield return new WaitForSeconds(8f);

        SceneManager.LoadScene(GameStats.level + 1);
        yield return null;
    }
}
