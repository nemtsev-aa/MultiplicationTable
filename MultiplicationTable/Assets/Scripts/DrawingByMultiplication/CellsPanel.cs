using DG.Tweening;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Zenject;

public class CellsPanel : UIPanel {
    private const string LargeConfig = "LargeConfig";
    private const string ConfigsPath = "EnemyConfigs";

    public event Action<Cell> ActiveCellChanged;
 
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

        var pixels = GetPixelsFromPNG();
        CreateCells(pixels);
        FillAllCells();

        _emptyCells = GetRandomCells(equationCount);
        DisableRandomCells();

        OnCellSelected(_emptyCells[0]);
    }

    public override void Reset() {
        base.Reset();

        _drawingData = null;

        foreach (var iCell in _cells) {
            iCell.Selected -= OnCellSelected;
            Destroy(iCell.gameObject);
        }

        _cells.Clear();
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        foreach (var iCell in _cells) {
            iCell.Selected -= OnCellSelected;
        }
    }

    public void FillActiveCell(float delaySwitchingEquation = 0f) {
        ActiveCell.SwitchState(CellStates.Fill);

        Invoke(nameof(ChangeEmptyCellsCount), delaySwitchingEquation);
    }

    private void ChangeEmptyCellsCount() {
        _emptyCells.Remove(ActiveCell);

        if (_emptyCells.Count > 0)
            OnCellSelected(_emptyCells[0]);
    }

    private void CreateCells(Color32[] pixels) {
        _cells = new List<Cell>();
        float size = Mathf.Sqrt(pixels.Length);
        int pixelIndex = 0;

        Logger.Instance.Log($"CreateCells size value:  {size}");

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

    private void DisableRandomCells() {
        for (int i = 0; i < _emptyCells.Count; i++) {
            _emptyCells[i].SwitchState(CellStates.Empty);
        }
    }

    private void FillAllCells() {
        float timeDuration = 0.01f;

        for (int i = 0; i < _cells.Count; i++) {
            Cell iCell = _cells[i];
            ShowFillAnimation(iCell, timeDuration * i);
        }
    }

    private Color32[] GetPixelsFromPNG() {
        ResourcesExtension resourcesExtension = new ResourcesExtension(ResourceType.AnimalTextures);

        try {
            if (SystemInfo.deviceType == DeviceType.Handheld) {
                Logger.Instance.Log($"GetPixelsFromPNG: Colors Count {resourcesExtension.Test(this).Length}");
                return resourcesExtension.Test(this);
            }
            else
            {
                var randomItemPath = resourcesExtension.GetRandomItemPathes();
                Logger.Instance.Log($"GetPixelsFromPNG: randomItemPath {randomItemPath}");

                byte[] bytes = File.ReadAllBytes(randomItemPath);
                Logger.Instance.Log($"GetPixelsFromPNG: bytes {bytes.Length}");

                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(bytes);

                Logger.Instance.Log($"GetPixelsFromPNG: Pixels32 Count {texture.GetPixels32().Length}");
                return texture.GetPixels32();
            }
        }
        catch (Exception ex) {
            Logger.Instance.Log($"GetPixelsFromPNG: {ex.Message}");
            throw new ArgumentException($"{ex.Message}");
        }
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
