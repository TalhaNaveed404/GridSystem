using System;
using System.Collections.Generic;
using GridSystem;
using GridSystem.GridConfig;

using ProjectCore.DataLoading;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Tile = UnityEngine.WSA.Tile;

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
        private string DataFile;
        private DataLoading DataLoading;
        private TerrainGridData TerrainGridData;
        private GameObject Grid;
       public IsometricGrid(GameObject Grid,string dataLayout,string datafile,IsometricGridConfig IsometricGridConfig)
       {

           this.Grid = Grid;
           DataLoading=Resources.Load<DataLoading>(dataLayout); 
           TerrainGridData=DataLoading.LoadData(datafile);
           Rows= TerrainGridData.TerrainGrid.Count;
           Col = TerrainGridData.TerrainGrid[0].Count;
           this.IsometricGridConfig = IsometricGridConfig;


           _dataArray = new int[Rows, Col];
           GridArray = new GridCell[Rows, Col];
           for (int i = 0; i < Rows; i++)
           {
               List<TileData> innerList = TerrainGridData.TerrainGrid[i];
               for (int j = 0; j < Col; j++)
               {
                   _dataArray[i, j] = innerList[j].TileType;
               }
           }

           // Display the 2D array
           for (int i = 0; i < Rows; i++)
           {
               string tiledata = "";
               for (int j = 0; j < Col; j++)
               {
                   TileTypeEnum tileTypeEnum= (TileTypeEnum)_dataArray[i, j];
                   GameObject tileObject = Instantiate(IsometricGridConfig.GridCellObject);
                   if(tileObject!=null)
                   GenerateTile(tileObject,new Vector3Int(i,j,0),TileType(tileTypeEnum));
                   GridArray[i, j] = tileObject.GetComponent<GridCell>(); 
                   
               }
               Debug.Log(tiledata);
            
           }

           CalculateNeighbours();


       }

       void ResourcesDataLoading()
       {
           
       }
       
        Sprite TileType(TileTypeEnum tileTypeEnum)
        {
            Vector3 tilePositition;
            Sprite CellSprite;
            switch (tileTypeEnum)
            {
                case(TileTypeEnum.Dirt):
                    CellSprite= IsometricGridConfig.Dirt;
                    return CellSprite;
                   
                case(TileTypeEnum.Grass):
                    CellSprite = IsometricGridConfig.Grass;
                    return CellSprite;
                
                case(TileTypeEnum.Stone):
                    CellSprite = IsometricGridConfig.Stone;
                    return CellSprite;
                
                case(TileTypeEnum.Wood):
                    CellSprite = IsometricGridConfig.Wood;
                    return CellSprite;
                
                default:
                    Debug.Log("No possible Tile");
                    break;
                    
            }

            return null;

        }

        void GenerateTile(GameObject Tile,Vector3Int PositionCoordinate,Sprite CellSprite)
        {
           
            Tile.transform.position = PositionCoordinate;
            Tile.transform.parent = Grid.transform;
            Tile.AddComponent<GridSystem.GridCell>();
            GridCell gridCell = Tile.GetComponent<GridCell>();
            gridCell.InitCell(new Vector2Int(PositionCoordinate.x,PositionCoordinate.y),CellSprite);
            
        }
        
        #region Finding Neighbour

         
        public void CalculateNeighbours()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Col; c++)
                {
                    FindNeigbhoringTile(new Vector2Int(r, c));
                    // NeighbouringTiles(new Vector2Int(c, c));

                }
            }
        }

        [Button]
        public void FindNeigbhoringTile(Vector2Int neighbour)
        {
            _neighbourList.Clear();
            int minCol = 0, minRow = 0, maxCol = Col, maxRow = Rows;
            for (int i = 0; i < IsometricGridUtilities.NeighboursConstant.Count; i++)
            {
                var neighbourCoordinate = IsometricGridUtilities.FindNeighbours(neighbour, i);
                if (neighbourCoordinate.x >= minCol && neighbourCoordinate.x < maxCol && neighbourCoordinate.y >= minRow && neighbourCoordinate.y < maxRow )//&& GridCells[item.x, item.y].GetViewState() == HexTileViewState.Enabled)
                {
                    _neighbourList.Add(GridArray[neighbourCoordinate.x,neighbourCoordinate.y]);
                }
                 
            }
            
            GridArray[neighbour.x,neighbour.y].SetNeighbourList(_neighbourList);
        }
        
        
        #endregion 
    }
}