using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadMove : MonoBehaviour
{
    public Canvas badMove;
    public Button play;
    //public Button cancel;

    void Start()
    {
        badMove = badMove.GetComponent<Canvas>();
        play = play.GetComponent<Button>();
        //exitButton = exitButton.GetComponent<Button>();
        badMove.enabled = false;
    }

    public void Ask()
    {
        badMove.enabled = true;
        play.enabled = true;
        //exitButton.enabled = false;
    }

    public void Continue()
    {
        badMove.enabled = false;
        play.enabled = false;
        //exitButton.enabled = false;
    }

    public void Cancel()
    {
        badMove.enabled = false;
        play.enabled = false;
        //exitButton.enabled = false;
    }
}