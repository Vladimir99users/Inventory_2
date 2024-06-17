using Assets.Inventory.Scripts.Helpers.Items;
using System;

namespace Assets.Inventory.Scripts.Helpers.Cells
{
    [Serializable]
    public class CellData
    {
        public int Height;
        public int Weight;
        public bool IsEmpty;
        public ItemType ItemType;
        public CellType CellType;
    }
}