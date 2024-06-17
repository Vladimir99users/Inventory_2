using Assets.Inventory.Scripts.Helpers.Audio;
using Assets.Inventory.Scripts.Helpers.Cells;
using Assets.Inventory.Scripts.Helpers.Message;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Assets.Inventory.Scripts.Controller
{
    public class DragAndDropController : Controller, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Button generateItemButton;

        private void OnEnable()
            => generateItemButton.onClick.AddListener(GenerateObjectInFastHandInventory);
        private void OnDisable()
            => generateItemButton.onClick.RemoveListener(GenerateObjectInFastHandInventory);

        private void GenerateObjectInFastHandInventory()
        {
            model.AddItem();
            AudioController.Instance.PlayCreateItemSound();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.pointerPressRaycast.gameObject.TryGetComponent(out Cell cell))
            {
                if (cell.ItemPrefabs is not null)
                {
                    model.CurrentClickCell = cell;
                    AudioController.Instance.PlayBeginSound();
                }
            }
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (model.CurrentClickCell?.IsEmpty == true)
                return;

            if (model.CurrentClickCell != null)
                model.CurrentClickCell.ItemPrefabs.Image.color = new Color(1, 1, 1, 1);

            OnMoveItem();
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject?.TryGetComponent(out Cell cell) == true)
            {
                if (!cell.IsEmpty && model.CurrentClickCell != null && model.CurrentClickCell != cell)
                {
                    AudioController.Instance.PlayErrorSound();
                    view.DisplayText(MessagePlayerContainers.PlaceIsNotEmpty);
                    view.UpdateVisual();
                    model.CurrentClickCell = null;
                    return;
                }

                if (model.CurrentClickCell != null && model.CurrentClickCell != cell && cell.IsEmpty)
                {
                    model.CurrentClickCell.ItemPrefabs.Image.color = new Color(1, 1, 1, 0);
                    model.MoveItemBetweenCells(cell);
                    AudioController.Instance.PlayGoodSound();
                    return;
                }
            }

            if (model.CurrentClickCell != null)
            {
                AudioController.Instance.PlayErrorSound();
                view.UpdateVisual();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.pointerPressRaycast.gameObject.TryGetComponent(out Cell cell))
            {
                if (model.CurrentClickCell == null && model.CurrentClickCell != cell && !cell.IsEmpty)
                {
                    model.MoveItemToFastHand(cell);
                    AudioController.Instance.PlayGoodSound();
                }
            }
        }

    }
}