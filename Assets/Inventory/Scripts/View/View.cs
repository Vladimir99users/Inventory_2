using Assets.Inventory.Scripts.Helpers.Cells;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Inventory.Scripts.View
{
    public abstract class View : MonoBehaviour
    {
        public virtual void Initialize(ICollection<Cell> fasthandCells, ICollection<Cell> generalCells)
        {
        }

        public abstract void UpdateGeneralInventory(ICollection<Cell> cells);
        public abstract void UpdateFastHandInventory(ICollection<Cell> cells);
    }
}