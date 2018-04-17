using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BMPuzzles : BoardManager
{
    public PuzzleP _puzzle;
    private int solved = 0;

    public override void Start()
    {
        Instance = this;
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        NewGame();
    }

    public override void NewGame()
    {
        foreach (GameObject go in activeChessman) Destroy(go);

        isWhiteTurn = true;
        isUserWhite = isWhiteTurn;
        isEngineOn = false;
        lineRenderer.enabled = false;
        isEnded = false;
        puzzleMode = true;
        whitedead = 0;
        blackdead = 0;
        moves = 0;
        solved = 0;

        for (int i = 0; i < 15; i++)
        {
            movetakenwhite[i] = -1;
            if (goWhite[i] != null) Destroy(goWhite[i].gameObject);
            movetakenblack[i] = -1;
            if (goBlack[i] != null) Destroy(goBlack[i].gameObject);
        }

        _puzzle.ButtonClick();
    }

    override public void EndGame(int result)
    {
        base.EndGame(result);
        solved++;
        _connect.text.text = solved.ToString();

        _puzzle.ButtonClick();
    }
}
