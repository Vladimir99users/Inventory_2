using Assets.Inventory.Scripts.Boostrap;
using Assets.Inventory.Scripts.Helpers.Cells;
using Assets.Inventory.Scripts.Helpers.Factory;
using Assets.Inventory.Scripts.Helpers.Items;
using Assets.Inventory.Scripts.Helpers.Message;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Inventory.Scripts.Model
{

    public class SimplyModel : Model
    {
        private ICollection<Cell> generalCells = new List<Cell>();
        private ICollection<ItemPrefabs> eneralItems = new List<ItemPrefabs>();
        private ICollection<Cell> fastHandCells = new List<Cell>();
        private ICollection<ItemPrefabs> fastHandItems = new List<ItemPrefabs>();
        private readonly ItemFactory itemFactory;
        private readonly CellFactory cellFactory;
        private readonly SettingModel settingModel;

        public SimplyModel(View.View view, SettingModel setting, ItemFactory itemFactory, CellFactory cellFactory) : base(view)
        {
            this.itemFactory = itemFactory;
            this.cellFactory = cellFactory;
            this.settingModel = setting;
        }
        public override void BuildInventory()
        {
            BuildingInventory(settingModel.HeightFastHandInventory, settingModel.WightFastHandInventory, fastHandCells, CellType.FastCellType);
            BuildingInventory(settingModel.HeightGeneralInventory, settingModel.WightGeneralInventory, generalCells, CellType.GeneralCellType);
            view.Initialize(fastHandCells, generalCells);
        }

        private void BuildingInventory(int height, int wight, ICollection<Cell> inventory, CellType type)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < wight; j++)
                {
                    var cell = cellFactory.GetObject(type);
                    cell.name = $"{type.ToString()} => {i}:{j}";
                    inventory.Add(cell);
                }
            }
        }
        public override void AddItem()
        {
            if (fastHandCells.All(x => !x.IsEmpty))
            {
                view.DisplayText(MessagePlayerContainers.InventoryIsFullIsNotEmpty);
                return;
            }

            var emptyCell = fastHandCells.FirstOrDefault(x => x.IsEmpty);
            var item = itemFactory.GetObject(ItemType.Apple);
            emptyCell.ItemPrefabs = item;
            fastHandItems.Add(item);
            view.UpdateFastHandInventory(fastHandCells);
        }

        public override void MoveItemBetweenCells(Cell cell)
        {
            cell.ItemPrefabs = CurrentClickCell.ItemPrefabs;
            cell.ItemPrefabs.gameObject.name = $"ItemPrefabs = {cell.gameObject.name}";
            CurrentClickCell.ItemPrefabs = null;
            CurrentClickCell = null;
            view.UpdateGeneralInventory(generalCells);
            view.UpdateFastHandInventory(fastHandCells);
        }

        public override void RemoveItem()
        {

        }

        public override void MoveItem(Vector3 vector)
        {
            CurrentClickCell.ItemPrefabs.transform.position = vector;
        }


    }
}