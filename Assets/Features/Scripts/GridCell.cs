using System.Collections.Generic;
using GridSystem.GridConfig;
using UnityEngine;
using UnityEngine.Serialization;

namespace GridSystem
{
    public class GridCell:MonoBehaviour
    {
        [FormerlySerializedAs("GridTileHelper")] [FormerlySerializedAs("TileHelper")] [SerializeField] private GridCellHelper gridCellHelper;

        [SerializeField] private Vector2Int Coordinates;

        [SerializeField] public bool IsPlaceableCell;
        [SerializeField] public TileTypeEnum TileTypeEnum;
        [SerializeField] public List<GridCell> neighbours= new List<GridCell>();
        
        
        public void InitCell(Vector2Int Coordinates,TileTypeEnum tileTypeEnum,Sprite sprite)
        {
            this.Coordinates = Coordinates;
            var coords = $"{this.Coordinates.x},{this.Coordinates.y}";
            gridCellHelper.SetCoordinatesText(coords);
            gridCellHelper.SetSprite(sprite);
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
            gridCellHelper.SetMaterialColor(GridTileState.selected);
        }

        public void CellNeighbourState()
        {
            gridCellHelper.SetMaterialColor(GridTileState.neighbour);
        }

        public void CellNormalState()
        {
            gridCellHelper.SetMaterialColor(GridTileState.normal);
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
            gridCellHelper.SetMaterialColor(GridTileState.placement);
        }
        public void HighLightNeighbourPlacement()
        {
            gridCellHelper.SetMaterialColor(GridTileState.neighbourPlacmement);
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