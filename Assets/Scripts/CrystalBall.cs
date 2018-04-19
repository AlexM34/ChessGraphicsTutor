using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBall : MonoBehaviour
{
    public Material mat;

    Color good = new Color
    {
        a = 1f,
        r = 0f,
        g = 1f,
        b = 0f
    };

    Color bad = new Color
    {
        a = 1f,
        r = 1f,
        g = 0f,
        b = 0f
    };

    public void Eval(int value)
    {
        if (value == 1000)
        {
            mat.color = Color.white;
            return;
        }

        if (value > 100) value = 100;
        else if (value < -200) value = -200;
        value = 100 - value;

        Color current = new Color
        {
            a = 1f,
            r = value / 300f,
            g = 1f - value / 300f,
            b = 0f
        };

        mat.color = current;
    }
}
