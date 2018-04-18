using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public BoardManager _bm;
    public int last = -1;
    private int type = -1;
    public bool next = true;
    string[] lines = System.IO.File.ReadAllLines(@"C:\Users\A.Monev\Documents\GitHub\CGT\ChessGraphicsTutor\Assets\Scripts\puzzles.txt");
    
    public void SetType (int t)
    {
        type = t;
        ButtonClick();
    }

    public void ButtonClick()
    {
        if (_bm.puzzleMode && _bm._connect.timeWhite < 0f) return;

        int r = Random.Range(0, lines.Length);
        while (r == last || (type != -1 && type != (int)(lines[r][0] - '0')))
        {
            r = Random.Range(0, lines.Length);
        }
        
        last = r;

        string line = lines[r];

        int current = 0, index = 0, l = 0;
        int[] pos = new int[96];
        int[] sol = new int[100];
        bool white = true;
        if (line[2] == '0') white = false;

        for (int i = 4; i < line.Length; i++)
        {
            if (line[i] == 'x')
            {
                l = i + 2;
                pos[index] = -1;
                index = 0;
                break;
            }

            else if (line[i] != ' ') current = 10 * current + (int)(line[i] - '0');
            else
            {
                pos[index] = current;
                current = 0;
                index++;
            }
        }

        for (int i = l; i < line.Length; i += 2)
        {
            sol[index] = (int)(line[i] - '0');
            //_bm._connect.text.text += " " + sol[index];
            index++;
        }
        
        sol[index] = -1;
        //_bm._connect.text.text += " " + sol[index];

        _bm.Puzzle(pos, sol, white);
        next = false;

        /*if (_bm.puzzleMode && _bm._connect.timeWhite < 0f) return;

        int r = Random.Range(1, 8);
        while (r == last)
        {
            r = Random.Range(1, 8);
        }
        last = r;

        switch (r)
        {
            case 1: _bm.Puzzle(pos1, sol1, color[r-1]); break;
            case 2: _bm.Puzzle(pos2, sol2, color[r-1]); break;
            case 3: _bm.Puzzle(pos3, sol3, color[r-1]); break;
            case 4: _bm.Puzzle(pos4, sol4, color[r-1]); break;
            case 5: _bm.Puzzle(pos5, sol5, color[r-1]); break;
            case 6: _bm.Puzzle(pos6, sol6, color[r-1]); break;
            case 7: _bm.Puzzle(pos7, sol7, color[r-1]); break;
        }*/
    }
}