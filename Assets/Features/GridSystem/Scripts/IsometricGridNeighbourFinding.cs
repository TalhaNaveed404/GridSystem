using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class IsometricGridNeighbourFinding
    {
        public static List<Vector2Int> NeighboursConstant = new List<Vector2Int>()
        {
            new Vector2Int(0,1),
           // new Vector2Int(1,1),
            new Vector2Int(1,0),
          //  new Vector2Int(1,-1),
            new Vector2Int(0,-1),
         //   new Vector2Int(-1,-1),
            new Vector2Int(-1,0),
        //    new Vector2Int(-1,1),
        };


        public static Vector2Int  FindNeighbours(Vector2Int CellCoordinate, int index)
        {
            Vector2Int neighbour = CellCoordinate + NeighboursConstant[index];
            return  neighbour;
        }
    }
}