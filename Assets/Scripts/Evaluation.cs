using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Evaluation : MonoBehaviour
{
    public Connect _connect;

    public void ButtonClick()
    {
        _connect.Evaluation();
    }

    public void GetValue (int eval)
    {
        if (eval != -99999)
        {
            string text = "";
            if (eval > -100 && eval < 0) text = '-'.ToString();
            text += (eval / 100).ToString() + '.';
            if (eval % 100 < 0) text += ((eval % 100) * -1).ToString();
            else text += (eval % 100).ToString();
            GameObject.Find("Evaluation").GetComponentInChildren<Text>().text = text;
        }
        else GameObject.Find("Evaluation").GetComponentInChildren<Text>().text = "Evaluation";
    }
}
