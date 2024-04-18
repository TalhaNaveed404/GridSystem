using System;
using System.Collections;
using System.Collections.Generic;
using GridSystem.GridConfig;
using GridSystem.GridGenerator;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
public class TileMapTesting : MonoBehaviour
{
    // [SerializeField] Tilemap tilemap;
    // [SerializeField] private Tile woodTile;
    // [FormerlySerializedAs("positon")] [SerializeField] private Vector3Int position;
    //
    [SerializeField] private string DataFile;
    [SerializeField] private string DataLayout;

    [SerializeField] private IsometricGridConfig IsometricGridConfig;
    [FormerlySerializedAs("GridGenerator")] [SerializeField] private GridSystem.GridGenerator.GridSystem GridSystem;
    [Button]
    public void CreateGrid()
    {
        GridSystem.GenerateGrid(DataFile,DataLayout,IsometricGridConfig,"IsometricGrid");
    }
    
}
