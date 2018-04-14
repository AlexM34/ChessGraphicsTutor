using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    public Connect _connect;

    public void ButtonClick()
    {
        _connect.Hint();
    }
}
