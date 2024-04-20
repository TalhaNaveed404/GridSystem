using System.Collections.Generic;
using GridSystem.GridConfig;
using UnityEngine;
using UnityEngine.Serialization;

namespace GridSystem.GridFields
{
    public class GridCell:MonoBehaviour
    {
        [FormerlySerializedAs("gridCellHelper")] [SerializeField] private GridCellView gridCellView;

        [SerializeField] private Vector2Int Coordinates;

        [SerializeField] public bool IsPlaceableCell;
       
        [SerializeField] public List<GridCell> neighbours= new List<GridCell>();
        
        
        public void InitCell(Vector2Int coordinates,TileTypeEnum tileTypeEnum,Sprite sprite)
        {
            this.Coordinates = coordinates;
            var coords = $"{this.Coordinates.x},{this.Coordinates.y}";
            gridCellView.SetCoordinatesText(coords);
            gridCellView.SetSprite(sprite);
            this.name = $"CELL_{coords}";
            if (tileTypeEnum == TileTypeEnum.Wood)
                IsPlaceableCell = true;
            else
            {
                IsPlaceableCell = false;
            }
        }
    
    

        public Vector2Int GetCoordinate()
        {
            return Coordinates;
        }
        public void CellSelectedState()
        {
            gridCellView.SetMaterialColor(GridTileState.selected);
        }

        public void CellNeighbourState()
        {
            gridCellView.SetMaterialColor(GridTileState.neighbour);
        }

        public void CellNormalState()
        {
            gridCellView.SetMaterialColor(GridTileState.normal);
        }

        

        public void SetNeighbourList(List<GridCell> neighbouringTiles)
        {
            neighbours.AddRange( neighbouringTiles);
        }
        
        public void CellSetectedState()
        {
            this.CellSelectedState();
            foreach (var VARIABLE in neighbours)
            {
                VARIABLE.CellNeighbourState();
            }
        }

        public void CellDeSelectedState()
        {
            this.CellNormalState();
            foreach (var VARIABLE in neighbours)
            {
                VARIABLE.CellNormalState();
            }
        }

        public void HighLightPossiblePlacement()
        {
            gridCellView.SetMaterialColor(GridTileState.placement);
        }
        public void HighLightNeighbourPlacement()
        {
            gridCellView.SetMaterialColor(GridTileState.neighbourPlacmement);
        }
        
        
        public void ShowPossiblePlacement()
        {
            HighLightPossiblePlacement();
            foreach (var VARIABLE in neighbours)
            {
                if (VARIABLE.IsPlaceableCell)
                    VARIABLE.HighLightNeighbourPlacement();
            }
            
        }

    }
}