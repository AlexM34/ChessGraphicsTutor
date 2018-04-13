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
        GameObject.Find("Opponent").GetComponentInChildren<Text>().text = "alexis";
    }

    public void Refresh(int time)
    {
        //text.text = time.ToString();
        GameObject.Find("Opponent").GetComponentInChildren<Text>().text = time.ToString();
    }
}
