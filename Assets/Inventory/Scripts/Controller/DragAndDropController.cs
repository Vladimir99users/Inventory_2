using Assets.Inventory.Scripts.Helpers.Cells;
using Assets.Inventory.Scripts.Helpers.Message;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Assets.Inventory.Scripts.Controller
{
    public class DragAndDropController : Controller, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Button generateItemButton;

        private void OnEnable()
            => generateItemButton.onClick.AddListener(GenerateObjectInFastHandInventory);
        private void OnDisable()
            => generateItemButton.onClick.RemoveListener(GenerateObjectInFastHandInventory);

        private void GenerateObjectInFastHandInventory()
        {
            model.AddItem();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.pointerPressRaycast.gameObject.TryGetComponent(out Cell cell))
            {
                if (cell.ItemPrefabs != null)
                {
                    model.CurrentClickCell = cell;
                }
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnMoveItem();
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out Cell cell))
            {
                if (!cell.IsEmpty && model.CurrentClickCell != null && model.CurrentClickCell != cell)
                {
                    view.DisplayText(MessagePlayerContainers.PlaceIsNotEmpty);
                    view.UpdateVisial();
                    return;
                }

                if (model.CurrentClickCell != null && model.CurrentClickCell != cell && cell.IsEmpty)
                {
                    model.MoveItemBetweenCells(cell);
                }
            }

            if (model.CurrentClickCell != null)
            {
                view.UpdateVisial();
            }
        }
    }
}