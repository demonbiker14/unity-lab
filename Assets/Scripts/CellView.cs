using TMPro;
using UnityEngine;

public class CellView : MonoBehaviour
{
    public TextMeshProUGUI valueText;
    private Cell cell;

    public void Init(Cell cell)
    {
        this.cell = cell;
        
        cell.OnValueChanged += UpdateValue;
        cell.OnPositionChanged += UpdatePosition;
        

        UpdateValue();
        UpdatePosition();
    }

    void UpdateValue()
    {
        int num = (int)Mathf.Pow(2, cell.Value);
        valueText.text = num.ToString();
    }

    void UpdatePosition()
    {
        // Пример позиционирования в UI сетке
        RectTransform rt = GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(cell.Position.x * 100, cell.Position.y * 100);
    }
}