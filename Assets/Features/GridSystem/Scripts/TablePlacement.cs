using UnityEngine;
using GridSystem.PlacementObjects;
using GridSystem.GridFields;
public class TablePlacement 
{

     private Table _selectedTable;
     private GridCell _selectedGridCell;
     private Vector2Int _selectedCellCoordinate;
    
    public void PlaceTable(Table table,GridCell gridCell)
    {
        _selectedTable = table;
        _selectedGridCell = gridCell;
        SelectedPlacementCell();

    }

   
    void SelectedPlacementCell()
    {
        GridCell neighbourCell;
        if (_selectedTable.IsHorizontal)
        {
            _selectedCellCoordinate=_selectedGridCell.GetCoordinate();
            neighbourCell=FindPlaceableNeighbour(new Vector2Int(1, 0));
        }
        else
        {
            _selectedCellCoordinate=_selectedGridCell.GetCoordinate();
            neighbourCell=FindPlaceableNeighbour(new Vector2Int(0, 1));
        }
        
        if (neighbourCell != null)
        {
            _selectedGridCell.IsPlaceableCell = false;
            neighbourCell.IsPlaceableCell = false;
            SuccessFulPlacement();

        }
        else
        {
            _selectedTable.transform.position = _selectedTable.StartPosition;
            _selectedGridCell.CellDeSelectedState();
            Debug.Log("No cell detected");    
        }

    }
    
    GridCell FindPlaceableNeighbour(Vector2Int neighbourCoordinate)
    {
        foreach (GridCell neighbour in _selectedGridCell.neighbours)
        {
            if (neighbour.GetCoordinate() == neighbourCoordinate+_selectedCellCoordinate&&neighbour.IsPlaceableCell)
            {
                return neighbour;
            }
        }

        return null;
    }

    void SuccessFulPlacement()
    {
        _selectedGridCell.GetComponent<GridCell>().CellDeSelectedState();
        _selectedTable.transform.position = _selectedGridCell.transform.position;
        _selectedGridCell.IsPlaceableCell = false;
        _selectedTable.Placed = true;
        _selectedTable = null;
    }
    
    
    
    
}
