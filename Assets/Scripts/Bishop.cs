using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Chessman
{
    public override bool[,] PossibleMove ()
    {
        bool[,] r = new bool[8, 8];
        Chessman c;
        int i = CurrentX - 1, j = CurrentY + 1;
        
        while (i >= 0 && j < 8)
        {
            c = BoardManager.Instance.Chessmans[i, j];
            if (c == null) r[i, j] = true;
            else
            {
                if (isWhite != c.isWhite) r[i, j] = true;
                break;
            }

            i--;
            j++;
        }

        i = CurrentX + 1;
        j = CurrentY + 1;

        while (i < 8 && j < 8)
        {
            c = BoardManager.Instance.Chessmans[i, j];
            if (c == null) r[i, j] = true;
            else
            {
                if (isWhite != c.isWhite) r[i, j] = true;
                break;
            }

            i++;
            j++;
        }

        i = CurrentX - 1;
        j = CurrentY - 1;

        while (i >= 0 && j >= 0)
        {
            c = BoardManager.Instance.Chessmans[i, j];
            if (c == null) r[i, j] = true;
            else
            {
                if (isWhite != c.isWhite) r[i, j] = true;
                break;
            }

            i--;
            j--;
        }

        i = CurrentX + 1;
        j = CurrentY - 1;

        while (i < 8 && j >= 0)
        {
            c = BoardManager.Instance.Chessmans[i, j];
            if (c == null) r[i, j] = true;
            else
            {
                if (isWhite != c.isWhite) r[i, j] = true;
                break;
            }

            i++;
            j--;
        }

        return r;
    }
}
