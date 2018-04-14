using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public Connect _connect;
    public CameraEvents _camera;
    public PromoteQueen _queen;
    public PromoteRook _rook;
    public PromoteBishop _bishop;
    public PromoteKnight _knight;

    public static BoardManager Instance { set; get; }
    private bool[,] allowedMoves { set; get; }

    public Chessman[,] Chessmans { set; get;}
    public Chessman selectedChessman;

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;
    public bool isUserWhite = true;
    public int x1 = -1, y1 = -1, x2 = -1, y2 = -1;
    public bool send = false;
    public bool isEngineOn = true;
    public bool isEnded = false;
    public bool wait = false;
    public bool puzzleMode = false;
    private int[] from = new int[400];
    private int[] to = new int[400];
    private int[] piece_from = new int[400];
    private int[] piece_to = new int[400];
    private int[] ep = new int[400];
    public int[] solution = new int[100];
    public int moves = 0;
    public int piece_x = -1;
    public int piece_y = -1;
    public int puzzleMoves = 100;
    private int whitedead = 0;
    private int blackdead = 0;
    public int[] movetakenwhite = new int[15];
    public int[] movetakenblack = new int[15];
    private GameObject[] goWhite = new GameObject[15];
    private GameObject[] goBlack = new GameObject[15];

    Color color = new Color
    {
        a = 1f,
        r = 0.96f,
        g = 0.22f,
        b = 0.9f
    };

    public List<GameObject> chessmanPrefabs;
    public List<GameObject> activeChessman = new List<GameObject>();
    private LineRenderer lineRenderer;

    private Material previousMat;
    public Material selectedMat;

    public int[] EnPassantMove { set; get; }

    public bool isWhiteTurn = true;

    private void Start()
    {
        Instance = this;
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.enabled = false;

        for (int i = 0; i < 15; i++)
        {
            movetakenwhite[i] = -1;
            if (goWhite[i] != null) Destroy(goWhite[i].gameObject);
            movetakenblack[i] = -1;
            if (goBlack[i] != null) Destroy(goBlack[i].gameObject);
        }

        SpawnAllChessmans();
        //_queen.Promotion();
    }

    public void NewGame ()
    {
        foreach (GameObject go in activeChessman) Destroy(go);

        isWhiteTurn = true;
        if (!isEngineOn) isUserWhite = true;
        lineRenderer.enabled = false;
        isEnded = false;
        puzzleMode = false;
        whitedead = 0;
        blackdead = 0;
        moves = 0;

        for (int i = 0; i < 15; i++)
        {
            movetakenwhite[i] = -1;
            if (goWhite[i] != null) Destroy(goWhite[i].gameObject);
            movetakenblack[i] = -1;
            if (goBlack[i] != null) Destroy(goBlack[i].gameObject);
        }

        BoardHighlights.Instance.HideHighlights();

        Instance = this;
        SpawnAllChessmans();
    }

    private void Update()
    {
        UpdateSelection();
        DrawChessBoard();

        if (Input.GetMouseButtonDown(0))
        {
            if (selectionX >= 0 && selectionY >= 0)
            {
                if (selectedChessman == null)
                {
                    SelectChessman(selectionX, selectionY);
                }
                else
                {
                    MoveChessman(selectionX, selectionY);
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && selectedChessman != null)
        {
            if (selectedChessman != Chessmans[selectionX, selectionY])
            {
                MoveChessman(selectionX, selectionY);
            }
        }
    }

    public void Play(int x1, int y1, int x2, int y2)
    {
        SelectChessman(x1, y1);
        MoveChessman(x2, y2);
    }

    public void SelectChessman(int x, int y)
    {
        if (isEnded || wait) return;
        if (Chessmans[x, y] == null) return;
        if (Chessmans[x, y].isWhite != isWhiteTurn) return;

        bool hasAtleastOneMove = false;
        allowedMoves = Chessmans[x, y].PossibleMove();
        piece_x = x;
        piece_y = y;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (allowedMoves[i, j] && (puzzleMode || isUserWhite != isWhiteTurn || _connect.Possible(i, j))) hasAtleastOneMove = true;
            }
        }

        if (!hasAtleastOneMove) return;

        selectedChessman = Chessmans[x, y];
        previousMat = selectedChessman.GetComponent<MeshRenderer>().material;
        selectedMat.mainTexture = previousMat.mainTexture;
        selectedChessman.GetComponent<MeshRenderer>().material = selectedMat;
        BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);
    }

    public void MoveChessman(int x, int y)
    {
        if (isEnded || wait) return;
        x1 = -1;
        y1 = -1;
        x2 = -1;
        y2 = -1;
        if (x < 0 || x > 7 || y < 0 || y > 7 || (!puzzleMode && isUserWhite == isWhiteTurn && !_connect.Possible(x, y)))
        {
            selectedChessman.GetComponent<MeshRenderer>().material = previousMat;
            BoardHighlights.Instance.HideHighlights();
            selectedChessman = null;
            return;
        }

        if (allowedMoves[x,y])
        {
            x1 = selectedChessman.CurrentX;
            y1 = selectedChessman.CurrentY;
            x2 = x;
            y2 = y;

            Record();

            if (puzzleMode && isWhiteTurn == isUserWhite && ((x1 != solution[4 * puzzleMoves - 4]) ||
                (y1 != solution[4 * puzzleMoves - 3]) || (x2 != solution[4 * puzzleMoves - 2])
                || (y2 != solution[4 * puzzleMoves - 1])))
            {
                selectedChessman.GetComponent<MeshRenderer>().material = previousMat;
                BoardHighlights.Instance.HideHighlights();
                selectedChessman = null;
                return;
            }

            Chessman c = Chessmans[x, y];
            if (c != null && c.isWhite != isWhiteTurn)
            {
                Capture(x, y);
                activeChessman.Remove(c.gameObject);
                Destroy(c.gameObject);
            }

            if (x == EnPassantMove[0] && y == EnPassantMove[1])
            {
                if (isWhiteTurn) c = Chessmans[x, y - 1];
                else c = Chessmans[x, y + 1];

                activeChessman.Remove(c.gameObject);
                Destroy(c.gameObject);
            }
            EnPassantMove[0] = -1;
            EnPassantMove[1] = -1;

            if (selectedChessman.GetType() == typeof(Pawn))
            {
                if (y == 7 || y == 0)
                {
                    if (isUserWhite == isWhiteTurn)
                    {
                        wait = true;
                        _queen.Promotion();
                        _rook.Promotion();
                        _bishop.Promotion();
                        _knight.Promotion();
                        return;
                    }

                    else
                    {
                        int t1 = 1;
                        if (isUserWhite) t1 += 6;
                        activeChessman.Remove(selectedChessman.gameObject);
                        Destroy(selectedChessman.gameObject);
                        SpawnChessman(t1, x2, y2);
                        selectedChessman = Chessmans[x2, y2];
                    }
                }

                if (selectedChessman.CurrentY == 1 && y == 3)
                {
                    EnPassantMove[0] = x;
                    EnPassantMove[1] = y - 1;
                }

                else if (selectedChessman.CurrentY == 6 && y == 4)
                {
                    EnPassantMove[0] = x;
                    EnPassantMove[1] = y + 1;
                }

                ep[moves] = EnPassantMove[0] * 10 + EnPassantMove[1];
            }
            else if (selectedChessman.GetType() == typeof(King) && selectedChessman.CurrentX == 4)
            {
                int side = 2;
                if (!isWhiteTurn) side += 6;
                if (x == 6)
                {
                    c = Chessmans[7, y];
                    Destroy(c.gameObject);
                    SpawnChessman(side, 5, y);
                }
                else if (x == 2)
                {
                    c = Chessmans[0, y];
                    Destroy(c.gameObject);
                    SpawnChessman(side, 3, y);
                }
            }
            
            x1 = selectedChessman.CurrentX;
            y1 = selectedChessman.CurrentY;
            x2 = x;
            y2 = y;
            Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY] = null;
            selectedChessman.transform.position = GetTileCenter(x, y);
            selectedChessman.SetPosition(x, y);
            Chessmans[x, y] = selectedChessman;
            isWhiteTurn = !isWhiteTurn;
        }

        selectedChessman.GetComponent<MeshRenderer>().material = previousMat;
        BoardHighlights.Instance.HideHighlights();
        selectedChessman = null;
        if (x1 != -1)
        {
            lineRenderer.enabled = true;
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
            lineRenderer.material.color = color;
            lineRenderer.startWidth = 0.3f;
            Vector3 from = GetTileCenter(x1, y1);
            Vector3 to = GetTileCenter(x2, y2);
            if (from.z < to.z)
            {
                from.z -= 0.2f;
                to.z += 0.2f;
            }
            else if (from.z > to.z)
            {
                from.z += 0.2f;
                to.z -= 0.2f;
            }

            lineRenderer.SetPosition(0, from);
            lineRenderer.SetPosition(1, to);

            if (!isEngineOn && !puzzleMode) isUserWhite = !isUserWhite;
            if (isUserWhite != isWhiteTurn || !isEngineOn) send = true;
        }

        if (puzzleMode && x1 != -1)
        {
            puzzleMoves--;
            if (puzzleMoves == 0)
            {
                if (isUserWhite) EndGame(1);
                else EndGame(-1);
            }

            if (isWhiteTurn != isUserWhite)
            {
                SelectChessman(solution[4 * puzzleMoves - 4], solution[4 * puzzleMoves - 3]);
                MoveChessman(solution[4 * puzzleMoves - 2], solution[4 * puzzleMoves - 1]);
            }
        }

    }

    public void Promote (int type)
    {
        if (!isWhiteTurn) type += 6;
        activeChessman.Remove(selectedChessman.gameObject);
        Destroy(selectedChessman.gameObject);
        SpawnChessman(type, x2, y2);
        selectedChessman = Chessmans[x2, y2];

        Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY] = null;
        selectedChessman.transform.position = GetTileCenter(x2, y2);
        selectedChessman.SetPosition(x2, y2);
        Chessmans[x2, y2] = selectedChessman;
        isWhiteTurn = !isWhiteTurn;

        selectedChessman.GetComponent<MeshRenderer>().material = previousMat;
        BoardHighlights.Instance.HideHighlights();
        selectedChessman = null;
        if (x1 != -1)
        {
            lineRenderer.enabled = true;
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
            lineRenderer.material.color = color;
            lineRenderer.startWidth = 0.3f;
            Vector3 vfrom = GetTileCenter(x1, y1);
            Vector3 vto = GetTileCenter(x2, y2);
            if (vfrom.z < vto.z)
            {
                vfrom.z -= 0.2f;
                vto.z += 0.2f;
            }
            else if (vfrom.z > vto.z)
            {
                vfrom.z += 0.2f;
                vto.z -= 0.2f;
            }

            lineRenderer.SetPosition(0, vfrom);
            lineRenderer.SetPosition(1, vto);

            if (!isEngineOn) isUserWhite = !isUserWhite;
            if (isUserWhite != isWhiteTurn || !isEngineOn) send = true;
        }
        
        wait = false;
        _queen.Ready();
        _rook.Ready();
        _bishop.Ready();
        _knight.Ready();
    }

    private void Capture (int x, int y)
    {
        _connect.text.text = moves.ToString();
        int type = TypeAsInt(x, y);

        Vector3 o;
        if (type < 6)
        {
            o = new Vector3(10f + whitedead / 6, -0.05f, 4.5f + whitedead % 6);
            if (type == 4)
            {
                o.x -= 0.15f;
                o.y += 0.6f;
                o.z -= 0.09f;
            }
            movetakenwhite[whitedead] = moves;
            whitedead++;
        }
        else
        {
            o = new Vector3(-2f - blackdead / 6, -0.05f, 3.5f - blackdead % 6);
            if (type == 10)
            {
                o.x += 0.15f;
                o.y += 0.6f;
                o.z += 0.09f;
            }
            movetakenblack[blackdead] = moves;
            blackdead++;
        }

        GameObject go = Instantiate(chessmanPrefabs[type], o, Quaternion.identity) as GameObject;
        go.transform.SetParent(transform);

        if (type < 6) goWhite[whitedead - 1] = go;
        else goBlack[blackdead - 1] = go;

        //Chessmans[whitedead + 10, blackdead + 10] = go.GetComponent<Chessman>();
        //Chessmans[whitedead + 10, blackdead + 10].SetPosition(whitedead + 10, blackdead + 10);
    }

    private void Record()
    {
        from[moves] = x1 * 10 + y1;
        to[moves] = x2 * 10 + y2;
        piece_from[moves] = TypeAsInt(x1, y1);
        piece_to[moves] = TypeAsInt(x2, y2);
        ep[moves] = EnPassantMove[0] * 10 + EnPassantMove[1];
        moves++;
    }

    private int TypeAsInt (int x, int y)
    {
        int t = -100;
        if (Chessmans[x, y] == null) return t;
        else if (Chessmans[x, y].GetType() == typeof(King)) t = 0;
        else if (Chessmans[x, y].GetType() == typeof(Queen)) t = 1;
        else if (Chessmans[x, y].GetType() == typeof(Rook)) t = 2;
        else if (Chessmans[x, y].GetType() == typeof(Bishop)) t = 3;
        else if (Chessmans[x, y].GetType() == typeof(Knight)) t = 4;
        else if (Chessmans[x, y].GetType() == typeof(Pawn)) t = 5;

        if (!Chessmans[x, y].isWhite) t += 6;

        return t;
    }

    public void Takeback()
    {
        if (selectedChessman != null)
        {
            //SelectChessman(selectedChessman.CurrentX, selectedChessman.CurrentY);
            selectedChessman.GetComponent<MeshRenderer>().material = previousMat;
            BoardHighlights.Instance.HideHighlights();
            selectedChessman = null;
        }

        lineRenderer.enabled = false;
        if (moves > 0) moves--;
        EnPassantMove[0] = ep[moves] / 10;
        EnPassantMove[1] = ep[moves] % 10;
        int xfrom = from[moves] / 10;
        int yfrom = from[moves] % 10;
        int xto = to[moves] / 10;
        int yto = to[moves] % 10;

        activeChessman.Remove(Chessmans[xto, yto].gameObject);
        Destroy(Chessmans[xto, yto].gameObject);
        SpawnChessman(piece_from[moves], xfrom, yfrom);
        if (piece_to[moves] >= 0) SpawnChessman(piece_to[moves], xto, yto);
        
        if (piece_from[moves] % 6 == 0 && Mathf.Abs(xfrom - xto) == 2)
        {
            int side = 0;
            if (isWhiteTurn) side += 6;
            if (xto == 6)
            {
                activeChessman.Remove(Chessmans[5, yto].gameObject);
                Destroy(Chessmans[5, yto].gameObject);
                SpawnChessman(2 + side, 7, yto);
            }
            else if (xto == 2)
            {
                activeChessman.Remove(Chessmans[3, yto].gameObject);
                Destroy(Chessmans[3, yto].gameObject);
                SpawnChessman(2 + side, 0, yto);
            }
        }

        if (moves > 0)
        {
            lineRenderer.enabled = true;
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
            lineRenderer.material.color = color;
            lineRenderer.startWidth = 0.3f;
            Vector3 vfrom = GetTileCenter(from[moves - 1] / 10, from[moves - 1] % 10);
            Vector3 vto = GetTileCenter(to[moves - 1] / 10, to[moves - 1] % 10);
            if (vfrom.z < vto.z)
            {
                vfrom.z -= 0.2f;
                vto.z += 0.2f;
            }
            else if (vfrom.z > vto.z)
            {
                vfrom.z += 0.2f;
                vto.z -= 0.2f;
            }

            lineRenderer.SetPosition(0, vfrom);
            lineRenderer.SetPosition(1, vto);
        }

        isWhiteTurn = !isWhiteTurn;
        if (!isEngineOn && !puzzleMode) isUserWhite = !isUserWhite;

        if (whitedead > 0 && movetakenwhite[whitedead-1] == moves+1)
        {
            movetakenwhite[whitedead - 1] = -1;
            whitedead--;
            Destroy(goWhite[whitedead].gameObject);
            //Chessman c = Chessmans[whitedead + 9, blackdead + 10];//(int)10f + whitedead / 6, (int)4.5f + whitedead % 6];
            //Destroy(c.gameObject);
        }

        if (blackdead > 0 && movetakenblack[blackdead-1] == moves+1)
        {
            movetakenblack[blackdead - 1] = -1;
            blackdead--;
            Destroy(goBlack[blackdead].gameObject);
            //Chessman c = Chessmans[whitedead + 10, blackdead + 9];
            //Destroy(c.gameObject);
        }
    }

    private void UpdateSelection()
    {
        if (!_camera.gameObject.activeSelf) return;

        RaycastHit hit;
        if (_camera.WhiteCamera.isActiveAndEnabled)
        {
            if (Physics.Raycast(_camera.WhiteCamera.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("ChessPlane")))
            {
                selectionX = (int)hit.point.x;
                selectionY = (int)hit.point.z;
            }
            else
            {
                selectionX = -1;
                selectionY = -1;
            }
        }

        else if (_camera.BlackCamera.isActiveAndEnabled)
        {
            if (Physics.Raycast(_camera.BlackCamera.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("ChessPlane")))
            {
                selectionX = (int)hit.point.x;
                selectionY = (int)hit.point.z;
            }
            else
            {
                selectionX = -1;
                selectionY = -1;
            }
        }
    }

    public void Puzzle (int[] pos, int[] sol, bool white)
    {
        foreach (GameObject go in activeChessman) Destroy(go);

        isWhiteTurn = white;
        isUserWhite = isWhiteTurn;
        isEngineOn = false;
        lineRenderer.enabled = false;
        isEnded = false;
        puzzleMode = true;
        moves = 0;
        BoardHighlights.Instance.HideHighlights();

        Instance = this;

        activeChessman = new List<GameObject>();
        Chessmans = new Chessman[8, 8];
        EnPassantMove = new int[2] { -1, -1 };

        for (int i = 0; i < pos.Length; i += 3)
        {
            SpawnChessman(pos[i], pos[i + 1], pos[i + 2]);
        }

        puzzleMoves = sol.Length / 4;
        for (int i = 0; i < sol.Length; i += 4)
        {
            solution[i] = sol[sol.Length - 4 - i];
            solution[i+1] = sol[sol.Length - 3 - i];
            solution[i+2] = sol[sol.Length - 2 - i];
            solution[i+3] = sol[sol.Length - 1 - i];
        }
    }

    public void SpawnChessman(int index, int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin = GetTileCenter(x, y);
        if (index % 6 == 4)
        {
            if (index == 4)
            {
                origin.x -= 0.15f;
                origin.y = 0.6f;
                origin.z -= 0.15f;
            }

            else if (index == 10)
            {
                origin.x += 0.15f;
                origin.y = 0.6f;
                origin.z += 0.15f;
            }
        }

        if (index % 6 != 4 && index % 6 != 5)
        {
            if (index < 6) origin.z -= 0.1f;
            else origin.z += 0.1f;
        }

        GameObject go = Instantiate(chessmanPrefabs[index], origin, Quaternion.identity) as GameObject;
        go.transform.SetParent(transform);
        Chessmans[x, y] = go.GetComponent<Chessman>();
        Chessmans[x, y].SetPosition(x, y);
        activeChessman.Add(go);
    }

    public virtual void SpawnAllChessmans()
    {
        activeChessman = new List<GameObject>();
        Chessmans = new Chessman[8, 8];
        EnPassantMove = new int[2] { -1, -1 };

        SpawnChessman(0, 4, 0);
        SpawnChessman(1, 3, 0);
        SpawnChessman(2, 0, 0);
        SpawnChessman(2, 7, 0);
        SpawnChessman(3, 2, 0);
        SpawnChessman(3, 5, 0);
        SpawnChessman(4, 1, 0);
        SpawnChessman(4, 6, 0);

        for (int i = 0; i < 8; i++) SpawnChessman(5, i, 1);

        SpawnChessman(6, 4, 7);
        SpawnChessman(7, 3, 7);
        SpawnChessman(8, 0, 7);
        SpawnChessman(8, 7, 7);
        SpawnChessman(9, 2, 7);
        SpawnChessman(9, 5, 7);
        SpawnChessman(10, 1, 7);
        SpawnChessman(10, 6, 7);

        for (int i = 0; i < 8; i++) SpawnChessman(11, i, 6);
    }

    private Vector3 GetTileCenter(int x, int y)
    {
        Chessman c = Chessmans[x, y];
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        if (selectedChessman != null)
        {
            if (selectedChessman.GetType() == typeof(Knight))
            {
                if (selectedChessman.isWhite)
                {
                    origin.x -= 0.15f;
                    origin.y = 0.6f;
                    origin.z -= 0.15f;
                }
                else
                {
                    origin.x += 0.15f;
                    origin.y = 0.6f;
                    //origin.z += 0.15f;
                }
            }

            else if (selectedChessman.GetType() != typeof(Pawn) && selectedChessman.GetType() != typeof(Bishop) && selectedChessman.isWhite)
            {
                if (selectedChessman.isWhite) origin.z -= 0.1f;
                else origin.z += 0.1f;
            }
        }
        else
        {
            origin.y = 0;
        }

        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        return origin;
    }

    private void DrawChessBoard()
    {
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heightLine = Vector3.forward * 8;

        for (int i = 0; i <= 8; i++)
        {
            Vector3 start = Vector3.forward * i;
            //Debug.DrawLine(start, start + widthLine);

            start = Vector3.right * i;
            //Debug.DrawLine(start, start + heightLine);
        }

        // Draw the selection
        if (selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(
                Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));

            Debug.DrawLine(
                Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
                Vector3.forward * selectionY + Vector3.right * (selectionX + 1));
        }
    }

    public void EndGame (int result)
    {
        isEnded = true;

        if (result == 1)
        {
            _connect.text.text = "White wins!";
            Debug.Log("White wins!");
        }
        else if (result == 0)
        {
            _connect.text.text = "Draw!";
            Debug.Log("Draw!");
        }
        else
        {
            _connect.text.text = "Black wins!";
            Debug.Log("Black wins!");
        }
    }
}
