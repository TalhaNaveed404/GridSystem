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
    private Vector3 screenPoint;
    private Vector3 offset;
    
    private GridCell _targetCell=null;
    private Table _selectedTable;

   
    private void OnEnable()
    { 
     //   StartPosition = this.transform.position;
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
                offset = _selectedTable.transform.position - mousePosition;
                _selectedTable.StartPosition = _selectedTable.gameObject.transform.position;
            }
            else
            {
                Debug.Log("No object found");
            }
            // LayerMask = LayerMask.GetMask("Table");
            // // Cast a ray from the mouse position into the scene
            // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // RaycastHit hit;
            //
            // Debug.Log(LayerMask);
            // // Check if the ray hits any object
            // if (Physics.Raycast(ray, out hit,Mathf.Infinity,LayerMask))
            // {
            //     screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            //     
            //     Debug.Log(LayerMask);
            //     // Perform selection logic when an object is clicked
            //     Debug.Log("Object selected: " + hit.collider.gameObject.name);
            //     _selectedTable = hit.collider.gameObject.GetComponent<Table>();
            //     offset = _selectedTable.transform.position -
            //              Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            //                  screenPoint.z));
            //     _selectedTable.StartPosition = _selectedTable.gameObject.transform.position;
            // }
            // else
            // {
            //     Debug.Log("NoObjectFound");
            //         
            // }
        }

        if (Input.GetMouseButton(0))
        {
            if (_selectedTable!=null&&!_selectedTable.Placed)
            {
                
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _selectedTable.transform.position = mousePosition + offset;
                // Update the object's position based on the mouse position
                // Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                // Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
                // _selectedTable.transform.position = new Vector3(curPosition.x, 0.5f, curPosition.z);
               // CastRay();

               NewRayCast();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_selectedTable != null)
                NewMouseButtonUp();
            //OnMouseButtonUp();
        }
    }

    private void NewMouseButtonUp()
    {
        Vector2 bottomPosition = _selectedTable.transform.position;
        LayerMask layerMask = LayerMask.GetMask("TileLayer");
        RaycastHit2D hit = Physics2D.Raycast(-bottomPosition, Vector2.zero, Mathf.Infinity, layerMask);

      

        if (hit.collider!=null)
        {
            Debug.Log("TargetCell"+_targetCell);
            if (_targetCell != null && _targetCell.GetComponent<GridCell>() != null && !_targetCell.IsPlaceableCell)
            {
                Debug.Log("TargetCell"+_targetCell);
                Debug.Log("CellDeselect");
                _targetCell.GetComponent<GridCell>().CellDeSelectedState();
            }
            _targetCell = hit.collider.gameObject.GetComponent<GridCell>();
            if (_targetCell != null && _targetCell.GetComponent<GridCell>() != null && !_targetCell.IsPlaceableCell)
            {
                Debug.Log("StackPlaced");
                StackPlaced();
            }
            else
            {
                Debug.Log("BackToStart");
                _selectedTable.transform.position = _selectedTable.StartPosition;
            }

          
            Debug.Log("Hit: " + hit.collider.gameObject.name);
            Debug.DrawRay(_selectedTable.transform.position, -_selectedTable.transform.up * raycastDistance, Color.green);
        }
        else
        {
            Debug.Log("No TIle Found");
            _selectedTable.transform.position = _selectedTable.StartPosition;
        }

        _selectedTable = null;

    }
    
   private void OnMouseButtonUp()
   {
       Vector3 bottomPosition = _selectedTable.transform.position;
       RaycastHit hit;
       
   //    LayerMask = LayerMask.NameToLayer("TileLayer");
       if (Physics.Raycast(bottomPosition, -_selectedTable.transform.up, out hit, raycastDistance,LayerMask))
       {
           if (_targetCell!= null&&_targetCell.GetComponent<GridCell>()!=null&&_targetCell&&!_targetCell.IsPlaceableCell)
           {
               _targetCell.GetComponent<GridCell>().CellDeSelectedState();
           }
           _targetCell = hit.collider.gameObject.GetComponent<GridCell>();
           if (_targetCell != null && _targetCell.GetComponent<GridCell>() != null&&!_targetCell.IsPlaceableCell)
           {
               StackPlaced();
           }
           else
           {
               _selectedTable.transform.position = _selectedTable.StartPosition;
           }// If the ray hits something, do something (e.g., log the hit object's name)
   
           Debug.Log("Hit: " + hit.collider.gameObject.name); 
           Debug.DrawRay(_selectedTable.transform.position, -_selectedTable.transform.up*raycastDistance, Color.green);
       }
      
       else
       {
           _selectedTable.transform.position = _selectedTable.StartPosition;
       }

       _selectedTable = null;
   }

   private void StackPlaced()
   {
       _targetCell.GetComponent<GridCell>().CellDeSelectedState();
       _selectedTable.transform.SetParent(_targetCell.transform);
       _selectedTable.transform.position = _targetCell.transform.position;
       _targetCell.IsPlaceableCell = true;
       _selectedTable.Placed = true;
      
   }


 
   
   void CastRay()
   {
//        // Calculate the position of the bottom of the object in world space
//        Vector3 bottomPosition = _selectedTable.gameObject.transform.position;// - transform.up * (GetComponent<Renderer>().bounds.extents.y);
//       
//         LayerMask = LayerMask.GetMask("TileLayer");
//        // Cast a ray from the bottom position downwards
//        RaycastHit hit;
//        if (Physics.Raycast(bottomPosition, -_selectedTable.transform.up, out hit, raycastDistance,LayerMask))
//        {
//            if (_targetCell!= null&&_targetCell.GetComponent<GridCell>()!=null)
//            {
//                _targetCell.GetComponent<GridCell>().CellNormalState();
//            }
//            _targetCell = hit.collider.gameObject.GetComponent<GridCell>();
//           if(_targetCell!= null&&_targetCell.GetComponent<GridCell>()!=null)
//               _targetCell.GetComponent<GridCell>().CellSelectedState();
//            // If the ray hits something, do something (e.g., log the hit object's name)
// //           Debug.Log("Hit: " + hit.collider.gameObject.name);
//    //        Debug.DrawRay(transform.position, -transform.up*raycastDistance, Color.green);
//        }
//        else if(_targetCell!=null&&_targetCell.GetComponent<GridCell>()!=null)
//        {
//            _targetCell.GetComponent<GridCell>().CellNormalState();
//            return;
//        }
//        else
//        {
//            _targetCell = null;
//            return;
//        }
       
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
