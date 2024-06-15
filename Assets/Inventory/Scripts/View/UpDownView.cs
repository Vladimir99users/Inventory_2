using Assets.Inventory.Scripts.Helpers.Cells;
using Assets.Inventory.Scripts.Helpers.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Inventory.Scripts.View
{
    public class UpDownView : View
    {
        [SerializeField] private Transform generalContentTransform;
        [SerializeField] private Transform fastHandContentTransform;

        [SerializeField] private List<Cell> generalCells;
        private ICollection<ItemPrefabs> generalItems;

        [SerializeField] private List<Cell> fastHandCells;
        private ICollection<ItemPrefabs> fastHandItems;
        public override void Initialize(ICollection<Cell> fastHandCells, ICollection<Cell> generalCells)
        {
            this.generalCells = new List<Cell>(generalCells);
            this.fastHandCells = new List<Cell>(fastHandCells);
            DisplayInventory(this.fastHandCells, fastHandContentTransform);
            DisplayInventory(this.generalCells, generalContentTransform);
        }

        private void DisplayInventory(ICollection<Cell> cells, Transform parent)
        {
            foreach (var cell in cells)
            {
                cell.transform.SetParent(parent);
                cell.gameObject.transform.localScale = Vector3.one;
            }
        }

        public override void UpdateGeneralInventory(ICollection<Cell> cells)
        {

        }

        public override void UpdateFastHandInventory(ICollection<Cell> cells)
        {

        }
    }
}