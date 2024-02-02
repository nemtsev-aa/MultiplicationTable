using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
using System.Linq;
using UnityEditor;
using System.IO;
using DG.Tweening;

public class CellsPanel : UIPanel {
    public event Action<Cell> ActiveCellChanged;
    public event Action<int> EmptyCellsCountChanged;

    [SerializeField] private RectTransform _cellsParent;
    [SerializeField] private List<Cell> _emptyCells;

    private List<Cell> _cells;
    
    private UICompanentsFactory _factory;
    private DrawingData _drawingData;
    private Color32[] _pixels;

    public Cell ActiveCell { get; private set; }

    [Inject]
    private void Construct(UICompanentsFactory companentsFactory) {
        _factory = companentsFactory;
    }

    public void Init(DrawingData drawingData, int equationCount) {
        _drawingData = drawingData;

        var pixels = GetPixelsFromPNG(_drawingData.Texture);
        CreateCells(pixels);
        FillAllCells();

        _emptyCells = GetRandomCells(equationCount);
        DisableRandonCells();

        OnCellSelected(_emptyCells[0]);
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        foreach (var iCell in _cells) {
            iCell.Selected -= OnCellSelected;
        }
    }

    public void FillActiveCell() {
        ActiveCell.SwitchState(CellStates.Fill);
        
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
    
    private List<Cell> GetRandomCells(int count) {
        List<Cell> emptyCells = new List<Cell>();

        while (emptyCells.Count < count) {
            int randomIndex = UnityEngine.Random.Range(0, _cells.Count);
            Cell randomCell = _cells[randomIndex];

            if (randomCell.FillStateColor == Color.white)
                continue;

            if (emptyCells.Contains(randomCell) == false) {
                emptyCells.Add(randomCell);
            }
        }

        return emptyCells;
    }

    private void DisableRandonCells() {
        for (int i = 0; i < _emptyCells.Count; i++) {
            _emptyCells[i].SwitchState(CellStates.Empty);
        }
    }

    [ContextMenu(nameof(FillAllCells))]
    private void FillAllCells() {
        float timeDuration = 0.01f;

        for (int i = 0; i < _cells.Count; i++) {
            Cell iCell = _cells[i];
            ShowFillAnimation(iCell, timeDuration * i);
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
        if (activeCell.Equals(ActiveCell) == true) 
            return;

        if (ActiveCell != null && ActiveCell.CurrentState != CellStates.Fill)
            ActiveCell.SwitchState(CellStates.Empty);

        ActiveCell = activeCell;
        ActiveCell.SwitchState(CellStates.Active);
        ActiveCellChanged?.Invoke(ActiveCell);
    }

    private void SwitchActiveCell() {
        _emptyCells.Remove(ActiveCell);

        if (_emptyCells.Count > 0) 
            OnCellSelected(_emptyCells[0]);
        
        EmptyCellsCountChanged?.Invoke(_emptyCells.Count);
    }

    private void SwitchActiveCell(OffsetDirections offsetDirection) {
        float row = ActiveCell.Position.y;
        float column = ActiveCell.Position.x; 

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
        ActiveCell = columnCells.First(cell => cell.Position.y == row);

    }

    private void ShowFillAnimation(Cell cell, float timeOffset) {
        float duration = 0.3f;
        float fadeValue = Mathf.PerlinNoise(cell.Position.x, cell.Position.y);

        var s = DOTween.Sequence();
        
        s.Append(cell.Background.DOFade(fadeValue, duration));
        s.Insert(timeOffset, cell.Background.DOFade(1f, duration));
    }
}
