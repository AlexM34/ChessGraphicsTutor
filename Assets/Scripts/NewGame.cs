using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGame : MonoBehaviour
{
    public SampleCode _sc;

    public void ButtonClick()
    {
        _sc.New();
    }
}