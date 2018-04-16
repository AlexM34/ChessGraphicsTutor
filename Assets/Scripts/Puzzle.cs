using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public BoardManager _bm;
    public int last = -1;

    bool[] color = new bool[7] { true, true, true, false, true, false, true };
    int[] pos1 = new int[9] { 0, 1, 5, 1, 0, 0, 6, 1, 7 };
    int[] sol1 = new int[4] { 0, 0, 7, 7 };
    int[] pos2 = new int[12] { 0, 5, 7, 4, 7, 5, 6, 7, 7, 11, 7, 6 };
    int[] sol2 = new int[4] { 7, 5, 5, 6 };
    int[] pos3 = new int[9] { 0, 2, 5, 2, 2, 0, 6, 3, 7 };
    int[] sol3 = new int[12] { 2, 0, 4, 0, 3, 7, 2, 7, 4, 0, 4, 7 };
    int[] pos4 = new int[12] { 0, 1, 0, 6, 1, 2, 9, 0, 5, 9, 6, 4 };
    int[] sol4 = new int[12] { 0, 5, 3, 2, 1, 0, 0, 0, 6, 4, 5, 5 };
    int[] pos5 = new int[42] { 0, 7, 0, 5, 6, 1, 5, 7, 1, 1, 0, 1, 2, 5, 0, 4, 6, 4, 6, 7, 7, 11, 6, 6, 11, 7, 6, 7, 1, 5, 8, 1, 7, 8, 2, 7, 11, 3, 2, 11, 4, 1};
    int[] sol5 = new int[28] { 6, 4, 5, 6, 7, 7, 6, 7, 5, 6, 7, 5, 6, 7, 7, 7, 0, 1, 6, 7, 2, 7, 6, 7, 7, 5, 5, 6 };
    int[] pos6 = new int[24] { 0, 0, 0, 5, 0, 1, 5, 1, 1, 1, 1, 3, 6, 6, 6, 7, 2, 6, 8, 2, 7, 2, 7, 0 };
    int[] sol6 = new int[12] { 2, 6, 2, 0, 7, 0, 2, 0, 2, 7, 2, 0 };
    int[] pos7 = new int[30] { 0, 2, 0, 2, 3, 1, 5, 0, 1, 5, 0, 2, 5, 0, 3, 5, 0, 4, 5, 0, 5, 5, 0, 6, 6, 0, 7, 9, 3, 0 };
    int[] sol7 = new int[60] { 3, 1, 3, 0, 0, 7, 0, 6, 3, 0, 3, 7, 0, 6, 0, 5, 3, 7, 3, 6, 0, 5, 0, 4, 3, 6, 3, 5, 0, 4, 0, 3, 3, 5, 3, 4, 0, 3, 0, 2, 3, 4, 3, 3, 0, 2, 0, 1, 3, 3, 3, 2, 0, 1, 0, 0, 3, 2, 0, 2 };

    public void ButtonClick()
    {
        int r = Random.Range(1, 8);
        while (r == last)
        {
            r = Random.Range(1, 8);
        }
        last = r;
        last = 7;

        switch (r)
        {
            case 1: _bm.Puzzle(pos1, sol1, color[r-1]); break;
            case 2: _bm.Puzzle(pos2, sol2, color[r-1]); break;
            case 3: _bm.Puzzle(pos3, sol3, color[r-1]); break;
            case 4: _bm.Puzzle(pos4, sol4, color[r-1]); break;
            case 5: _bm.Puzzle(pos5, sol5, color[r-1]); break;
            case 6: _bm.Puzzle(pos6, sol6, color[r-1]); break;
            case 7: _bm.Puzzle(pos7, sol7, color[r-1]); break;
        }
    }
}