using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class CellsPanel : UIPanel {
    [SerializeField] private RectTransform _cellsParent;

    private List<Cell> _cells;
    private UICompanentsFactory _factory;
    
    public IReadOnlyList<Cell> Cells => _cells;
    
    [Inject]
    private void Construct(UICompanentsFactory companentsFactory) {
        _factory = companentsFactory;
    }

    public void Init() {
        CreateCells();
    }

    public void MoveActiveCell(OffsetDirections offset) {
        switch (offset) {
            case OffsetDirections.Left:

                break;

            case OffsetDirections.Up:

                break;
            case OffsetDirections.Down:

                break;

            case OffsetDirections.Right:

                break;

            default:
                throw new ArgumentException($"Invalid OffsetDirections: {offset}");
        }
    }

    private void CreateCells() {
        _cells = new List<Cell>();

        for (int row = 0; row < 10; row++) {
            for (int column = 0; column < 10; column++) {
                CellConfig config = new CellConfig();

                Cell newCell = _factory.Get<Cell>(config, _cellsParent);
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
