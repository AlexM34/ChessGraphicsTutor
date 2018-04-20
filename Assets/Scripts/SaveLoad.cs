using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public GameControl _control;

    public void Save()
    {
        _control.Save();
    }

    public void Load()
    {
        _control.Load();
    }
}
