using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Chessman
{
    public override bool[,] PossibleMove ()
    {
        bool[,] r = new bool[8, 8];

        Chessman c, c1, c2, c3, c4;
        int i = CurrentX - 1, j = CurrentY + 1;
        if (CurrentY != 7)
        {
            for (int k = 0; k < 3; k++)
            {
                if (i >= 0 && i < 8)
                {
                    c = BoardManager.Instance.Chessmans[i, j];
                    if (c == null || isWhite != c.isWhite) r[i, j] = true;
                }

                i++;
            }
        }
        
        i = CurrentX - 1;
        j = CurrentY - 1;
        if (CurrentY != 0)
        {
            for (int k = 0; k < 3; k++)
            {
                if (i >= 0 && i < 8)
                {
                    c = BoardManager.Instance.Chessmans[i, j];
                    if (c == null || isWhite != c.isWhite) r[i, j] = true;
                }

                i++;
            }
        }

        if (CurrentX != 0)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY];
            if (c == null || isWhite != c.isWhite) r[CurrentX - 1, CurrentY] = true;
        }

        if (CurrentX != 7)
        {
            c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY];
            if (c == null || isWhite != c.isWhite) r[CurrentX + 1, CurrentY] = true;
        }

        if ((isWhite && CurrentX == 4 && CurrentY == 0) || (!isWhite && CurrentX == 4 && CurrentY == 7))  //improve
        {
            c1 = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY];
            c2 = BoardManager.Instance.Chessmans[CurrentX + 2, CurrentY];
            c3 = BoardManager.Instance.Chessmans[CurrentX + 3, CurrentY];
            if (c1 == null && c2 == null && c3.GetType() == typeof(Rook) && c3.isWhite == isWhite)
            {
                r[CurrentX + 2, CurrentY] = true;
            }

            c1 = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY];
            c2 = BoardManager.Instance.Chessmans[CurrentX - 2, CurrentY];
            c3 = BoardManager.Instance.Chessmans[CurrentX - 3, CurrentY];
            c4 = BoardManager.Instance.Chessmans[CurrentX - 4, CurrentY];
            if (c1 == null && c2 == null && c3 == null && c4.GetType() == typeof(Rook) && c4.isWhite == isWhite)
            {
                r[CurrentX - 2, CurrentY] = true;
            }
        }

        return r;
    }
}
