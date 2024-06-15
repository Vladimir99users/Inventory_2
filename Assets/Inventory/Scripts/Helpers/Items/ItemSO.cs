using UnityEngine;

namespace Assets.Inventory.Scripts.Helpers.Items
{
    [CreateAssetMenu(fileName = "Inventory item", menuName = "ScriptableObjects/Item", order = 1)]
    public class ItemSO : ScriptableObject
    {
        public string Name;
        public ItemType ItemType;
        public Sprite VisualizationSprite;
    }
}
