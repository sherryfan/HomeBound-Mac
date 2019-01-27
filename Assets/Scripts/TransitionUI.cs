using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TransitionUI : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro tmp;
    void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadNextScene(){
        tmp.text = "Day " + GameStats.day + " reach planet X";
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(GameStats.level + 1);
        GameStats.level++;
        yield return null;
    }
}
