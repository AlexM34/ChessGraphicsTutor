using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    public SampleCode _sc;
    public Slider mainSlider;

    public void Start()
    {
        mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void ButtonClick ()
    {
        _sc.Flip();
    }

    public void ValueChangeCheck ()
    {
        _sc.Level((int)mainSlider.value);
    }
}
