using Assets.Inventory.Scripts.Helpers.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Inventory.Scripts.Helpers.Cells
{

    public class Cell : MonoBehaviour
    {
        public int Height;
        public int Weight;
        public CellType CellType { get; set; }

        [SerializeField] private ItemPrefabs itemPrefabs;
        [SerializeField] private Image ChildImage;
        public ItemPrefabs ItemPrefabs
        {
            get => itemPrefabs;
            set
            {
                itemPrefabs = value;
                var color = itemPrefabs is null ? Color.white : Color.gray;
                Image.color = color;
                ChildImage.sprite = itemPrefabs?.Sprite;
                ChildImage.color = itemPrefabs?.Sprite is null ? new Color(1, 1, 1, 0) : new Color(1, 1, 1, 1);
            }
        }

        public bool IsEmpty => itemPrefabs is null;
        private Image Image => GetComponent<Image>();


    }
}
