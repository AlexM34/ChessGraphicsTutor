using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coach : MonoBehaviour
{
    public Connect _connect;
    public bool isOn = true;

    public void ButtonClick()
    {
        isOn = !isOn;
        if (isOn) GameObject.Find("Coach").GetComponentInChildren<Text>().text = "Coach On";
        else GameObject.Find("Coach").GetComponentInChildren<Text>().text = "Coach Off";

        _connect.Coach(isOn);
    }
}
