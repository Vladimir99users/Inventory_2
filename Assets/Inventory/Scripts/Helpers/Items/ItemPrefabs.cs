using UnityEngine;
using UnityEngine.UI;

namespace Assets.Inventory.Scripts.Helpers.Items
{
    public class ItemPrefabs : MonoBehaviour
    {
        [SerializeField] private Image image;
        public Vector2Int Size { get; private set; }
        public ItemType Type { get; private set; }

        public void Initialize(Item item)
        {
            image.sprite = item.Sprite;
            Size = item.Size;
            Type = item.Type;
        }
    }
}
