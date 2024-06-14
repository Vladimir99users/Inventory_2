namespace Assets.Inventory.Scripts.Controller
{

    public abstract class Controller
    {
        protected readonly Model.Model model;
        protected readonly View view;

        public Controller(Model.Model model, View view)
        {
            this.model = model;
            this.view = view;
        }

    }
}