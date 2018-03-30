using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seven : MonoBehaviour
{
    public BoardManager _bm;

    Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        if (_bm.isUserWhite) text.text = "7";
        else text.text = "2";
    }
}
