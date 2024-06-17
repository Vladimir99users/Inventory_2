using Assets.Inventory.Scripts.Helpers.Cells;
using Inventory;
using System;
using UnityEngine;

namespace Assets.Inventory.Scripts.Model
{
    [Serializable]
    public abstract class Model
    {
        public Cell CurrentClickCell { get; set; }
        protected DataSave dataSave;
        public DataSave DataSave => dataSave;

        protected readonly View.View view;
        public Model(View.View view)
            => this.view = view;
        public abstract void AddItem();
        public abstract void MoveItemToFastHand(Cell cell);
        public abstract void MoveItemBetweenCells(Cell cell);
        public abstract void BuildInventory(DataSave dataSave = null);
        public abstract void MoveItem(Vector3 vector3);
        public abstract DataSave GetDataSave();
    }
}