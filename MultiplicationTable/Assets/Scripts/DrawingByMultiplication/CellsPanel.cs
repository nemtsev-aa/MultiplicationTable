using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
using System.Linq;

public class CellsPanel : UIPanel {
    public event Action<Cell> ActiveCellChanged;

    [SerializeField] private RectTransform _cellsParent;

    private List<Cell> _cells;
    private Cell _activeCell;
    private UICompanentsFactory _factory;
    private QuestionsConfig _question;

    [Inject]
    private void Construct(UICompanentsFactory companentsFactory, QuestionsConfig question) {
        _factory = companentsFactory;
        _question = question;
    }

    public void Init() {
        CreateCells();

        FillingCells();

        //_activeCell = _cells.FirstOrDefault(cell => cell.Position == Vector2.one);
        //_activeCell.SetState(CellStates.Active);
    }

    private void FillingCells() {
        foreach (var iEquation in _question.Equations) {
            var x = iEquation.Multipliable;
            var y = iEquation.Multiplier;

            Cell cell = _cells.Find(cell => cell.Position.x == x && cell.Position.y == y);
            cell.SetState(CellStates.Fill, iEquation.BaseColor);
        }
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        foreach (var iCell in _cells) {
            iCell.Selected -= OnCellSelected;
        }
    }

    public void GetActiveCell(OffsetDirections direction) {
        _activeCell.SetState(CellStates.Empty);

        SwitchActiveCell(direction);
        _activeCell.SetState(CellStates.Active);
    }

    public void FillActiveCell(Color color) {
        _activeCell.SetState(CellStates.Fill, color);
    }

    private void CreateCells() {
        _cells = new List<Cell>();

        for (int row = 0; row < 10; row++) {
            for (int column = 0; column < 10; column++) {
                CellConfig config = new CellConfig();

                Cell newCell = _factory.Get<Cell>(config, _cellsParent);
                newCell.Position = new Vector2(column, row);
                newCell.Selected += OnCellSelected;

                if (row == 0 && column > 0)
                    newCell.TextValue = $"{column}";
                else if (row > 0 && column == 0)
                    newCell.TextValue = $"{row}";
                else if (row > 0 && column > 0) {
                    newCell.SetState(CellStates.Empty);
                }
                    
                _cells.Add(newCell);
            }
        }
    }

    private void OnCellSelected(Cell activeCell) {
        if (activeCell.Position.x == 0 || activeCell.Position.y == 0) 
            return;
        
        _activeCell.SetState(CellStates.Empty);

        _activeCell = activeCell;
        _activeCell.SetState(CellStates.Active);

        ActiveCellChanged?.Invoke(_activeCell);
    }

    private void SwitchActiveCell(OffsetDirections offsetDirection) {
        float row = _activeCell.Position.y;
        float column = _activeCell.Position.x; 

        switch (offsetDirection) {
            case OffsetDirections.Left:
                if (column == 1f)
                    return;

                column -= 1;
                break;

            case OffsetDirections.Up:
                if (row == 9f)
                    return;

                row += 1;
                break;

            case OffsetDirections.Down:
                if (row == 1f)
                    return;

                row -= 1;
                break;

            case OffsetDirections.Right:
                if (column == 9f)
                    return;

                column += 1;
                break;

            default:
                throw new ArgumentException($"Invalid OffsetDirections: {offsetDirection}");
        }

        var columnCells = _cells.Where(cell => cell.Position.x == column);
        _activeCell = columnCells.First(cell => cell.Position.y == row);

    }
    
    private EquationData GetQuestionData(int multipliable, int multiplier) {
        var multipliables = _question.Equations.Where(data => data.Multipliable == multipliable);

        if (multipliables.Count() > 0) {
            EquationData data = multipliables.First(data => data.Multiplier == multiplier);
            
            if (data != null)
                return data;
        }

        return null;

    }
}
