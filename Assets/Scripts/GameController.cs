using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public float howLongIsADay = 5f;
    public int day;
    public TextMeshPro tmp;
    void Start()
    {
        day = GameStats.day;
    }

    // Update is called once per frame
    void Update()
    {
        howLongIsADay -= Time.deltaTime;
        if(howLongIsADay < 0){
            day++;
            howLongIsADay = 5;
        }

        tmp.text = "Day " + day;

    }
}
