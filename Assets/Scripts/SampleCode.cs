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
    private int level = 10;
    public bool puzzleMode = false;

    public BoardManager _bm;
    public Play _play;

    [DllImport("Engine.dll", CharSet = CharSet.Unicode)]
    static extern int Move(int move, double time, int l);
    
    public Text text;

	// Use this for initialization
	void Start ()
    {
        NewGame();
    }

    public void NewGame ()
    {
        timeWhite = 613f;
        timeBlack = 65f;
        text = GetComponent<Text>();
        int x =  Move(0, 1000 * timeWhite, level);
        if (!_bm.isUserWhite) CPU(-6);
    }

    public void Hint ()
    {
        if (puzzleMode)
        {
            int xfrom = _bm.solution[4 * _bm.puzzleMoves - 4];
            int yfrom = _bm.solution[4 * _bm.puzzleMoves - 3];
            if (_bm.selectedChessman != null) _bm.MoveChessman(xfrom, yfrom);
            _bm.SelectChessman(xfrom, yfrom);
            return;
        }

        if (_bm.isEnded ||_bm.wait) return;
        int m;
        if (_bm.isWhiteTurn) m = Move(-16, 1000 * timeWhite, level);
        else m = Move(-17, 1000 * timeBlack, level);

        int from = m / 100; //promotion issues
        int x1 = from % 8;
        int y1 = 7 - (from / 8);
        if (_bm.selectedChessman != null) _bm.MoveChessman(x1, y1);
        _bm.SelectChessman(x1, y1);
    }

    public void Takeback ()
    {
        if (_bm.wait || _bm.moves == 0) return;
        //if (_bm.moves == 1 || (_bm.moves == 2 && _bm.isEngineOn)) New();
        if (_bm.moves == 1 && _bm.isEngineOn && !_bm.isUserWhite) return;
        _bm.Takeback();
        if (_bm.puzzleMode) _bm.puzzleMoves++;
        int m = Move(-666, 100, level);
        if (_bm.isEngineOn || (_bm.puzzleMode && _bm.isUserWhite != _bm.isWhiteTurn))
        {
            _bm.Takeback();
            if (_bm.puzzleMode) _bm.puzzleMoves++;
            m = Move(-666, 100, level);
        }
        _bm.isEnded = false;
    }

    public void Analyze()
    {
        if (_bm.isEnded || _bm.wait || puzzleMode) return;
        _bm.isEngineOn = false;
        _bm.isEnded = false;
        _bm.puzzleMode = false;
    }

    public void Level (int x)
    {
        level = x;
    }

    public bool Possible (int x, int y)
    {
        int from;
        if (_bm.selectedChessman == null) from = _bm.piece_x + 8 * (7 - _bm.piece_y);
        else from = _bm.selectedChessman.CurrentX + 8 * (7 - _bm.selectedChessman.CurrentY);
        int to = x + 8 * (7 - y);

        if (Move(from * 100 + to, 100, -1) == 1) return true;
        else return false;
    }

    // Update is called once per frame
    void Update ()
    {
        GameObject.Find("Slider").GetComponentInChildren<Text>().text = "Level " + _play.mainSlider.value;// + "/10";
        puzzleMode = _bm.puzzleMode;

        if (_bm.isWhiteTurn) timeWhite -= Time.deltaTime;
        else timeBlack -= Time.deltaTime;

        if (timeWhite <= 0f)
        {
            if (timeBlack > 0f)
            {
                text.text = "White lost on time";
                if (_bm.isEngineOn) _bm.EndGame(-1);
            }
        }
        else if (timeBlack <= 0f)
        {
            text.text = "Black lost on time";
            if (_bm.isEngineOn) _bm.EndGame(1);
        }
        else if (!_bm.isEnded)
        {
            if (_bm.isUserWhite)
            {
                text.text = "        ";
                if ((int)timeBlack >= 3600) text.text += ((int)timeBlack / 3600).ToString() + ":";
                else text.text += "  ";
                if (((int)timeBlack % 3600) < 600 && ((int)timeBlack >= 3600)) text.text += "0";
                if ((int)timeBlack < 600) text.text += " ";
                text.text += (((int)timeBlack % 3600) / 60).ToString() + ".";
                if (((int)timeBlack % 60) < 10) text.text += "0";
                text.text += ((int)timeBlack % 60).ToString() + "\r\n\r\n\r\n";
                if ((int)timeWhite >= 3600) text.text += ((int)timeWhite / 3600).ToString() + ":";
                else text.text += "  ";
                if (((int)timeWhite % 3600) < 600 && ((int)timeWhite >= 3600)) text.text += "0";
                if ((int)timeWhite < 600) text.text += " ";
                text.text += (((int)timeWhite % 3600) / 60).ToString() + ".";
                if (((int)timeWhite % 60) < 10) text.text += "0";
                text.text += ((int)timeWhite % 60).ToString();
            }
            else
            {
                text.text = "        ";
                if ((int)timeWhite >= 3600) text.text += ((int)timeWhite / 3600).ToString() + ":";
                else text.text += "  ";
                if (((int)timeWhite % 3600) < 600 && ((int)timeWhite >= 3600)) text.text += "0";
                if ((int)timeWhite < 600) text.text += " ";
                text.text += (((int)timeWhite % 3600) / 60).ToString() + ".";
                if (((int)timeWhite % 60) < 10) text.text += "0";
                text.text += ((int)timeWhite % 60).ToString() + "\r\n\r\n\r\n";
                if ((int)timeBlack >= 3600) text.text += ((int)timeBlack / 3600).ToString() + ":";
                else text.text += "  ";
                if (((int)timeBlack % 3600) < 600 && ((int)timeBlack >= 3600)) text.text += "0";
                if ((int)timeBlack < 600) text.text += " ";
                text.text += (((int)timeBlack % 3600) / 60).ToString() + ".";
                if (((int)timeBlack % 60) < 10) text.text += "0";
                text.text += ((int)timeBlack % 60).ToString();
            }
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
        if (_bm.puzzleMode)
        {
            int xfrom = _bm.solution[4 * _bm.puzzleMoves - 4];
            int yfrom = _bm.solution[4 * _bm.puzzleMoves - 3];
            int xto = _bm.solution[4 * _bm.puzzleMoves - 2];
            int yto = _bm.solution[4 * _bm.puzzleMoves - 1];

            _bm.SelectChessman(xfrom, yfrom);
            _bm.MoveChessman(xto, yto);

            return;
        }

        _bm.isEngineOn = true;
        _bm.isUserWhite = !_bm.isWhiteTurn;
        _bm.puzzleMode = false;
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

        m = Move(move, time, level);
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
        m = Move(-5, 10, level);   //mate or stalemate?

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
