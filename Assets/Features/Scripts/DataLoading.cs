using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;


namespace ProjectCore.DataLoading
{
    [CreateAssetMenu(fileName = "DataLoading",menuName = "ProjectCore/DataLoading")]
    public class DataLoading : ScriptableObject
    {
        
        [SerializeField] private string FileName;

        
       // public TerrainGridData TerrainGridData; 
        [Button]
        void LoadData(string filename)
        {
            TextAsset jsonData = Resources.Load<TextAsset>(filename);
            Debug.Log("Data\n"+jsonData);
            if (jsonData != null)
            {
            //    TerrainGridData = JsonUtility.FromJson<TerrainGridData>(jsonData.text);
                
                
                
                // foreach (var row in TerrainGridData.TerrainGrid)
                // {
                //     Debug.Log("Row"+row);
                //     foreach (var element in row)
                //     {
                //         Debug.Log(element + " ");
                //     }
                //     Debug.Log(" ");
                // }
                
           //     Debug.Log("TerrainGrid"+TerrainGridData);
           //     
            //    Debug.Log("TerrainGrid"+TerrainGridData.TerrainGrid.Count);
            }
            
        }

        [Button]
        void SaveData(string fileName)
        {
            string  path = @"E:\UnityProjects\Assignment\GridSystem\Assets\Features\Resources";
            //string path = @"E:\UnityProjects\HazelProjects\hexasort\Assets\HexaGamePlay\JsonData";
            string filePath = Path.Combine(path, fileName+".json");
          //  string jsonData = JsonUtility.ToJson(TerrainGridData);
       //     File.WriteAllText(filePath, jsonData);

        }

        [Button]
        void NewDataLoading(string filename)
        {
            TextAsset jsonData = Resources.Load<TextAsset>(filename);
            Debug.Log("JsonDatat"+jsonData);
            string json = System.IO.File.ReadAllText(jsonData.text);

            // Deserialize JSON to TerrainData object
            TerrainData terrainData = JsonUtility.FromJson<TerrainData>(json);

            // Display the loaded data
            Console.WriteLine("Loaded TerrainGrid:");
            foreach (var row in terrainData.TerrainGrid)
            {
                foreach (var tile in row)
                {
                    Console.Write($"{tile.TileType} ");
                }
                Console.WriteLine();
            }

            // Serialize the data back to JSON
            string serializedJson = JsonUtility.ToJson(terrainData);// Formatting.Indented);

            // Save the JSON to a file
            System.IO.File.WriteAllText("new_data.json", serializedJson);
            Console.WriteLine("Data saved to new_data.json");
        }

    }
    // Classes to represent the structure of the JSON data
    public class Tile
    {
        public int TileType { get; set; }
    }

    public class TerrainData
    {
        public List<List<Tile>> TerrainGrid { get; set; }
    }
    [Serializable]
    public enum TileType
    {
        Dirt,
        Grass,
        Stone,
        Wood
    }
}