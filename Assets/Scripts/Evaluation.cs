using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Evaluation : MonoBehaviour
{
    public Connect _connect;
    public bool hide = false;

    private void Update()
    {
        if (hide) GameObject.Find("Evaluation").GetComponentInChildren<Text>().text = "Evaluation";
        hide = false;
    }

    public void ButtonClick()
    {
        _connect.Evaluation();
    }

    public void GetValue(int eval)
    {
        string text = "";
        if (eval > -100 && eval < 0) text = '-'.ToString();
        text += (eval / 100).ToString() + '.';
        if (eval % 100 < 0) text += ((eval % 100) * -1).ToString();
        else text += (eval % 100).ToString();
        GameObject.Find("Evaluation").GetComponentInChildren<Text>().text = text;
    }
}
