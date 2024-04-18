using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json;

namespace ProjectCore.DataLoading
{
    [CreateAssetMenu(fileName = "DataLoading",menuName = "ProjectCore/DataLoading")]
    public class DataLoading : ScriptableObject
    {
        
        [SerializeField] private string FileName;
        
        public TerrainGridData TerrainGridData; 
        [Button]
        public TerrainGridData LoadData(string filename)
        {
            TextAsset jsonData = Resources.Load<TextAsset>(filename);
            Debug.Log("Data\n"+jsonData);
            if (jsonData != null)
            {
                TerrainGridData = JsonConvert.DeserializeObject<TerrainGridData>(jsonData.text);
                ShowData();
                return TerrainGridData;
            }

            return null;

        }
        
        void ShowData()
        {
            foreach (var row in TerrainGridData.TerrainGrid)
            {
                Debug.Log("Row"+row);
                string _singleRow = " ";
                foreach (var element in row)
                {
                    _singleRow += element.TileType + " ";
                }
                Debug.Log(_singleRow);                    
            }
            
        }

    }
    // Classes to represent the structure of the JSON data
    public class TileData
    {
        public int TileType { get; set; }
    }

    public class TerrainGridData
    {
        public List<List<TileData>> TerrainGrid { get; set; }
    }
  
}