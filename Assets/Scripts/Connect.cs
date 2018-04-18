using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System;

public class Connect : MonoBehaviour
{
    public float slowdownFactor = 0.00005f;
    public float slowdownLength = 20f;

    public double timeWhite;
    public double timeBlack;
    private int level = 10;
    public bool puzzleMode = false;
    private int eval = 25;

    public BoardManager _bm;
    public Play _play;
    public Opponent _opp;
    public Player _pl;
    public Evaluation _eval;
    public BadMove _bad;

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
        timeWhite = 300f;
        timeBlack = 60f;
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
        
        if (_bm.isWhiteTurn && !_bm.pause) timeWhite -= Time.deltaTime;
        else if (!_bm.pause) timeBlack -= Time.deltaTime;
        return;
        if (timeWhite <= 0f)
        {
            if (puzzleMode)
            {
                text.text = "Final score:" + "\r\n      " + _bm.solved + "\r\n\r\n";
            }

            else if (timeBlack > 0f)
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
            if (puzzleMode)
            {
                text.text = "   Solved:" + "\r\n      " + _bm.solved + "\r\n\r\n";

                if ((int)timeWhite >= 3600) text.text += ((int)timeWhite / 3600).ToString() + ":";
                else text.text += "  ";
                if (((int)timeWhite % 3600) < 600 && ((int)timeWhite >= 3600)) text.text += "0";
                if ((int)timeWhite < 600) text.text += " ";
                text.text += (((int)timeWhite % 3600) / 60).ToString() + ".";
                if (((int)timeWhite % 60) < 10) text.text += "0";
                text.text += ((int)timeWhite % 60).ToString();
            }
            else if (_bm.isUserWhite)
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

                //_opp.Refresh((int)timeBlack);
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

                //_opp.Refresh((int)timeWhite);
            }
        }

        /*text.text = _bm.movetakenwhite[0].ToString() + _bm.movetakenblack[0].ToString();
        for (int i = 1; i < 5; i++)
        {
            text.text += _bm.movetakenwhite[i].ToString() + _bm.movetakenblack[i].ToString();
        }

        if (_bm.isUserWhite)
        {
            _pl.Refresh((int)timeWhite);
            _opp.Refresh((int)timeBlack);
        }
        else
        {
            _pl.Refresh((int)timeBlack);
            _opp.Refresh((int)timeWhite);
        }*/
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

    public void Evaluation()
    {
        int eval = Move(-22, 10, 1);
        _eval.GetValue(eval);
    }

    private void CPU(int move)
    {
        DateTime start = DateTime.Now;
        int m, evaldiff;
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

        evaldiff = Move(-22, 10, 1) - eval;
        text.text = evaldiff.ToString();
        if (evaldiff < -20) _bad.Ask();
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

        eval = Move(-22, 10, 1);
        TimeSpan diff = DateTime.Now - start;
        if (_bm.isUserWhite) timeBlack -= diff.TotalSeconds;
        else timeWhite -= diff.TotalSeconds;
        _eval.hide = true;
    }

    public void SlowMotion ()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
}
