using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Assets.Scripts
{
    public class GameField : MonoBehaviour
    {
        public GameObject cellPrefab;
        public RectTransform gridTransform;
        public TextMeshProUGUI scoreText;

        private Cell[][] field;
        private List<Vector2Int> allPositions = new();

        private void Awake()
        {
            field = new Cell[FieldConfig.FieldSize][];
            for (int i = 0; i < FieldConfig.FieldSize; i++)
            {
                field[i] = new Cell[FieldConfig.FieldSize];
            }
        }

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
            for (int x = 0; x < FieldConfig.FieldSize; x++)
            for (int y = 0; y < FieldConfig.FieldSize; y++)
                allPositions.Add(new Vector2Int(x, y));
        }


        public Vector2Int? GetEmptyPosition()
        {
            List<Vector2Int> empty = new List<Vector2Int>(allPositions);
            for (int i = 0; i < FieldConfig.FieldSize; i++)
            {
                for (int j = 0; j < FieldConfig.FieldSize; j++)
                {
                    if (field[i][j] != null)
                    {
                        empty.Remove(field[i][j].Position);
                    }
                }
            }

            if (empty.Count == 0) return null;
            return empty[Random.Range(0, empty.Count)];
        }

        public void CreateCell()
        {
            Vector2Int? pos = GetEmptyPosition();
            if (pos == null) return;
            if (field[pos.Value.x][pos.Value.y] != null) return;
            allPositions.Remove(pos.Value);

            int val = Random.value < 0.9f ? 1 : 2;
            Cell newCell = new Cell(pos.Value, val);

            GameObject go = Instantiate(cellPrefab, gridTransform);
            CellView view = go.GetComponent<CellView>();
            view.Init(newCell);
            field[pos.Value.x][pos.Value.y] = newCell;
        }

        private Vector2Int _convertPosition(Vector2Int dir, Vector2Int pos)
        {
            if (dir == Vector2Int.right)
            {
                return new Vector2Int(pos.y, pos.x);
            }

            if (dir == Vector2Int.left)
            {
                return new Vector2Int(pos.y, FieldConfig.FieldSize - pos.x - 1);
            }

            if (dir == Vector2Int.up)
            {
                return new Vector2Int(pos.x, pos.y);
            }

            if (dir == Vector2Int.down)
            {
                return new Vector2Int(FieldConfig.FieldSize - pos.x - 1, pos.y);
            }

            throw new Exception("Invalid direction");
        }

        private bool _checkBounds(Vector2Int pos)
        {
            return pos.x is >= 0 and < FieldConfig.FieldSize && pos.y is >= 0 and < FieldConfig.FieldSize;
        }


        private void _shiftCells(int k, Vector2Int dir, int a, int b)
        {
            if (b == FieldConfig.FieldSize - 1) return;
            for (int i = b - 1; i >= a; i--)
            {
                var origPos = _convertPosition(dir, new Vector2Int(k, i));
                var newPos = _convertPosition(dir, new Vector2Int(k, i + 1));
                
                if (field[origPos.x][origPos.y] == null) continue;
                var cell = field[origPos.x][origPos.y];
                cell.Position = newPos;
                
                field[newPos.x][newPos.y] = cell;
                field[origPos.x][origPos.y] = null;
            }

            Debug.LogFormat("Shifted k={0} from {1} to {2} by {3}", k, a, b, dir);
        }

        public void Move(Vector2Int dir)
        {
            for (int i = 0; i < FieldConfig.FieldSize; i++)
            {
                int? start = null;
                for (int j = 0; j < FieldConfig.FieldSize - 1; j++)
                {
                    if (field[i][j] == null) continue;
                    var origPos = _convertPosition(dir, new Vector2Int(i, j));
                    if (field[i] == null || field[i][j] == null) continue;

                    if (start == null)
                    {
                        start = j;
                    }

                    var newPos = origPos + dir;
                    if (!_checkBounds(newPos)) continue;

                    if (field[newPos.x][newPos.y] == null)
                    {
                        _shiftCells(i, dir, start.Value, j + 1);
                        start += 1;
                    }
                }
            }

            int sum = 0;
            for (int i = 0; i < FieldConfig.FieldSize; i++)
            {
                for (int j = 0; j < FieldConfig.FieldSize; j++)
                {
                    if (field[i] == null || field[i][j] == null) continue;
                    sum += (int)Mathf.Pow(2, field[i][j].Value);
                }
            }

            scoreText.text = "Score: " + sum;
        }
    }
}