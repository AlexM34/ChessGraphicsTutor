using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letters : MonoBehaviour
{
    public BoardManager _bm;

    Text text;

    void Start ()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        if (_bm.isUserWhite) text.text = "A    B    C    D    E    F    G    H";
        else text.text = "H    G    F    E    D    C    B    A";
    }
}

