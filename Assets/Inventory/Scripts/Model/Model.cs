using Assets.Inventory.Scripts.Helpers.Cells;
using UnityEngine;

namespace Assets.Inventory.Scripts.Model
{
    public abstract class Model
    {
        public Cell OnClickCell { get; set; }

        protected readonly View.View view;
        public Model(View.View view)
            => this.view = view;
        public abstract void AddItem();
        public abstract void RemoveItem();

        public abstract void BuildInventory(MonoBehaviour helperMonoBehaviour);

    }
}