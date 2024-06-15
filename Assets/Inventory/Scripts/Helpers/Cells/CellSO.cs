using UnityEngine;

namespace Assets.Inventory.Scripts.Helpers.Cells
{
    [CreateAssetMenu(fileName = "Inventory cell", menuName = "ScriptableObjects/Cells", order = 3)]
    public class CellSO : ScriptableObject
    {
        public Cell PrefabsCell;
        public CellType CellType;
    }
}
