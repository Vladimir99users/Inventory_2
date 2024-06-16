using Assets.Inventory.Scripts.Helpers.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Inventory.Scripts.Helpers.Cells
{
    public class Cell : MonoBehaviour
    {
        public CellType CellType { get; set; }

        [SerializeField] private ItemPrefabs itemPrefabs;
        public ItemPrefabs ItemPrefabs
        {
            get => itemPrefabs;
            set
            {
                itemPrefabs = value;
                var color = itemPrefabs is null ? Color.white : Color.gray;
                Image.color = color;
            }
        }

        public bool IsEmpty => itemPrefabs is null;
        private Image Image => GetComponent<Image>();
    }
}
