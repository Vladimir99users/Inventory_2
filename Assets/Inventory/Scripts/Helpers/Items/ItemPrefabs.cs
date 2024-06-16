using UnityEngine;
using UnityEngine.UI;

namespace Assets.Inventory.Scripts.Helpers.Items
{
    public class ItemPrefabs : MonoBehaviour
    {
        public Image Image => GetComponent<Image>();
        public Vector2Int Size { get; private set; }
        public ItemType Type { get; private set; }
        public Sprite Sprite { get; private set; }
        public void Initialize(Item item)
        {
            Size = item.Size;
            Type = item.Type;
            Sprite = item.Sprite;
            Image.sprite = Sprite;
        }
    }
}
