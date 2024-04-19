using System;
using UnityEditor;
using UnityEngine;

namespace GridSystem
{
    public class Table:MonoBehaviour
    {
        public Vector3 StartPosition;
        public bool Placed;
        public Sprite TableSpriteHorizontal;
        public Sprite TableSpriteVertical;

        private void Start()
        {
            HorizontalTable();
        }

        private void HorizontalTable()
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = TableSpriteHorizontal;
        }

        private void VerticalTable()
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = TableSpriteVertical;
        }
    }
}