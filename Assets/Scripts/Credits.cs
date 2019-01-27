using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            Restart();
        }
    }

    void Restart()
    {
        GameStats.level = 0;
        GameStats.day = 0;
        GameStats.remainTime = 0;
        SceneManager.LoadScene(0);


    }
}
