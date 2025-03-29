using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class Cell
    {
        private Vector2Int position;
        private int value;

        public Action onValueChanged;
        public Action onPositionChanged;

        public Vector2Int Position
        {
            get => position;
            set
            {
                if (this.position == value) return;
                this.position = value;
                onPositionChanged.Invoke();
            }
        }

        public int Value
        {
            get => value;
            set
            {
                if (this.value == value) return;
                this.value = value;
                onValueChanged.Invoke();
            }
        }

        public Cell(Vector2Int position, int value)
        {
            this.position = position;
            this.value = value;
            
        }
    }
}