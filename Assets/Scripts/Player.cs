using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public SampleCode _sc;
    public Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        GameObject.Find("Player").GetComponentInChildren<Text>().text = "alexis";
    }

    public void Refresh(int time)
    {
        text.text = "10.34";
        GameObject.Find("Player").GetComponentInChildren<Text>().text = time.ToString();
    }
}
