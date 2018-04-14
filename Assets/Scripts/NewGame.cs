using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGame : MonoBehaviour
{
    public Connect _connect;

    public void ButtonClick()
    {
        _connect.New();
    }
}