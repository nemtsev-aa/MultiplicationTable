using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class CellsPanel : UIPanel {
    [SerializeField] private RectTransform _cellsParent;
    [SerializeField] private Cell _activeCell;

    private List<Cell> _cells;
    private UICompanentsFactory _factory;
    
    [Inject]
    private void Construct(UICompanentsFactory companentsFactory) {
        _factory = companentsFactory;
    }

    public void Init() {
        CreateCells();

        _activeCell = _cells[0];
    }

    public void MoveActiveCell(OffsetDirections offsetDirection) {
        _activeCell.Position = GetNewPosition(offsetDirection);
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

    private Vector2 GetNewPosition(OffsetDirections offsetDirection) {
        Vector2 offsetValue = Vector2.zero;

        switch (offsetDirection) {
            case OffsetDirections.Left:
                offsetValue = new Vector2(_activeCell.Position.x - 1, _activeCell.Position.y);
                break;

            case OffsetDirections.Up:
                offsetValue = new Vector2(_activeCell.Position.x, _activeCell.Position.y + 1);
                break;

            case OffsetDirections.Down:
                offsetValue = new Vector2(_activeCell.Position.x, _activeCell.Position.y - 1);
                break;

            case OffsetDirections.Right:
                offsetValue = new Vector2(_activeCell.Position.x + 1, _activeCell.Position.y);
                break;

            default:
                throw new ArgumentException($"Invalid OffsetDirections: {offsetDirection}");
        }

        return offsetValue;
    }
}
