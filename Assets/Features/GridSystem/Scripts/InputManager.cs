using UnityEngine;
using GridSystem.PlacementObjects;
using GridSystem.GridFields;
public class InputManager : MonoBehaviour
{
    
    //Input Related Stuff
    public float RaycastDistance = 1f;
    [SerializeField] private LayerMask LayerMask;
    [SerializeField] float CellSize=1;
    [SerializeField] private Vector3 _offset;
    
    private Vector3 _screenPoint;
    private GridCell _targetCell=null;
    private Table _selectedTable;
    private TablePlacement _tablePlacement= new TablePlacement();
   

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
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
               Debug.Log("Space Pressed");
            if (_selectedTable != null && !_selectedTable.Placed)
            {
                _selectedTable.SetTableRotation();
            }
        }
        //To Detect the mouse Click
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("buttonPressed");
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Table"));
            if (hit.collider != null)
            {
                // Object with "Table" layer was hit
                Debug.Log("Object selected: " + hit.collider.gameObject.name);
                _selectedTable = hit.collider.gameObject.GetComponent<Table>();
                if (!_selectedTable.Placed)
                {
                    _offset = _selectedTable.transform.position - mousePosition;
                    _selectedTable.StartPosition = _selectedTable.gameObject.transform.position;
                }
            }
     
           
        }
        //For mouse Dragging
        if (Input.GetMouseButton(0))
        {
            
            if (_selectedTable!=null&&!_selectedTable.Placed)
            {
                Debug.Log("DraggingObject");
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
                _selectedTable.transform.position = SnapToGridCell(mousePosition+_offset);
                CellDetectionRayCast();
            }
        }
        //When mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            if (_selectedTable != null&&!_selectedTable.Placed)
                TileDetection();
        }
    }
    
    void CellDetectionRayCast()
    {
        Vector3 bottomPosition = _selectedTable.gameObject.transform.position;
        LayerMask mask = LayerMask.GetMask("TileLayer");
        RaycastHit2D hit = Physics2D.Raycast(bottomPosition, -Vector2.up, RaycastDistance, mask);
        
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
                if (_targetCell.IsPlaceableCell)
                {
                    _targetCell.CellDeSelectedState();
                    _targetCell.ShowPossiblePlacement();
                }
                else
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
    //GridSnapping
    Vector3 SnapToGridCell(Vector3 position)
    {
        float x = Mathf.Round(position.x / CellSize) * CellSize;
        float y = Mathf.Round(position.y / CellSize) * CellSize;
        float z = position.z; // Maintain initial z position
        return new Vector3(x, y, z);
    }
    
    void TileDetection()
   {
       Debug.Log("Selected Table"+_selectedTable);
       if(_targetCell!=null&&_targetCell.IsPlaceableCell)
       {
           Debug.Log("Placing Table");
          // TablePlacement();
          _tablePlacement.PlaceTable(_selectedTable,_targetCell);
           
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

   
  
}
