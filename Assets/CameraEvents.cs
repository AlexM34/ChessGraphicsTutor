using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEvents : MonoBehaviour
{
    public BoardManager _bm;
    public SampleCode _sc;
    private int waiting = 0;

    public Camera WhiteCamera, BlackCamera;

    public void Update ()
    {
        if (_bm.isUserWhite) WhiteCam();
        else BlackCam();
        
        if (_bm.puzzleMode && !_bm.isWhiteTurn)
        {
            //_bm.SelectChessman(_bm.solution[4 * _bm.puzzleMoves - 4], _bm.solution[4 * _bm.puzzleMoves - 3]);
            //_bm.MoveChessman(_bm.solution[4 * _bm.puzzleMoves - 2], _bm.solution[4 * _bm.puzzleMoves - 1]);
        }

        if (_bm.send && !_bm.puzzleMode)
        {
            waiting++;
        }

        if (waiting == 10)
        {
            _sc.User(_bm.x1, _bm.y1, _bm.x2, _bm.y2);
            _bm.send = false;
            waiting = 0;
        }
    }

    private void WhiteCam ()
    {
        WhiteCamera.enabled = true;
        BlackCamera.enabled = false;
    }

    private void BlackCam ()
    {
        WhiteCamera.enabled = false;
        BlackCamera.enabled = true;
    }
}
