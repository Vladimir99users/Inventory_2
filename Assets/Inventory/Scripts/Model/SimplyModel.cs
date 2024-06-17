using Assets.Inventory.Scripts.Boostrap;
using Assets.Inventory.Scripts.Helpers.Audio;
using Assets.Inventory.Scripts.Helpers.Cells;
using Assets.Inventory.Scripts.Helpers.Factory;
using Assets.Inventory.Scripts.Helpers.Items;
using Assets.Inventory.Scripts.Helpers.Message;
using Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Inventory.Scripts.Model
{
    [Serializable]
    public class SimplyModel : Model
    {
        private Cell[][] grid;
        private readonly ItemFactory itemFactory;
        private readonly CellFactory cellFactory;
        private readonly SettingModel settingModel;
        public List<Cell> GeneralCells = new();
        public List<Cell> FastHandCells = new();
        public Cell[][] Grid => grid;

        public SimplyModel(View.View view, SettingModel setting, ItemFactory itemFactory, CellFactory cellFactory) : base(view)
        {
            this.itemFactory = itemFactory;
            this.cellFactory = cellFactory;
            this.settingModel = setting;
            dataSave = new DataSave();

        }
        public override void BuildInventory(DataSave data = null)
        {
            grid = new Cell[settingModel.HeightGeneralInventory][];
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = new Cell[settingModel.WightGeneralInventory];
            }


            if (data is not null)
                BuildingInventoryFromSaveData(data);
            else
            {
                BuildingInventory(settingModel.HeightFastHandInventory, settingModel.WightFastHandInventory, FastHandCells, CellType.FastCellType);
                BuildingInventory(settingModel.HeightGeneralInventory, settingModel.WightGeneralInventory, GeneralCells, CellType.GeneralCellType);
            }

            view.Initialize(FastHandCells, GeneralCells);
            view.UpdateGeneralInventory();
            view.UpdateFastHandInventory();
        }

        public override DataSave GetDataSave()
            => new()
            {
                GeneralCells = GetDataFromInventory(GeneralCells),
                FastHandCells = GetDataFromInventory(FastHandCells)
            };

        private List<CellData> GetDataFromInventory(IEnumerable<Cell> inventory)
        {
            var saveData = new List<CellData>();

            foreach (var cell in inventory)
            {
                saveData.Add(new CellData
                {
                    CellType = cell.CellType,
                    Height = cell.Height,
                    Weight = cell.Weight,
                    IsEmpty = cell.IsEmpty,
                    ItemType = cell.ItemPrefabs?.Type ?? ItemType.None
                });
            }

            return saveData;
        }

        private void BuildingInventoryFromSaveData(DataSave data)
        {
            var dataGeneralCells = data.GeneralCells;
            var dataFastHandCells = data.FastHandCells;
            GeneralCells.Clear();
            foreach (var saveCell in dataGeneralCells)
            {
                var cell = cellFactory.GetObject(saveCell.CellType);
                cell.Height = saveCell.Height;
                cell.Weight = saveCell.Weight;
                grid[cell.Height][cell.Weight] = cell;
                GeneralCells.Add(cell);
            }

            foreach (var saveCell in dataGeneralCells)
            {
                if (saveCell.ItemType == ItemType.None)
                    continue;

                var cell = GeneralCells.FirstOrDefault(x => x.Height == saveCell.Height && x.Weight == saveCell.Weight);
                if (cell is not null && cell.IsEmpty)
                {
                    cell.ItemPrefabs = itemFactory.GetObject(saveCell.ItemType);
                    for (int i = cell.Height; i < (cell.Height + cell.ItemPrefabs.Size.y); i++)
                    {
                        for (int j = cell.Weight; j < (cell.Weight + cell.ItemPrefabs.Size.x); j++)
                        {
                            if (grid[i][j].ItemPrefabs is null)
                            {
                                grid[i][j].ItemPrefabs = cell.ItemPrefabs;
                            }
                        }
                    }
                }
            }

            FastHandCells.Clear();
            foreach (var saveCell in dataFastHandCells)
            {
                var cell = cellFactory.GetObject(saveCell.CellType);
                cell.Height = saveCell.Height;
                cell.Weight = saveCell.Weight;
                if (!saveCell.IsEmpty)
                    cell.ItemPrefabs = itemFactory.GetObject(saveCell.ItemType);

                grid[cell.Height][cell.Weight] = cell;
                FastHandCells.Add(cell);
            }
        }

        private void BuildingInventory(int height, int wight, ICollection<Cell> inventory, CellType type)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < wight; j++)
                {
                    var cell = cellFactory.GetObject(type);
                    cell.CellType = type;
                    cell.name = $"{type.ToString()} => {i}:{j}";
                    cell.Height = i;
                    cell.Weight = j;
                    grid[i][j] = cell;
                    inventory.Add(cell);
                }
            }
        }
        public override void AddItem()
        {
            if (IsInventoryFull())
                return;

            var emptyCell = FastHandCells.FirstOrDefault(x => x.IsEmpty);
            var item = itemFactory.GetObject(GetRandomItemType());
            emptyCell.ItemPrefabs = item;
            view.UpdateFastHandInventory();
        }

        private ItemType GetRandomItemType()
        {
            var values = Enum.GetValues(typeof(ItemType));
            return (ItemType)values.GetValue(Random.Range(1, values.Length));
        }
        public override void MoveItemBetweenCells(Cell cell)
        {
            if (FastHandCells.Contains(cell))
            {
                var cells = GeneralCells.Where(x => x.ItemPrefabs == CurrentClickCell.ItemPrefabs).ToList();
                cell.ItemPrefabs = CurrentClickCell.ItemPrefabs;
                foreach (var cel in cells)
                {
                    cel.ItemPrefabs = null;
                }

                CurrentClickCell.ItemPrefabs = null;
                CurrentClickCell = null;
            }
            else
            {
                var itemSize = CurrentClickCell.ItemPrefabs.Size;
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
                    if (IsPlacesEmpty(indexHeigth, indexWigth, itemSize) && !IsOutOfTheRange(indexHeigth, indexWigth, itemSize))
                    {
                        var cells = GeneralCells.Where(x => x.ItemPrefabs == CurrentClickCell.ItemPrefabs).ToList();
                        cell.ItemPrefabs = CurrentClickCell.ItemPrefabs;
                        foreach (var cel in cells)
                        {
                            cel.ItemPrefabs = null;
                        }


                        for (int i = indexHeigth; i < (indexHeigth + itemSize.y); i++)
                        {
                            for (int j = indexWigth; j < (indexWigth + itemSize.x); j++)
                            {
                                if (grid[i][j].ItemPrefabs is null)
                                {
                                    grid[i][j].ItemPrefabs = cell.ItemPrefabs;
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
        private bool IsPlacesEmpty(int indexHeigth, int indexWigth, Vector2Int itemSize)
        {

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

        private bool IsOutOfTheRange(int indexHeigth, int indexWigth, Vector2Int itemSize)
            => (indexHeigth + itemSize.y) > settingModel.HeightGeneralInventory ||
               (indexWigth + itemSize.x) > settingModel.WightGeneralInventory;



        public override void MoveItemToFastHand(Cell cell)
        {
            if (!GeneralCells.Contains(cell) || IsInventoryFull())
                return;

            var emptyCell = FastHandCells.FirstOrDefault(x => x.IsEmpty);
            emptyCell.ItemPrefabs = cell.ItemPrefabs;
            var cells = GeneralCells.Where(x => x.ItemPrefabs == cell.ItemPrefabs).ToList();
            foreach (var cel in cells)
            {
                cel.ItemPrefabs = null;
            }

            view.UpdateGeneralInventory();
            view.UpdateFastHandInventory();
        }

        private bool IsInventoryFull()
        {
            if (FastHandCells.All(x => !x.IsEmpty))
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