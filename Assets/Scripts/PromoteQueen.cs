using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromoteQueen : MonoBehaviour
{
    public BoardManager _bm;
    public Canvas canvas;

    private void Start()
    {
        Ready();
    }

    public void Promotion()
    {
        canvas.gameObject.SetActive(true);
    }

    public void Ready ()
    {
        canvas.gameObject.SetActive(false);
    }

    public void ButtonClick()
    {
        _bm.Promote(1);
    }
}
