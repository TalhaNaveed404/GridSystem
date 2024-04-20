﻿using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace GridSystem
{
    public class GridCellHelper:MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI TileCoordinatesText; 
        [SerializeField] private GameObject TileMaterial;
        
        [SerializeField] private Color Normal;
        [SerializeField] private Color Selected;
        [SerializeField] private Color Neighbour;
        [SerializeField] private Color HighLighted;
        [SerializeField] private Color NeighbourPlacemement;
        public void SetCoordinatesText(string coordinates)
        {
            TileCoordinatesText.text = coordinates;
        }

        public void SetSprite(Sprite cellSprite)
        {
            this.GetComponent<SpriteRenderer>().sprite = cellSprite;
        }

        public void SetMaterialColor(GridTileState gridTileState)
        {

            switch (gridTileState)
            {
                case(GridTileState.normal):
                    TileMaterial.gameObject.GetComponent<SpriteRenderer>().material.color = Normal;////HexaMaterial.color = Normal;
                    break;
                case(GridTileState.selected):
                    TileMaterial.GetComponent<SpriteRenderer>().material.color = Selected;
                    break;
                case(GridTileState.neighbour):
                    TileMaterial.GetComponent<SpriteRenderer>().material.color = Neighbour;
                    break;
                case(GridTileState.neighbourPlacmement):
                    TileMaterial.GetComponent<SpriteRenderer>().material.color = NeighbourPlacemement;
                    break;
                case(GridTileState.placement):
                    TileMaterial.GetComponent<SpriteRenderer>().material.color = HighLighted;
                    break;
                
                
            }
       
        }

        public void NeighbourPlacement()
        {
            TileMaterial.GetComponent<SpriteRenderer>().material.color = NeighbourPlacemement;
        }
        
        public void HighLightPlacement()
        {
            TileMaterial.GetComponent<SpriteRenderer>().material.color = HighLighted;
        }
        
    }
}
public enum GridTileState
{
    normal,
    selected,
    neighbour,
    neighbourPlacmement,
    placement,
    
}
