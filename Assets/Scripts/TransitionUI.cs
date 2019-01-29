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
    private string[] text = {
        "\"THE MARTIAN\" IS THE FIRST THING THAT POPPED UP IN MY HEAD AS SOON AS I REALIZE I WAS IN SPACE...BUT WAIT, HE'S STRANDED ON MARS, WE'RE IN SPACE. ALONE. @@IN SPACE NO ONE CAN HEAR YOU SCREAM, SO WE'D BETTER GET TO IT.@@TO OCCION IV WE GO.",
        "I CAN'T HELP BUT APPRECIATE HOW SPACE TECH HAS EVOLVED THROUGH THE YEARS. I COULDN'T IMAGINE HOW WE'D SURVIVE IF THIS HAPPENED YEARS AGO.@@IF YOU'RE WONDERING WHY I'M SO CHILLED, GOD, IT'S BECAUSE THERE'S NO TIME TO PANIC. AND IT IS CHILLY HERE.@@WE'RE STOPPING AT ZEIMIA-3H28.....HOLD ON, IS THAT A BLACK HOLE?",
        "WE NEARLY DIED OUT THERE.... THE BLACK HOLE COULD'VE TORN US INTO PIECES.@@UGH... THAT WAS TOO CLOSE. BREATHE. BREATHE.@@BUT WE MADE IT. WE'RE HERE AT THE MOON. WE'RE CLOSE TO HOME.",
        "HOW FAR HAVE WE TRAVELED? I DON'T KNOW. NEVER SPENT THIS MUCH TIME TALKING TO MYSELF. EVEN IN A SITUATION LIKE THIS, I COULDN'T POSSIBLY LET MY MATE HEAR ME BABBLING NONSENSE.@@I KINDA APPRECIATED IT. KEEPS ME SANE.@@TO EARTH, FRIENDS. I'M COMING HOME."
    };

    public string[] realText;

    public string[] planetNames = {
        "OCCION IV","ZEIMIA-3H28","THE MOON","THE EARTH"
    };
    void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    void Awake()
    {
        realText = new string[4];
        realText = text;
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
                string rawText = realText[i];
                tmpDay.text = "DAY " + GameStats.day + "  " + planetNames[i];
                tmp.text = rawText.Replace('@', '\n');
            }
            //reach earth, load ending credits
            yield return new WaitForSeconds(8f);
            SceneManager.LoadScene(6);
        }
        //print(GameStats.level);

        for (int i = 0; i < GameStats.level; i++)
        {
            planets[i].gameObject.SetActive(true);
            string rawText = realText[i];
            tmpDay.text = "DAY " + GameStats.day + "  " + planetNames[i];
            tmp.text = rawText.Replace('@', '\n');
        }

        yield return new WaitForSeconds(8f);

        SceneManager.LoadScene(GameStats.level + 1);
        yield return null;
    }
}
