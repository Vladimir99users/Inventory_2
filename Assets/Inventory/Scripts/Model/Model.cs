using Assets.Inventory.Scripts.Helpers.Cells;
using UnityEngine;

namespace Assets.Inventory.Scripts.Model
{
    public abstract class Model
    {
        public Cell CurrentClickCell { get; set; }

        protected readonly View.View view;
        public Model(View.View view)
            => this.view = view;
        public abstract void AddItem();
        public abstract void MoveItemToFastHand(Cell cell);
        public abstract void MoveItemBetweenCells(Cell cell);
        public abstract void BuildInventory();
        public abstract void MoveItem(Vector3 vector3);
    }
}