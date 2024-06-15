using Assets.Inventory.Scripts.Boostrap;
using Assets.Inventory.Scripts.Helpers.Audio;
using Assets.Inventory.Scripts.Helpers.Cells;
using Assets.Inventory.Scripts.Helpers.Factory;
using Assets.Inventory.Scripts.Helpers.Items;
using Assets.Inventory.Scripts.Helpers.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Inventory.Scripts.Model
{

    public class SimplyModel : Model
    {
        private ICollection<Cell> generalCells = new List<Cell>();
        private ICollection<Cell> fastHandCells = new List<Cell>();

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
            if (IsInventoryFull())
                return;

            var emptyCell = fastHandCells.FirstOrDefault(x => x.IsEmpty);
            var item = itemFactory.GetObject(GetRandomItemType());
            emptyCell.ItemPrefabs = item;
            view.UpdateFastHandInventory();
        }

        private ItemType GetRandomItemType()
        {
            var values = Enum.GetValues(typeof(ItemType));
            return (ItemType)values.GetValue(Random.Range(0, values.Length));
        }


        public override void MoveItemBetweenCells(Cell cell)
        {
            cell.ItemPrefabs = CurrentClickCell.ItemPrefabs;
            CurrentClickCell.ItemPrefabs = null;
            CurrentClickCell = null;
            view.UpdateGeneralInventory();
            view.UpdateFastHandInventory();
        }

        public override void MoveItemToFastHand(Cell cell)
        {
            if (!generalCells.Contains(cell) || IsInventoryFull())
                return;

            var emptyCell = fastHandCells.FirstOrDefault(x => x.IsEmpty);
            emptyCell.ItemPrefabs = cell.ItemPrefabs;
            cell.ItemPrefabs = null;
            view.UpdateGeneralInventory();
            view.UpdateFastHandInventory();
        }

        private bool IsInventoryFull()
        {
            if (fastHandCells.All(x => !x.IsEmpty))
            {
                view.DisplayText(MessagePlayerContainers.InventoryIsFullIsNotEmpty);
                AudioController.Instance.PlayErrorSound();
                return true;
            }

            return false;
        }

        public override void MoveItem(Vector3 vector)
            => CurrentClickCell.ItemPrefabs.transform.position = vector;



    }
}