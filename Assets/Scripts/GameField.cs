using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameField : MonoBehaviour
{
    public int fieldSize = 4;
    public GameObject cellPrefab;
    public RectTransform gridTransform;

    private List<Cell> cells = new List<Cell>();
    private List<Vector2Int> allPositions = new List<Vector2Int>();

    private void Start()
    {
        Debug.Log("GameField Start");
        InitAllPositions();
        for (int x = 0; x < 2; x++)
        {
            CreateCell();
        }
    }

    void InitAllPositions()
    {
        for (int x = 0; x < fieldSize; x++)
        for (int y = 0; y < fieldSize; y++)
            allPositions.Add(new Vector2Int(x, y));
    }

    public Vector2Int GetEmptyPosition()
    {
        List<Vector2Int> empty = new List<Vector2Int>(allPositions);
        foreach (var cell in cells)
            empty.Remove(cell.Position);

        if (empty.Count == 0) return Vector2Int.zero;
        return empty[Random.Range(0, empty.Count)];
    }

    public void CreateCell()
    {
        Vector2Int pos = GetEmptyPosition();
        if (cells.Exists(c => c.Position == pos)) return;

        int val = Random.value < 0.9f ? 1 : 2;
        Cell newCell = new Cell(pos, val);
        cells.Add(newCell);

        GameObject go = Instantiate(cellPrefab, gridTransform);
        CellView view = go.GetComponent<CellView>();
        view.Init(newCell);
    }
}