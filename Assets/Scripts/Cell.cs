using System;
using UnityEngine;

public class Cell
{
    private Vector2Int position;
    private int value;

    public event Action OnValueChanged;
    public event Action OnPositionChanged;

    public Vector2Int Position
    {
        get => position;
        set 
        {
            if (position == value) return;
            position = value;
            OnPositionChanged?.Invoke();
        }
    }

    public int Value
    {
        get => value;
        set
        {
            if (this.value == value) return;
            this.value = value;
            OnValueChanged?.Invoke();
        }
    }

    public Cell(Vector2Int position, int value)
    {
        this.position = position;
        this.value = value;
    }
}