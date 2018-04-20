using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class GameControl : MonoBehaviour
{
    public static GameControl control;
    public BoardManager _bm;

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameInfo.dat");

        Game data = new Game
        {
            moves = _bm.moves,
            timeWhite = _bm._connect.timeWhite,
            timeBlack = _bm._connect.timeBlack
        };

        for (int i = 0; i < 400; i++)
        {
            data.from[i] = _bm.from[i];
            data.to[i] = _bm.to[i];
        }

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/gameInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat", FileMode.Open);
            Game data = (Game)bf.Deserialize(file);
            file.Close();

            _bm._connect.New();
            _bm.moves = data.moves;
            _bm._connect.timeWhite = data.timeWhite;
            _bm._connect.timeBlack = data.timeBlack;
            for (int i = 0; i < 400; i++)
            {
                _bm.from[i] = data.from[i];
                _bm.to[i] = data.to[i];
            }
            _bm.Reload();
        }
    }
}

[Serializable]
class Game
{
    public int moves;
    public int[] from = new int[400];
    public int[] to = new int[400];
    public double timeWhite;
    public double timeBlack;
}