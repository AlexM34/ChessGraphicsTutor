using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        KnightMove(CurrentX - 1, CurrentY + 2, ref r);
        KnightMove(CurrentX + 1, CurrentY + 2, ref r);
        KnightMove(CurrentX + 2, CurrentY + 1, ref r);
        KnightMove(CurrentX + 2, CurrentY - 1, ref r);
        KnightMove(CurrentX - 1, CurrentY - 2, ref r);
        KnightMove(CurrentX + 1, CurrentY - 2, ref r);
        KnightMove(CurrentX - 2, CurrentY + 1, ref r);
        KnightMove(CurrentX - 2, CurrentY - 1, ref r);

        return r;
    }

    public void KnightMove (int x, int y, ref bool[,] r)
    {
        Chessman c;
        if (x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            c = BoardManager.Instance.Chessmans[x, y];
            if (c == null || isWhite != c.isWhite) r[x, y] = true;

        }
    }
}
