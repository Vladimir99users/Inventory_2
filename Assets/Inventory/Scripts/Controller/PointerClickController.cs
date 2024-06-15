using Assets.Inventory.Scripts.Helpers.Cells;
using Assets.Inventory.Scripts.Helpers.Message;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Inventory.Scripts.Controller
{
    public class PointerClickController : Controller, IPointerUpHandler, IPointerDownHandler, IPointerMoveHandler
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

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.pointerPressRaycast.gameObject.TryGetComponent(out Cell cell))
            {
                if (model.CurrentClickCell != null && model.CurrentClickCell != cell && cell.IsEmpty)
                {
                    model.MoveItemBetweenCells(cell);
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.pointerPressRaycast.gameObject.TryGetComponent(out Cell cell))
            {
                if (!cell.IsEmpty && model.CurrentClickCell != null && model.CurrentClickCell != cell)
                {
                    view.DisplayText(MessagePlayerContainers.PlaceIsNotEmpty);
                    return;
                }

                if (cell.ItemPrefabs != null)
                {
                    model.CurrentClickCell = cell;
                }
            }
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            var positionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(positionMouse);
            if (model.CurrentClickCell != null)
            {
                model.MoveItem(new Vector3(positionMouse.x, positionMouse.y, 0));
                view.UpdatePositionCell(model.CurrentClickCell);
            }
        }
    }
}