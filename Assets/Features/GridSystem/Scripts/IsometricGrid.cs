using System.Collections.Generic;
using GridSystem.GridConfig;
using ProjectCore.DataLoading;
using Sirenix.OdinInspector;
using UnityEngine;
using GridSystem.GridFields;

namespace GridSystem.Grid
{  
    
    [CreateAssetMenu(fileName = "IsometricGrid",menuName = "GridSystem/IsometricGrid")]
    public class IsometricGrid:ScriptableObject
    {
        public GridCell[,] GridArray;
        
        [SerializeField] private int Rows;
        [SerializeField] private int Col;
        [SerializeField] private IsometricGridConfig IsometricGridConfig;
        
        private List<GridCell> _neighbourList = new List<GridCell>();
        private int[,] _dataArray ;
        private string _dataFile;
        private IsometricGridData _isometricGridData;
        private TerrainGridData _terrainGridData;
        private GameObject _grid;
        
        #region GridGeneration
       public IsometricGrid(GameObject grid,string dataLayout,string datafile,IsometricGridConfig isometricGridConfig)
       {

           this._grid = grid;
           _isometricGridData=Resources.Load<IsometricGridData>(dataLayout); 
           _terrainGridData=_isometricGridData.LoadData(datafile);
           Rows= _terrainGridData.TerrainGrid.Count;
           Col = _terrainGridData.TerrainGrid[0].Count;
           this.IsometricGridConfig = isometricGridConfig;


           _dataArray = new int[Rows, Col];
           GridArray = new GridCell[Rows, Col];
           for (int i = 0; i < Rows; i++)
           {
               List<TileData> innerList = _terrainGridData.TerrainGrid[i];
               for (int j = 0; j < Col; j++)
               {
                   _dataArray[i, j] = innerList[j].TileType;
               }
           }
           
           for (int i = 0; i < Rows; i++)
           {
               string tiledata = "";
               for (int j = 0; j < Col; j++)
               {
                   TileTypeEnum tileTypeEnum= (TileTypeEnum)_dataArray[i, j];
                   GameObject tileObject = Instantiate(isometricGridConfig.GridCellObject);
                   if(tileObject!=null)
                   GenerateTile(tileObject,new Vector3Int(i,j,0),tileTypeEnum);
                   GridArray[i, j] = tileObject.GetComponent<GridCell>(); 
                   
               }
               Debug.Log(tiledata);
            
           }

           CalculateNeighbours();


       }
 
        Sprite TileType(TileTypeEnum tileTypeEnum)
        {
            Sprite cellSprite;
            switch (tileTypeEnum)
            {
                case(TileTypeEnum.Dirt):
                    cellSprite= IsometricGridConfig.Dirt;
                    return cellSprite;
                   
                case(TileTypeEnum.Grass):
                    cellSprite = IsometricGridConfig.Grass;
                    return cellSprite;
                
                case(TileTypeEnum.Stone):
                    cellSprite = IsometricGridConfig.Stone;
                    return cellSprite;
                
                case(TileTypeEnum.Wood):
                    cellSprite = IsometricGridConfig.Wood;
                    return cellSprite;
                
                default:
                    Debug.Log("No possible Tile");
                    break;
                    
            }

            return null;

        }

        void GenerateTile(GameObject tile,Vector3Int positionCoordinate,TileTypeEnum tileTypeEnum)
        {
            Sprite cellSprite=TileType(tileTypeEnum);
            tile.transform.position = positionCoordinate;
            tile.transform.parent = _grid.transform;
            GridCell gridCell = tile.GetComponent<GridCell>();
            gridCell.InitCell(new Vector2Int(positionCoordinate.x,positionCoordinate.y),tileTypeEnum,cellSprite);
            
        }
        
        #endregion
        
        
        
        #region Finding Neighbour
        
        private void CalculateNeighbours()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Col; c++)
                {
                    FindNeigbouringTile(new Vector2Int(r, c));
                }
            }
        }

        [Button]
        public void FindNeigbouringTile(Vector2Int neighbour)
        {
            _neighbourList.Clear();
            int minCol = 0, minRow = 0, maxCol = Col, maxRow = Rows;
            for (int i = 0; i < IsometricGridNeighbourFinding.NeighboursConstant.Count; i++)
            {
                var neighbourCoordinate = IsometricGridNeighbourFinding.FindNeighbours(neighbour, i);
                if (neighbourCoordinate.x >= minCol && neighbourCoordinate.x < maxCol && neighbourCoordinate.y >= minRow && neighbourCoordinate.y < maxRow )
                {
                    _neighbourList.Add(GridArray[neighbourCoordinate.x,neighbourCoordinate.y]);
                }
                 
            }
            
            GridArray[neighbour.x,neighbour.y].SetNeighbourList(_neighbourList);
        }
        
        
        #endregion 
    }
}