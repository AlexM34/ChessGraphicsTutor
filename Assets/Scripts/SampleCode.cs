using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System;

public class SampleCode : MonoBehaviour
{
    public float slowdownFactor = 0.00005f;
    public float slowdownLength = 20f;

    public double timeWhite;
    public double timeBlack;

    public BoardManager _bm;

    [DllImport("Engine.dll", CharSet = CharSet.Unicode)]
    static extern int Move(int move, double time);
    
    public Text text;

	// Use this for initialization
	void Start ()
    {
        NewGame();
    }

    public void NewGame ()
    {
        timeWhite = 60f;
        timeBlack = 60f;
        text = GetComponent<Text>();
        int x =  Move(0, 1000 * timeWhite);
        if (!_bm.isUserWhite) CPU(-6);
    }

    public void Hint ()
    {
        if (_bm.isEnded ||_bm.wait) return;
        int m;
        if (_bm.isWhiteTurn) m = Move(-16, 1000 * timeWhite);
        else m = Move(-17, 1000 * timeBlack);

        int from = m / 100; //promotion issues
        int x1 = from % 8;
        int y1 = 7 - (from / 8);
        if (_bm.selectedChessman != null) _bm.MoveChessman(x1, y1);
        _bm.SelectChessman(x1, y1);
    }

    public void Takeback ()
    {
        if (_bm.wait) return;
        //if (_bm.moves == 1 || (_bm.moves == 2 && _bm.isEngineOn)) New();
        if (_bm.moves == 1 && _bm.isEngineOn && !_bm.isUserWhite) return;
        _bm.Takeback();
        int m = Move(-666, 100);
        if (_bm.isEngineOn)
        {
            _bm.Takeback();
            m = Move(-666, 100);
        }
    }

    public void Analyze()
    {
        if (_bm.isEnded || _bm.wait) return;
        _bm.isEngineOn = false;
    }

    // Update is called once per frame
    void Update ()
    {
        if (_bm.isWhiteTurn) timeWhite -= Time.deltaTime;
        else timeBlack -= Time.deltaTime;

        if (timeWhite < 0f)
        {
            if (timeBlack >= 0f)
            {
                text.text = "White lost on time";
                _bm.EndGame(-1);
            }
        }
        else if (timeBlack < 0f)
        {
            text.text = "Black lost on time";
            _bm.EndGame(1);
        }
        else if (!_bm.isEnded)
        {
            if (_bm.isUserWhite) text.text ="        " + ((int)timeBlack).ToString() + "\r\n\r\n\r\n\r\n" + ((int)timeWhite).ToString();
            else text.text = "        " + ((int)timeWhite).ToString() + "\r\n\r\n\r\n\r\n" + ((int)timeBlack).ToString();
        }
        //Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        //Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    public void User (int x1, int y1, int x2, int y2)
    {
        int from = x1 + 8 * (7 - y1);
        int to = x2 + 8 * (7 - y2);
        CPU(100 * from + to);
    }

    public void Flip ()
    {
        _bm.isEngineOn = true;
        _bm.isUserWhite = !_bm.isWhiteTurn;
        if (!_bm.isWhiteTurn) CPU(-7);   //black
        else CPU(-6);   //white
    }

    public void New ()
    {
        _bm.NewGame ();
        NewGame ();
    }

    private void CPU(int move)
    {
        DateTime start = DateTime.Now;
        int m;
        double time;

        if (!_bm.isEngineOn) time = -10;
        else if (!_bm.isUserWhite) time = 1000 * timeWhite;
        else time = 1000 * timeBlack;

        m = Move(move, time);
        if (m > -102 && m < -98)
        {
            if (m == -99) _bm.EndGame(1);
            else if (m == -100) _bm.EndGame(0);
            else if (m == -101) _bm.EndGame(-1);

            return;
        }

        int from = m / 100; //promotion issues
        int to = m % 100;
        int x1 = from % 8;
        int y1 = 7 - (from / 8);
        int x2 = to % 8;
        int y2 = 7 - (to / 8);
        if (m != 0) _bm.Play(x1, y1, x2, y2);
        m = Move(-5, 10);   //mate or stalemate?

        if (m > -102 && m < -98)
        {
            if (m == -99) _bm.EndGame(1);
            else if (m == -100) _bm.EndGame(0);
            else if (m == -101) _bm.EndGame(-1);

            return;
        }


        TimeSpan diff = DateTime.Now - start;
        if (_bm.isUserWhite) timeBlack -= diff.TotalSeconds;
        else timeWhite -= diff.TotalSeconds;
    }

    public void SlowMotion ()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
}
