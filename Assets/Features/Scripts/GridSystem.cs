using System.Collections;
using System.Collections.Generic;
using GridSystem.Grid;
using GridSystem.GridConfig;
using UnityEngine;

namespace GridSystem.GridGenerator
{


    [CreateAssetMenu(fileName = "GridGenerator", menuName = "GridSystem/GridSystem")]
    public class GridSystem : ScriptableObject
    {
        private IsometricGrid IsometricGrid;
        
        public void GenerateGrid(string datafile,string dataLayout,IsometricGridConfig isometricGridConfig, string GridName)
        {

          GameObject GridHolder=  Instantiate(new GameObject());
          
          GameObject gridContainer = new GameObject(GridName)
          {
              transform =
              {
                  position = Vector3.zero,
                  rotation = Quaternion.identity
              }
          };



          Grid.IsometricGrid isometricGrid =
              new IsometricGrid(gridContainer, dataLayout, datafile, isometricGridConfig);
        }
        
        
    }
}