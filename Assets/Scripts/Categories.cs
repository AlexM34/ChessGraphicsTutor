using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Categories : MonoBehaviour
{
    public Puzzle _puzzle;
    public MenuButton _menu;
    public Blanket _blanket;

    public void Mate()
    {
        _blanket.Change(0);
        _puzzle.SetType(0);
        _menu.ButtonClick();
    }

    public void BestMove()
    {
        _blanket.Change(1);
        _puzzle.SetType(1);
        _menu.ButtonClick();
    }

    public void Endgame()
    {
        _blanket.Change(2);
        _puzzle.SetType(2);
        _menu.ButtonClick();
    }

    public void Stalemate()
    {
        _blanket.Change(3);
        _puzzle.SetType(3);
        _menu.ButtonClick();
    }

    public void Fork()
    {
        _blanket.Change(4);
        _puzzle.SetType(4);
        _menu.ButtonClick();
    }

    public void Pin()
    {
        _blanket.Change(5);
        _puzzle.SetType(5);
        _menu.ButtonClick();
    }
}
