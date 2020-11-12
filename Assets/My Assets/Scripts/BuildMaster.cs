using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMaster : MonoBehaviour
{
    [SerializeField]
    private GameObject[] buildingPrefabs;
    [SerializeField]
    private GameObject[] previewPrefabs;
    [SerializeField]
    private GameObject highlightTile;
    [SerializeField]
    private int tileHighlightRadius;

    private bool isSelected;
    private bool isPreviewBuilt;
    private Vector3 mousePos;
    private Vector3 cellCenter;
    private Vector3Int mouseCell;
    private Vector3Int latestCellToBuildPreview;
    private Grid grid;
    private GameObject toBuild;
    private int indexSelected;

    private GameObject latestPreviewBuilt;
    private GameObject[,] highlightTilesBuilt;
    private List<Vector3Int> occupiedCells;

    RectTransform panel;

    public bool IsSelected => isSelected;

    private void Start()
    {
        panel = gameObject.transform.Find("/Canvas/BottomPanel").GetComponent<RectTransform>();
        highlightTilesBuilt = new GameObject[tileHighlightRadius * 2 + 1, tileHighlightRadius * 2 + 1];
        latestCellToBuildPreview = new Vector3Int();
        occupiedCells = new List<Vector3Int>();
        isPreviewBuilt = false;
        latestPreviewBuilt = null;
        isSelected = false;
        grid = GameObject.FindObjectOfType<Grid>();
    }
    private void Update()
    {
        if (!isSelected) return;
        if (!OnActiveScreen())
        {
            DestroyPreview();
            return;
        }

        CalculatePosition();

        if (mouseCell != latestCellToBuildPreview || !isPreviewBuilt)
        {
            if (latestPreviewBuilt != null)
            {
                DestroyPreview();
            }
            if (indexSelected < previewPrefabs.Length)
            {
                BuildPreview();
                BuildHighlightTiles();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Build();
        }
    }

    private bool OnActiveScreen()
    {
        return Input.mousePosition.y > panel.sizeDelta.y;
    }

    private void BuildHighlightTiles()
    {
        for (int i = 0; i < tileHighlightRadius * 2 + 1; ++i)
        {
            for (int j = 0; j < tileHighlightRadius * 2 + 1; ++j)
            {
                highlightTilesBuilt[i,j] = Instantiate(highlightTile, MoveFromMuseCell(i - tileHighlightRadius, j - tileHighlightRadius), Quaternion.identity, GameObject.Find("/Grid/Tilemap").transform);
            }
        }
    }

    private Vector3 MoveFromMuseCell(int xtimes, int ytimes)
    {
        return cellCenter + new Vector3(grid.cellSize.x * xtimes, grid.cellSize.y * ytimes, 0);
    }

    private void DestroyPreview()
    {
        if (latestPreviewBuilt != null)
            Destroy(latestPreviewBuilt);
        DestroyHighlightTiles();
    }

    private void DestroyHighlightTiles()
    {
        for (int i = 0; i < tileHighlightRadius * 2 + 1; ++i)
        {
            for (int j = 0; j < tileHighlightRadius * 2 + 1; ++j)
            {
                if (highlightTilesBuilt[i, j] != null)
                    Destroy(highlightTilesBuilt[i, j]);
            }
        }
    }

    private void BuildPreview()
    {
        GameObject preview = toBuild;
        latestPreviewBuilt = Instantiate(previewPrefabs[indexSelected], cellCenter, Quaternion.identity, GameObject.Find("/Grid/Tilemap").transform);
        latestCellToBuildPreview = mouseCell;
        isPreviewBuilt = true;
    }

    private void Build()
    {
        if (!BuildingIsAllowed())
            return;
        occupiedCells.Add(mouseCell);
        DestroyPreview();
        (Instantiate(toBuild.transform, cellCenter, Quaternion.identity, GameObject.Find("/Grid/Tilemap").transform)).name = "BuildMaster_cell_" + mouseCell.ToString();
        Deselect();
    }
    private void CalculatePosition()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseCell = grid.WorldToCell(mousePos);
        cellCenter = grid.CellToWorld(mouseCell) + new Vector3(grid.cellSize.x, grid.cellSize.y) * 0.5f;
    }

    private bool BuildingIsAllowed()
    {
        if (occupiedCells.Contains(mouseCell)) return false;
        return true;
    }
    public void Select(GameObject toSelect)
    {
        if (!IsSelectionAllowed()) return;
        SelectionMaster.OccupySelection("BuildingMaster");
        toBuild = toSelect;
        isSelected = true;
    }

    private bool IsSelectionAllowed()
    {
        return SelectionMaster.IsSelectionBusy == false;
    }

    public void SelectByInd(int ind)
    {
        indexSelected = ind;
        Select(buildingPrefabs[ind]);
    }

    public void Deselect()
    {
        SelectionMaster.ReleaseSelection();
        DestroyPreview();
        toBuild = null;
        isSelected = false;
    }
}