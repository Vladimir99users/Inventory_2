using UnityEngine;
using UnityEngine.UI;

namespace Assets.Inventory.Scripts.Helpers.Items
{
    public class ItemPrefabs : MonoBehaviour
    {
        [SerializeField] private Image image;

        public void Initialize(Item item)
        {
            image.sprite = item.Sprite;
        }
    }
}
