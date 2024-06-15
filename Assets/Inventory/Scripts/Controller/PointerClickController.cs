using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Inventory.Scripts.Controller
{
    public class PointerClickController : Controller, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler, IPointerMoveHandler
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
        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("Enter");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("Exit");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("Up");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Down");
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            Debug.Log("Move");
        }
    }
}