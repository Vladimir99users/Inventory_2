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

    }
}