using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Four : MonoBehaviour
{
    public BoardManager _bm;

    Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        if (_bm.isUserWhite) text.text = "4";
        else text.text = "5";
    }
}
