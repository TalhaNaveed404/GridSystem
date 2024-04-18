using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GridSystem
{
    public class GridCell:MonoBehaviour
    {
        [FormerlySerializedAs("GridTileHelper")] [FormerlySerializedAs("TileHelper")] [SerializeField] private GridCellHelper gridCellHelper;

        [SerializeField] private Vector2Int Coordinates;

        [SerializeField] public bool IsOccupied;
        
        [SerializeField] public List<GridCell> neighbours= new List<GridCell>();
        
        
        public void InitCell(Vector2Int Coordinates,Sprite sprite)
        {
            this.Coordinates = Coordinates;
        
            //Helper
            var coords = $"{this.Coordinates.x},{this.Coordinates.y}";
            gridCellHelper.SetCoordinatesText(coords);
            gridCellHelper.SetSprite(sprite);
            this.name = $"CELL_{coords}";
        }
    
    

        public Vector2Int GetCoordinate()
        {
            return Coordinates;
        }
        public void SelectedState()
        {
            gridCellHelper.SetMaterialColor(GridTileState.selected);
        }

        public void NeighbourState()
        {
            gridCellHelper.SetMaterialColor(GridTileState.neighbour);
        }

        public void NormalState()
        {
            gridCellHelper.SetMaterialColor(GridTileState.normal);
        }

        

        public void SetNeighbourList(List<GridCell> neighbouringTiles)
        {
            neighbours.AddRange( neighbouringTiles);
     
        }
        


    }
}