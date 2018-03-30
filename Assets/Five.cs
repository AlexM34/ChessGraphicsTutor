using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Five : MonoBehaviour
{
    public BoardManager _bm;

    Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        if (_bm.isUserWhite) text.text = "5";
        else text.text = "4";
    }
}
