using System;
using UnityEngine;

namespace GridSystem.GridConfig
{
    [CreateAssetMenu(fileName = "GridConfig",menuName = "GridSystem/GridConfig")]
    public class IsometricGridConfig
    {
        public Sprite Dirt; 
        public Sprite Grass;
        public Sprite Stone;
        public Sprite Wood;
    }
    
    
    
    [Serializable]
    public enum TileTypeEnum
    {
        Dirt=0,
        Grass=1,
        Stone=2,
        Wood=3
    }
}