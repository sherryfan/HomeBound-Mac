﻿using System.Collections;
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

    private void OnCollisionEnter2D(Collision other) {
        print("Collision");
    }

    private void OnCollisionExit2D(Collision2D other)
    {
       gameObject.SetActive(false);
    }

}
