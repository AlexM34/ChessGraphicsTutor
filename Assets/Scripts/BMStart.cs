using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BMStart : BoardManager
{
    public override void SpawnAllChessmans()
    {
        activeChessman = new List<GameObject>();
        Chessmans = new Chessman[8, 8];
        EnPassantMove = new int[2] { -1, -1 };

        SpawnChessman(0, 6, 0);
        SpawnChessman(2, 3, 0);
        SpawnChessman(3, 3, 6);
        SpawnChessman(3, 0, 2);
        SpawnChessman(5, 0, 1);
        SpawnChessman(5, 2, 2);
        SpawnChessman(5, 5, 1);
        SpawnChessman(5, 5, 5);
        SpawnChessman(5, 6, 1);
        SpawnChessman(5, 7, 1);

        SpawnChessman(6, 5, 7);
        SpawnChessman(7, 5, 2);
        SpawnChessman(8, 1, 7);
        SpawnChessman(8, 6, 7);
        SpawnChessman(9, 1, 5);
        SpawnChessman(9, 1, 6);
        SpawnChessman(10, 4, 6);
        SpawnChessman(11, 0, 6);
        SpawnChessman(11, 2, 6);
        SpawnChessman(11, 5, 6);
        SpawnChessman(11, 7, 6);
    }
}
