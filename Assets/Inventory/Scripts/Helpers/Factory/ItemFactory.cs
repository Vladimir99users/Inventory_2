using Assets.Inventory.Scripts.Helpers.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Inventory.Scripts.Helpers.Factory
{
    [CreateAssetMenu(fileName = "Factory items", menuName = "ScriptableObjects/Factory Items", order = 2)]
    public class ItemFactory : Factory<ItemPrefabs, ItemType, ItemSO>
    {
        [SerializeField] private List<ItemSO> Items;
        [SerializeField] private ItemPrefabs prefabsItems;
        public int CountItems => Items.Count;
        public override ItemPrefabs GetObject(ItemType type)
        {
            var itemSO = Get(type);
            var item = new Item(itemSO);
            var newPrefabs = Instantiate(prefabsItems);
            newPrefabs.Initialize(item);
            return newPrefabs;
        }

        public override ItemPrefabs GetObject(ItemType type, Transform transform)
            => new();


        protected override ItemSO Get(ItemType type)
        {
            switch (type)
            {
                case ItemType.Apple:
                    return GetItemFromType(type);
                case ItemType.Lemon:
                    return GetItemFromType(type);
                case ItemType.Cherry:
                    return GetItemFromType(type);
                case ItemType.Candy:
                    return GetItemFromType(type);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private ItemSO GetItemFromType(ItemType type)
            => Items.FirstOrDefault(x => x.ItemType == type);
    }
}
