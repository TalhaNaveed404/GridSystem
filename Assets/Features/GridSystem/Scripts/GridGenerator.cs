using GridSystem.GridConfig;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
public class GridGenerator : MonoBehaviour
{
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
