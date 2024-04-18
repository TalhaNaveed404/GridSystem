using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace GridSystem.GridTile
{
    public class GridCellHelper:MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI TileCoordinatesText;
        [FormerlySerializedAs("HexaMaterial")] [SerializeField] private GameObject TileMaterial;
        [SerializeField] private Sprite CellSprite;

        [SerializeField] private Color Normal;
        [SerializeField] private Color Selected;
        [SerializeField] private Color Neighbour;
    
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
                    TileMaterial.gameObject.GetComponent<MeshRenderer>().material.color = Normal;////HexaMaterial.color = Normal;
                    break;
                case(GridTileState.selected):
                    TileMaterial.GetComponent<MeshRenderer>().material.color = Selected;
                    break;
                case(GridTileState.neighbour):
                    TileMaterial.GetComponent<MeshRenderer>().material.color = Neighbour;
                    break;
                
            }
       
        }
    }
}
public enum GridTileState
{
    normal,
    selected,
    neighbour
    
}
