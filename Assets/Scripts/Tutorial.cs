using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public Movement playerA, playerB;

    private bool AisReseting = false;
    private bool BisReseting = false;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(StartGame());
        }

        if(playerA.state == Movement.State.landing && !AisReseting){
            StartCoroutine(ResetPlayerA());
        }

        if(playerB.state == Movement.State.landing && !BisReseting){
            StartCoroutine(ResetPlayerB());
        }
        
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }

    IEnumerator ResetPlayerA(){
        AisReseting = true;
        yield return new WaitForSeconds(1.8f);
        
        StartCoroutine(playerA.TutorialLand());
        yield return new WaitForSeconds(0.2f);
        AisReseting = false;
        yield return null;
    }

    IEnumerator ResetPlayerB(){
        BisReseting = true;
        yield return new WaitForSeconds(1.8f);
        
        StartCoroutine(playerB.TutorialLand());
        yield return new WaitForSeconds(0.2f);
        BisReseting = false;
        yield return null;
    }
}
