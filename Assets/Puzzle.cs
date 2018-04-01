using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public BoardManager _bm;
    
    public int[] pos1 = new int[9] { 0, 1, 5, 1, 0, 0, 6, 1, 7 };
    public int[] sol1 = new int[4] { 0, 0, 7, 7 };
    public int[] pos2 = new int[12] { 0, 5, 7, 4, 7, 5, 6, 7, 7, 11, 7, 6 };
    public int[] sol2 = new int[4] { 7, 5, 5, 6 };
    public int[] pos3 = new int[9] { 0, 3, 5, 2, 3, 0, 6, 4, 7 };
    public int[] sol3 = new int[12] { 3, 0, 5, 0, 4, 7, 3, 7, 5, 0, 5, 7 };

    public void ButtonClick()
    {
        //int r = Random.Range(1, 4);

        _bm.Puzzle(pos3, sol3);
    }
}