using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEventsP : MonoBehaviour
{
    public BMPuzzles _bm;
    public Connect _connect;
    private int waiting = 0;

    public Camera WhiteCamera, BlackCamera;

    public void Update()
    {
        if (_bm.isUserWhite) WhiteCam();
        else BlackCam();

        if (_bm.send && !_bm.puzzleMode)
        {
            waiting++;
        }

        if (waiting == 10)
        {
            _connect.User(_bm.x1, _bm.y1, _bm.x2, _bm.y2);
            _bm.send = false;
            waiting = 0;
        }
    }

    private void WhiteCam()
    {
        WhiteCamera.enabled = true;
        BlackCamera.enabled = false;
    }

    private void BlackCam()
    {
        WhiteCamera.enabled = false;
        BlackCamera.enabled = true;
    }
}
