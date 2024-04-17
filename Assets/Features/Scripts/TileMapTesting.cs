using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
public class TileMapTesting : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] private Tile woodTile;
    [FormerlySerializedAs("positon")] [SerializeField] private Vector3Int position;
    private void Start()
    {
    // tilemap.SetTile(position,woodTile);   
    }
}
