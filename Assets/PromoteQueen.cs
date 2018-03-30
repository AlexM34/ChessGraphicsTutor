using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromoteQueen : MonoBehaviour
{
    public BoardManager _bm;
    public Canvas canvas;
    public bool go = false;

    private void Start()
    {
        Ready();
    }

    public IEnumerator Pr()
    {
        canvas.gameObject.SetActive(true);
        yield return new WaitUntil(() => go);
        //canvas.gameObject.SetActive(false);
        go = false;
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
