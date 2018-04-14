using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour
{
    public Canvas puzzle;
    public Canvas analyze;
    public Canvas newgame;
    public Canvas hint;
    public Canvas takeback;
    public Canvas exit;
    public Button playButton;
    public Slider slider;
    public Slider whiteSlider;
    public Slider blackSlider;

    bool active = false;

    private void Start()
    {
        puzzle.gameObject.SetActive(false);
        analyze.gameObject.SetActive(false);
        newgame.gameObject.SetActive(false);
        hint.gameObject.SetActive(false);
        takeback.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        //slider.gameObject.SetActive(false);
        whiteSlider.gameObject.SetActive(false);
        blackSlider.gameObject.SetActive(false);
        active = false;
    }

    public void ButtonClick()
    {
        active = !active;
        puzzle.gameObject.SetActive(active);
        analyze.gameObject.SetActive(active);
        newgame.gameObject.SetActive(active);
        hint.gameObject.SetActive(active);
        takeback.gameObject.SetActive(active);
        exit.gameObject.SetActive(active);
        playButton.gameObject.SetActive(active);
        //slider.gameObject.SetActive(active);
        whiteSlider.gameObject.SetActive(active);
        blackSlider.gameObject.SetActive(active);

        if (active) GameObject.Find("Menu").GetComponentInChildren<Text>().text = "Hide";
        else GameObject.Find("Menu").GetComponentInChildren<Text>().text = "Menu";
    }
}
