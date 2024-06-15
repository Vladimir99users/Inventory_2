using Assets.Inventory.Scripts.Helpers.Cells;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Inventory.Scripts.View
{
    public abstract class View : MonoBehaviour
    {
        public virtual void Initialize(IEnumerable<Cell> fasthandCells, IEnumerable<Cell> generalCells)
        {
        }

        public abstract void UpdateGeneralInventory();
        public abstract void UpdateFastHandInventory();
        public abstract void UpdatePositionCell(Cell currentClickCell);
        public abstract void UpdateVisual();
        public virtual void DisplayText(string str, int time = 1) { }
    }
}