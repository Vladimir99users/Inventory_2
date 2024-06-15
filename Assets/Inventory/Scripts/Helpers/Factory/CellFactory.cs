using Assets.Inventory.Scripts.Helpers.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Inventory.Scripts.Helpers.Factory
{
    [CreateAssetMenu(fileName = "Factory cell", menuName = "ScriptableObjects/Factory cells", order = 4)]
    public class CellFactory : Factory<Cell, CellType, CellSO>
    {
        [SerializeField] private List<CellSO> Items;
        public override Cell GetObject(CellType type)
        {
            var cell = Get(type);
            var prefab = Instantiate(cell.PrefabsCell);
            return prefab;
        }

        public override Cell GetObject(CellType type, Transform transform)
            => new();

        protected override CellSO Get(CellType type)
        {
            switch (type)
            {
                case CellType.FastCellType:
                    return GetSellSO(type);
                case CellType.GeneralCellType:
                    return GetSellSO(type);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private CellSO GetSellSO(CellType type)
            => Items.FirstOrDefault(x => x.CellType == type);
    }
}
