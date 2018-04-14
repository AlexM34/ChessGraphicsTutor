using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Takeback : MonoBehaviour
{
    public Connect _connect;

    public void ButtonClick()
    {
        _connect.Takeback();
    }
}
