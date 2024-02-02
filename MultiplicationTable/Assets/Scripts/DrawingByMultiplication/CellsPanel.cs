using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
using System.Linq;
using UnityEditor;
using System.IO;

public class CellsPanel : UIPanel {
    public event Action<Cell> ActiveCellChanged;
    public event Action<int> EmptyCellsCountChanged;

    [SerializeField] private RectTransform _cellsParent;
    [SerializeField] private List<Cell> _emptyCells;

    private List<Cell> _cells;
    private Cell _activeCell;
    private UICompanentsFactory _factory;
    private DrawingData _drawingData;
    private Color32[] _pixels;

    [Inject]
    private void Construct(UICompanentsFactory companentsFactory) {
        _factory = companentsFactory;
    }

    public void Init(DrawingData drawingData, int equationCount) {
        _drawingData = drawingData;

        var pixels = GetPixelsFromPNG(_drawingData.Texture);
        CreateCells(pixels);
        ActivateCells();

        _emptyCells = DisableRandomCells(equationCount);

        _activeCell = _emptyCells[0];
        _activeCell.SetState(CellStates.Active);
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        foreach (var iCell in _cells) {
            iCell.Selected -= OnCellSelected;
        }
    }

    public void FillActiveCell() {
        _activeCell.SetState(CellStates.Fill);
        _emptyCells.Remove(_activeCell);

        SwitchActiveCell();
    }
        
    private void CreateCells(Color32[] pixels) {
        _cells = new List<Cell>();
        float size = Mathf.Sqrt(pixels.Length);
        int pixelIndex = 0;

        for (int row = 0; row < size; row++) {
            for (int column = 0; column < size; column++) {
                CellConfig config = new CellConfig();

                Cell newCell = _factory.Get<Cell>(config, _cellsParent);
                newCell.Init(pixels[pixelIndex], new Vector2(column, row));
                newCell.Selected += OnCellSelected;

                pixelIndex++;

                _cells.Add(newCell);
            }
        }
    }
    
    private List<Cell> DisableRandomCells(int count) {
        List<Cell> emptyCells = new List<Cell>();

        while (emptyCells.Count < count) {
            int randomIndex = UnityEngine.Random.Range(0, _cells.Count);
            Cell randomCell = _cells[randomIndex];

            if (emptyCells.Contains(randomCell) == false) {
                randomCell.SetState(CellStates.Empty);
                emptyCells.Add(randomCell);
            }
        }

        return emptyCells;
    }

    private void ActivateCells() {
        foreach (var iCell in _cells) {
            iCell.SetState(CellStates.Fill);
        }
    }

    private Color32[] GetPixelsFromPNG(Texture2D png) {
        string assetPath = AssetDatabase.GetAssetPath(png);

        Texture2D texture = new Texture2D(2, 2);
        byte[] bytes = File.ReadAllBytes(assetPath);
        texture.LoadImage(bytes);
        
        return texture.GetPixels32();
    }

    private Dictionary<Color, int> GetColorsFromPNG() {
          var colors = new Dictionary<Color, int>();
        for (int i = 0; i < _pixels.Length; i++) {
            if (!colors.ContainsKey(_pixels[i]))
                colors.Add(_pixels[i], 1);
            else
                colors[_pixels[i]]++;
        }

        return colors;
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
    
    private void SwitchActiveCell() {
        if (_emptyCells.Count >= 0) {
            _activeCell = _emptyCells[0];
            _activeCell.SetState(CellStates.Active);
        }

        EmptyCellsCountChanged?.Invoke(_emptyCells.Count);
    }

}
