using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opponent : MonoBehaviour
{
    public Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        text.text = "start";
    }

    private void Update()
    {
        text.text = "8.09";
    }
}
