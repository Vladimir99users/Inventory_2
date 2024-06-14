namespace Assets.Inventory.Scripts.Model
{
    public abstract class Model
    {
        protected readonly View view;
        public Model(View view)
            => this.view = view;
    }
}