using System;
using UnityEditor;
using UnityEngine;

namespace GridSystem
{
    public class Table:MonoBehaviour
    {
        public Vector3 StartPosition;
        public bool Placed;
        public GameObject TableSpriteHorizontal;
        public GameObject TableSpriteVertical;
        public bool IsHorizontal;
        private void Start()
        {
            HorizontalTable();
            IsHorizontal = true;
        }

       public void SetTableRotation()
        {
            if (IsHorizontal)
            {
                VerticalTable();
            }
            else
            {
                HorizontalTable();
            }

            IsHorizontal = !IsHorizontal;
        }
        
        private void HorizontalTable()
        {
            
            TableSpriteHorizontal.SetActive(true);
            TableSpriteVertical.SetActive(false);
        }

        private void VerticalTable()
        {
            TableSpriteHorizontal.SetActive(false);
            TableSpriteVertical.SetActive(true);
        }
        
       
    }
}