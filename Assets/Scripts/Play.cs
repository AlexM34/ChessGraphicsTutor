using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    public Connect _connect;
    public Slider mainSlider;
    public Slider whiteSlider;
    public Slider blackSlider;

    private void Start()
    {
        mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        whiteSlider.onValueChanged.AddListener(delegate { WhiteChange(); });
        blackSlider.onValueChanged.AddListener(delegate { BlackChange(); });
    }

    public void ButtonClick ()
    {
        _connect.Flip();
    }

    public void ValueChangeCheck ()
    {
        _connect.Level((int)mainSlider.value);
    }

    public void WhiteChange()
    {
        _connect.timeWhite = ((int)whiteSlider.value);
    }

    public void BlackChange()
    {
        _connect.timeBlack = ((int)blackSlider.value);
    }
}
