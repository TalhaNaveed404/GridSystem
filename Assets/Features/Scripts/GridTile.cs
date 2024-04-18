using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GridSystem.GridTile
{
    public class GridTile:MonoBehaviour
    {
        [SerializeField] private TileHelper TileHelper;

        [SerializeField] private Vector2Int Coordinates;

        [SerializeField] public bool IsOccupied;
        
        [SerializeField] public List<GridTile> neighbours= new List<GridTile>();
        
        
        public void InitCell(Vector2Int Coordinates)
        {
            this.Coordinates = Coordinates;
        
            //Helper
            var coords = $"{this.Coordinates.x},{this.Coordinates.y}";
            TileHelper.SetCoordinatesText(coords);
            this.name = $"CELL_{coords}";
        }
    
    

        public Vector2Int GetCoordinate()
        {
            return Coordinates;
        }
        public void SelectedState()
        {
            TileHelper.SetMaterialColor(GridTileState.selected);
        }

        public void NeighbourState()
        {
            TileHelper.SetMaterialColor(GridTileState.neighbour);
        }

        public void NormalState()
        {
            TileHelper.SetMaterialColor(GridTileState.normal);
        }

        

        public void SetNeighbourList(List<GridTile> neighbouringTiles)
        {
            neighbours.AddRange( neighbouringTiles);
     
        }
        


    }
}