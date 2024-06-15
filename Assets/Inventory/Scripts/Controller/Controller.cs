using UnityEngine;

namespace Assets.Inventory.Scripts.Controller
{

    public abstract class Controller : MonoBehaviour
    {
        protected Model.Model model;
        protected View.View view;

        public virtual void Initialize(Model.Model model, View.View view)
        {
            this.model = model;
            this.view = view;
        }

        public void OnMoveItem()
        {
            var positionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (model.CurrentClickCell != null)
            {
                model.MoveItem(new Vector3(positionMouse.x, positionMouse.y, 0));
                view.UpdatePositionCell(model.CurrentClickCell);
            }
        }

    }
}