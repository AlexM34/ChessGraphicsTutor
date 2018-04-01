using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public BoardManager _bm;
    public int last = -1;

    bool[] color = new bool[4] { true, true, true, false };
    int[] pos1 = new int[9] { 0, 1, 5, 1, 0, 0, 6, 1, 7 };
    int[] sol1 = new int[4] { 0, 0, 7, 7 };
    int[] pos2 = new int[12] { 0, 5, 7, 4, 7, 5, 6, 7, 7, 11, 7, 6 };
    int[] sol2 = new int[4] { 7, 5, 5, 6 };
    int[] pos3 = new int[9] { 0, 2, 5, 2, 2, 0, 6, 3, 7 };
    int[] sol3 = new int[12] { 2, 0, 4, 0, 3, 7, 2, 7, 4, 0, 4, 7 };
    int[] pos4 = new int[12] { 0, 1, 0, 6, 1, 2, 9, 0, 5, 9, 6, 4 };
    int[] sol4 = new int[12] { 0, 5, 3, 2, 1, 0, 0, 0, 6, 4, 5, 5 };

    public void ButtonClick()
    {
        int r = Random.Range(1, 5);
        while (r == last)
        {
            r = Random.Range(1, 5);
        }
        last = r;

        switch (r)
        {
            case 1: _bm.Puzzle(pos1, sol1, color[r-1]); break;
            case 2: _bm.Puzzle(pos2, sol2, color[r-1]); break;
            case 3: _bm.Puzzle(pos3, sol3, color[r-1]); break;
            case 4: _bm.Puzzle(pos4, sol4, color[r-1]); break;
        }
    }
}