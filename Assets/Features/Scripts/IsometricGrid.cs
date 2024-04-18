using System;
using System.Collections.Generic;
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
                   tiledata += GridArray[i, j];
                  // Console.Write(GridArray[i, j] + " ");
               }
               Debug.Log(tiledata);
               //Console.WriteLine();
           }
           
           // foreach (var TileList in TerrainGridData.TerrainGrid)
           // {
           //     foreach (var Tile in TileList)
           //     {
           //         TileTypeEnum tileTypeEnum = (TileTypeEnum)Tile.TileType;
           //         GenerateTile(tileTypeEnum);
           //     }
           // }
        }


        void GenerateTile(TileTypeEnum tileTypeEnum, Vector3 positionCoordinate)
        {
            Vector3 tilePositition;
            GameObject Tile;
            switch (tileTypeEnum)
            {
                case(TileTypeEnum.Dirt):
                   Tile= Instantiate(TileObject);
                   break;
                    
                    default:
                        Debug.Log("No possible Tile");
                        break;
                    
            }
            
        }
        
    }
}