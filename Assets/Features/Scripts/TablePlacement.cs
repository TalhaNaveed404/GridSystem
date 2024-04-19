using System.Collections;
using System.Collections.Generic;
using GridSystem;
using UnityEngine;
using UnityEngine.Rendering;

public class TablePlacement : MonoBehaviour
{

    [SerializeField] private Table SelectedTable;
    [SerializeField] private GridCell SelectedGridCell;


    public void PlaceTable(Table table,GridCell gridCell)
    {
        SelectedTable = table;
        SelectedGridCell = gridCell;

        
        
    }

    void SelectedPlacementCell()
    {
        foreach (GridCell gridCell in SelectedGridCell.neighbours)
        {
            if (gridCell.IsPlaceableCell)
            {
                
            }
        }
        
    }
}
