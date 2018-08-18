using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LifeScene : MonoBehaviour {

    [SerializeField] string[] strings;

    void Start()
    {
        Text text = GetComponent<Text>();
        var num = UnityEngine.Random.Range(0,strings.Length);
        text.text = strings[num];
    }
    
}
