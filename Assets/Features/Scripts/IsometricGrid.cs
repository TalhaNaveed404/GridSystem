using System;
using System.Collections.Generic;
using GridSystem.GridTile;
using GridSystem.GridConfig;

using ProjectCore.DataLoading;
using UnityEditor;
using UnityEngine;
using Tile = UnityEngine.WSA.Tile;

namespace GridSystem.Grid
{  
    
    [CreateAssetMenu(fileName = "IsometricGrid",menuName = "GridSystem/IsometricGrid")]
    public class IsometricGrid:ScriptableObject
    {
        [SerializeField] private GameObject TileObject;
        [SerializeField] private GameObject Grid;
        [SerializeField] private int Rows;
        [SerializeField] private int Col;
        [SerializeField] private IsometricGridConfig IsometricGridConfig;
        
        
        public int[,] GridArray ;//= new int[rows, columns];
        public string DataFile;
        public DataLoading DataLoading;
        private TerrainGridData TerrainGridData;
       public IsometricGrid(GameObject Grid,string dataLayout,string datafile,IsometricGridConfig IsometricGridConfig)
       {


           this.Grid = Grid;
           DataLoading=Resources.Load<DataLoading>(dataLayout); 
           TerrainGridData=DataLoading.LoadData(datafile);
           Rows= TerrainGridData.TerrainGrid.Count;
           Col = TerrainGridData.TerrainGrid[0].Count;
           this.IsometricGridConfig = IsometricGridConfig;


           GridArray = new int[Rows, Col];

           for (int i = 0; i < Rows; i++)
           {
               List<TileData> innerList = TerrainGridData.TerrainGrid[i];
               for (int j = 0; j < Col; j++)
               {
                   GridArray[i, j] = innerList[j].TileType;
               }
           }

           // Display the 2D array
           for (int i = 0; i < Rows; i++)
           {
               string tiledata = "";
               for (int j = 0; j < Col; j++)
               {
                   TileTypeEnum tileTypeEnum= (TileTypeEnum)GridArray[i, j];
                   GameObject tileObject = Instantiate(IsometricGridConfig.GridCellObject);
                   if(tileObject!=null)
                   GenerateTile(tileObject,new Vector3Int(i,j,0),TileType(tileTypeEnum));
                   
               }
               Debug.Log(tiledata);
            
           }
           
         
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
            Tile.AddComponent<GridTile.GridCell>();
            GridCell gridCell = Tile.GetComponent<GridCell>();
            gridCell.InitCell(new Vector2Int(PositionCoordinate.x,PositionCoordinate.y),CellSprite);
        }
        
        
    }
}