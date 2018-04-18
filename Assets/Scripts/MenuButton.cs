using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour
{
    public BoardManager _bm;
    public Canvas puzzle;
    public Canvas analyze;
    public Canvas newgame;
    public Canvas hint;
    public Canvas takeback;
    public Canvas exit;
    public Canvas evaluation;
    public Canvas image;
    public Button playButton;
    public Slider slider;
    public Slider whiteSlider;
    public Slider blackSlider;

    bool active = false;

    private void Start()
    {
        active = false;

        image.gameObject.SetActive(false);
        newgame.gameObject.SetActive(false);
        hint.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        whiteSlider.gameObject.SetActive(false);
        puzzle.gameObject.SetActive(false);
        analyze.gameObject.SetActive(false);
        takeback.gameObject.SetActive(false);
        evaluation.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        blackSlider.gameObject.SetActive(false);
    }

    public void ButtonClick()
    {
        active = !active;
        _bm.pause = !_bm.pause;
        image.gameObject.SetActive(active);

        if (active) GameObject.Find("Menu").GetComponentInChildren<Text>().text = "Hide";
        else GameObject.Find("Menu").GetComponentInChildren<Text>().text = "Menu";

        newgame.gameObject.SetActive(active);
        hint.gameObject.SetActive(active);
        exit.gameObject.SetActive(active);
        whiteSlider.gameObject.SetActive(active);
        puzzle.gameObject.SetActive(active);
        analyze.gameObject.SetActive(active);
        takeback.gameObject.SetActive(active);
        evaluation.gameObject.SetActive(active);
        playButton.gameObject.SetActive(active);
        blackSlider.gameObject.SetActive(active);
    }
}
