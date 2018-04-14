using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Analyze : MonoBehaviour
{
    public Connect _connect;

    public void ButtonClick()
    {
        _connect.Analyze();
    }
}