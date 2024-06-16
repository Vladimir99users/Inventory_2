using UnityEngine;

namespace Assets.Inventory.Scripts.Helpers.Items
{
    public class Item
    {
        public string Name { get; }
        public ItemType Type { get; }
        public Sprite Sprite { get; }
        public Vector2Int Size { get; }

        public Item() { }

        public Item(ItemSO itemSo)
        {
            Name = itemSo.name;
            Type = itemSo.ItemType;
            Sprite = itemSo.VisualizationSprite;
            Size = itemSo.Size;
        }
    }
}
