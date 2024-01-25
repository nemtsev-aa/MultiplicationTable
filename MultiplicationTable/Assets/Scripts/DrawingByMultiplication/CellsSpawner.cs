using System.Collections.Generic;
using UnityEngine;

public class CellsSpawner : MonoBehaviour {
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private RectTransform _cellsParent;

    private List<Cell> _cells;

    public IReadOnlyList<Cell> Cells => _cells;

    private void Start() {
        CreateCells();
    }

    private void CreateCells() {
        _cells = new List<Cell>();

        for (int row = 0; row < 10; row++) {
            for (int column = 0; column < 10; column++) {
                Cell newCell = Instantiate(_cellPrefab, _cellsParent);
                newCell.Position = new Vector2 (row, column);

                if (row == 0 && column > 0) 
                    newCell.TextValue = $"{column}";

                if (row > 0 && column == 0)
                    newCell.TextValue = $"{row}";

                _cells.Add(newCell);
            }
        }
    }

}
