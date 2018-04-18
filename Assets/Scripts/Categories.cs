using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Categories : MonoBehaviour
{
    public Puzzle _puzzle;
    public MenuButton _menu;

    public void Mate()
    {
        _puzzle.SetType(0);
        _menu.ButtonClick();
    }

    public void BestMove()
    {
        _puzzle.SetType(1);
        _menu.ButtonClick();
    }

    public void Endgame()
    {
        _puzzle.SetType(2);
        _menu.ButtonClick();
    }

    public void Stalemate()
    {
        _puzzle.SetType(3);
        _menu.ButtonClick();
    }

    public void Fork()
    {
        _puzzle.SetType(4);
        _menu.ButtonClick();
    }

    public void Pin()
    {
        _puzzle.SetType(5);
        _menu.ButtonClick();
    }
}
