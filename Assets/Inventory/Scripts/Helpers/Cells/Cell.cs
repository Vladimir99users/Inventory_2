using Assets.Inventory.Scripts.Helpers.Items;
using UnityEngine;

namespace Assets.Inventory.Scripts.Helpers.Cells
{
    public class Cell : MonoBehaviour
    {
        public bool isEmpty = true;
        public CellType CellType { get; set; }

        public ItemPrefabs ItemPrefabs { get; set; }


    }
}
