using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadMove : MonoBehaviour
{
    public Connect _connect;
    public Canvas _badMove;
    public CrystalBall _ball;
    public Button play;
    public Button cancel;
    public int eval = 150;

    void Start()
    {
        _badMove = _badMove.GetComponent<Canvas>();
        play = play.GetComponent<Button>();
        cancel = cancel.GetComponent<Button>();
        _badMove.enabled = false;
    }

    public void Ask()
    {
        _badMove.enabled = true;
        play.enabled = true;
        cancel.enabled = true;
        _connect._bm.pause = true;
    }

    public void Continue()
    {
        _badMove.enabled = false;
        play.enabled = false;
        cancel.enabled = false;
        _connect._bm.pause = false;
        _ball.Eval(eval);
    }

    public void Cancel()
    {
        _badMove.enabled = false;
        play.enabled = false;
        cancel.enabled = false;
        _connect._bm.pause = false;
        _connect.Takeback();
    }
}