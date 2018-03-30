using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        Chessman c;
        int i = CurrentX + 1;
        while (i < 8)
        {
            c = BoardManager.Instance.Chessmans[i, CurrentY];
            if (c == null) r[i, CurrentY] = true;
            else
            {
                if (c.isWhite != isWhite) r[i, CurrentY] = true;
                break;
            }

            i++;
        }

        i = CurrentX - 1;
        while (i >= 0)
        {
            c = BoardManager.Instance.Chessmans[i, CurrentY];
            if (c == null) r[i, CurrentY] = true;
            else
            {
                if (c.isWhite != isWhite) r[i, CurrentY] = true;
                break;
            }

            i--;
        }

        i = CurrentY + 1;
        while (i < 8)
        {
            c = BoardManager.Instance.Chessmans[CurrentX, i];
            if (c == null) r[CurrentX, i] = true;
            else
            {
                if (c.isWhite != isWhite) r[CurrentX, i] = true;
                break;
            }

            i++;
        }

        i = CurrentY - 1;
        while (i >= 0)
        {
            c = BoardManager.Instance.Chessmans[CurrentX, i];
            if (c == null) r[CurrentX, i] = true;
            else
            {
                if (c.isWhite != isWhite) r[CurrentX, i] = true;
                break;
            }

            i--;
        }

        i = CurrentX - 1;
        int j = CurrentY + 1;

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
