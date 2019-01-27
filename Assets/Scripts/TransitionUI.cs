using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TransitionUI : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro[] tmp;
    public GameObject[] planets;
    public string[] text = {
        " REACH PLANET - OCCION IV.",
        " REACH PLANET - Zeimia 3H28.",
        " REACH THE MOON. WE ARE CLOSE NOW...",
        " REACH THE EARTH. WE ARE HOME..."
    };
    void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadNextScene(){
        tmp[GameStats.level].gameObject.SetActive(true);
        tmp[GameStats.level].text = "Day " + GameStats.day + text[GameStats.level];
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(GameStats.level + 1);
        GameStats.level++;
        yield return null;
    }
}
