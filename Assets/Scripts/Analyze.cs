using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Analyze : MonoBehaviour
{
    public SampleCode _sc;

    public void ButtonClick()
    {
        _sc.Analyze();
    }
}