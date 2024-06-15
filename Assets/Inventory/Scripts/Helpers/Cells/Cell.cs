using Assets.Inventory.Scripts.Helpers.Items;
using UnityEngine;

namespace Assets.Inventory.Scripts.Helpers.Cells
{
    public class Cell : MonoBehaviour
    {
        public bool IsEmpty => ItemPrefabs is null;
        public CellType CellType { get; set; }

        public ItemPrefabs ItemPrefabs;


    }
}
