using UnityEngine;

namespace Assets.Inventory.Scripts.Helpers.Factory
{

    public abstract class Factory<TObject, TType, TItemSO> : ScriptableObject
    {
        public abstract TObject GetObject(TType type);
        public abstract TObject GetObject(TType type, Transform transform);
        protected abstract TItemSO Get(TType type);
    }

}


