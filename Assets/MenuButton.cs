using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour
{
    public void ButtonClick()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
