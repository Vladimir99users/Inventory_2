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
        private IList<Cell> generalCells = new List<Cell>();
        private IList<Cell> fastHandCells = new List<Cell>();

        private Cell[][] grid;

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
            grid = new Cell[settingModel.HeightGeneralInventory][];
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = new Cell[settingModel.WightGeneralInventory];
            }
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
                    grid[i][j] = cell;
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
            if (fastHandCells.Contains(cell))
            {
                cell.ItemPrefabs = CurrentClickCell.ItemPrefabs;
                CurrentClickCell.ItemPrefabs = null;
                CurrentClickCell = null;
            }
            else
            {
                var itemSize = CurrentClickCell.ItemPrefabs.Size; // 5, 2
                var indexHeigth = -1;
                var indexWigth = -1;
                for (int i = 0; i < settingModel.HeightGeneralInventory; i++)
                {
                    var indexCellInPlaceItem = Array.IndexOf(grid[i], cell);
                    if (indexCellInPlaceItem != -1)
                    {
                        indexHeigth = i;
                        indexWigth = indexCellInPlaceItem;
                        break;
                    }
                }

                if (indexHeigth != -1 && indexWigth != -1)
                {
                    if (TrySetItem(indexHeigth, indexWigth, itemSize))
                    {
                        cell.ItemPrefabs = CurrentClickCell.ItemPrefabs;
                        for (int i = indexHeigth; i < (indexHeigth + itemSize.y); i++)
                        {
                            for (int j = indexWigth; j < (indexWigth + itemSize.x); j++)
                            {
                                if (grid[i][j].ItemPrefabs is null)
                                {
                                    grid[i][j].ItemPrefabs = itemFactory.GetObject(CurrentClickCell.ItemPrefabs.Type);
                                }
                            }
                        }
                        CurrentClickCell.ItemPrefabs = null;
                        CurrentClickCell = null;
                    }
                }
            }
            view.UpdateGeneralInventory();
            view.UpdateFastHandInventory();
        }

        private bool TrySetItem(int indexHeigth, int indexWigth, Vector2Int itemSize)
        {
            if ((indexHeigth + itemSize.y) > settingModel.HeightGeneralInventory)
                return false;

            if ((indexWigth + itemSize.x) > settingModel.WightGeneralInventory)
                return false;

            for (int i = indexHeigth; i < (indexHeigth + itemSize.y); i++)
            {
                for (int j = indexWigth; j < (indexWigth + itemSize.x); j++)
                {
                    if (grid[i][j].ItemPrefabs != null)
                    {
                        return false;
                    }
                }
            }

            return true;
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