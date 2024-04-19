using System.Collections;
using System.Collections.Generic;
using GridSystem;
using UnityEngine;

public class InputManager : MonoBehaviour
{
   
  // [FormerlySerializedAs("HexaGridHolder")] [FormerlySerializedAs("_hexaGridHolder")] public MergeMechanics mergeMechanics;
    //Input Related Stuff
    public float raycastDistance = 1f;
    [SerializeField] private LayerMask LayerMask;
    [SerializeField] float cellSize=1;
    private Vector3 screenPoint;
    private Vector3 offset;
    
    private GridCell _targetCell=null;
    private Table _selectedTable;

   
    private void OnEnable()
    { 
        InputOnEnable();
    }
    
    private void InputOnEnable()
    {
        LayerMask = LayerMask.GetMask("Table");
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Table"));
            if (hit.collider != null)
            {
                // Object with "Table" layer was hit
                Debug.Log("Object selected: " + hit.collider.gameObject.name);
                _selectedTable = hit.collider.gameObject.GetComponent<Table>();
                if (!_selectedTable.Placed)
                {
                    offset = _selectedTable.transform.position - mousePosition;
                    _selectedTable.StartPosition = _selectedTable.gameObject.transform.position;
                }
            }
            // else if(_selectedTable!=null)
            // {
            //     _selectedTable.transform.position = _selectedTable.StartPosition;
            //     Debug.Log("No object found");
            // }
           
        }

        if (Input.GetMouseButton(0))
        {
            if (_selectedTable!=null&&!_selectedTable.Placed)
            {
                Debug.Log("DraggingObject");
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _selectedTable.transform.position = SnapToGrid(mousePosition+offset);
                NewRayCast();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_selectedTable != null&&!_selectedTable.Placed)
                TileDetection();
        }
    }
    Vector3 SnapToGrid(Vector3 position)
    {
        float x = Mathf.Round(position.x / cellSize) * cellSize;
        float y = Mathf.Round(position.y / cellSize) * cellSize;
        float z = position.z; // Maintain initial z position
        return new Vector3(x, y, z);
    }


   private void TablePlaced()
   {
       _targetCell.GetComponent<GridCell>().CellDeSelectedState();
       _selectedTable.transform.SetParent(_targetCell.transform);
       _selectedTable.transform.position = _targetCell.transform.position;
       _targetCell.IsPlaceableCell = false;
       _selectedTable.Placed = true;
       _selectedTable = null;

   }



   void TileDetection()
   {
       Debug.Log("Selected Table"+_selectedTable);
       if(_targetCell!=null&&_targetCell.IsPlaceableCell)
       {
           Debug.Log("Placing Table");
           TablePlaced();
           
       }
       else 
       {
           _selectedTable.transform.position = _selectedTable.StartPosition;
           _targetCell.CellDeSelectedState();
           
                Debug.Log("No cell detected");    
           
       }
       Debug.Log("targetCell"+_targetCell);
       Debug.Log("targetCell"+_targetCell.IsPlaceableCell);
       _selectedTable = null;
       _targetCell = null;
   }


   void NewRayCast()
   {
       Vector3 bottomPosition = _selectedTable.gameObject.transform.position;

       LayerMask mask = LayerMask.GetMask("TileLayer");
       RaycastHit2D hit = Physics2D.Raycast(bottomPosition, -Vector2.up, raycastDistance, mask);

       if (hit.collider != null)
       {
           Debug.Log("TargetCell"+_targetCell);
           if (_targetCell != null && _targetCell.GetComponent<GridCell>() != null)
           {
               _targetCell.GetComponent<GridCell>().CellDeSelectedState();
           }
    
           _targetCell = hit.collider.gameObject.GetComponent<GridCell>();
    
           if (_targetCell != null && _targetCell.GetComponent<GridCell>() != null)
           {
               _targetCell.GetComponent<GridCell>().CellSetectedState();
           }
       }
       else if (_targetCell != null && _targetCell.GetComponent<GridCell>() != null)
       {
           _targetCell.GetComponent<GridCell>().CellDeSelectedState();
       }
       else
       {
           _targetCell = null;
       }
   }
}
