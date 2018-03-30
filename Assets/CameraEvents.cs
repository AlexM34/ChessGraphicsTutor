using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEvents : MonoBehaviour
{
    public BoardManager _bm;
    public SampleCode _sc;

    public Camera WhiteCamera, BlackCamera;

    public void Update ()
    {
        if (_bm.isUserWhite) WhiteCam();
        else BlackCam();

        if (_bm.send)
        {
            _sc.User(_bm.x1, _bm.y1, _bm.x2, _bm.y2);
            _bm.send = false;
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
