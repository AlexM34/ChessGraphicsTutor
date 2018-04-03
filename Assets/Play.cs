using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    public SampleCode _sc;
    public Slider mainSlider;
    public Text text;

    public void Start()
    {
        text = GetComponent<Text>();
        mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    private void Update()
    {
        text.text = "32131";// mainSlider.value.ToString() + "/10";
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
