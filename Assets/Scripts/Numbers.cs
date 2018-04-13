using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Numbers : MonoBehaviour
{
    public BoardManager _bm;

    Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        if (_bm.isUserWhite) text.text = "                           8\r\n                         7\r\n                      6\r\n                    5\r\n                4\r\n\r\n            3\r\n\r\n       2\r\n\r\n\r\n  1";
        else text.text = "                           1\r\n                         2\r\n                      3\r\n                    4\r\n                5\r\n\r\n            6\r\n\r\n       7\r\n\r\n\r\n  8";
    }
}
