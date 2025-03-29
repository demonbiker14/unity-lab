using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class CellView : MonoBehaviour
    {
        const float CellSize = 130.0f;
        const float CellSpacing = 14.0f;
        public TextMeshProUGUI valueText;
        private Cell _cell;

        public void Init(Cell cell)
        {
            _cell = cell;


            cell.onPositionChanged = UpdatePosition;
            cell.onValueChanged = UpdateValue;

            UpdateValue();
            UpdatePosition();
        }


        void UpdateValue()
        {
            int num = (int)Mathf.Pow(2, _cell.Value);
            valueText.text = num.ToString();
        }

        void UpdatePosition()
        {
            Debug.Log("UpdatePosition");
            RectTransform rt = GetComponent<RectTransform>();

            const float delta = CellSize + CellSpacing;
            int x = _cell.Position.x - FieldConfig.FieldSize / 2;
            int y = _cell.Position.y - FieldConfig.FieldSize / 2;
            rt.anchoredPosition = new Vector2(
                x * delta + delta / 2,
                -y * delta - delta / 2
            );
        }
    }
}