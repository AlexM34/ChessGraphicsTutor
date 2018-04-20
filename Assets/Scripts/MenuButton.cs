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
    public Canvas coach;
    public Canvas saveload;
    public Button playButton;
    public Slider slider;
    public Slider whiteSlider;
    public Slider blackSlider;

    bool active = true;

    private void Start()
    {
        active = true;

        image.gameObject.SetActive(true);
        GameObject.Find("Menu").GetComponentInChildren<Text>().text = "Hide";
        newgame.gameObject.SetActive(true);
        hint.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);
        whiteSlider.gameObject.SetActive(true);
        puzzle.gameObject.SetActive(true);
        analyze.gameObject.SetActive(true);
        takeback.gameObject.SetActive(true);
        evaluation.gameObject.SetActive(true);
        saveload.gameObject.SetActive(true);
        coach.gameObject.SetActive(true);
        playButton.gameObject.SetActive(true);
        blackSlider.gameObject.SetActive(true);
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
        saveload.gameObject.SetActive(active);
        coach.gameObject.SetActive(active);
        playButton.gameObject.SetActive(active);
        blackSlider.gameObject.SetActive(active);
    }
}
