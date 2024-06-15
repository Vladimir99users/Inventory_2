using Assets.Inventory.Scripts.Boostrap;
using Assets.Inventory.Scripts.Controller;
using Assets.Inventory.Scripts.Helpers.Factory;
using Assets.Inventory.Scripts.Model;
using Assets.Inventory.Scripts.View;
using UnityEngine;

namespace Inventory
{
    public class Bootstrap : MonoBehaviour
    {
        [Header("Helper attributes")]
        [SerializeField] private Transform parentControllerTransform;
        [SerializeField] private ItemFactory itemFactory;
        [SerializeField] private CellFactory cellFactory;

        [Header("Simply View component")]
        [SerializeField] private View simplyView;
        [Header("Controller")]
        [SerializeField] private Controller controller;
        [Header("Model setting")]
        [SerializeField] private SettingModel settingModel;


        private void Awake()
        {
            if (!IsCheckGameObjectExist())
                return;

            var controllerPrefabs = Instantiate(controller, parentControllerTransform);
            var viewPrefabs = Instantiate(simplyView, controllerPrefabs.transform);
            viewPrefabs.transform.SetParent(controllerPrefabs.transform);
            viewPrefabs.transform.SetAsFirstSibling();
            var model = new SimplyModel(viewPrefabs, settingModel, itemFactory, cellFactory);
            model.BuildInventory(this);
            controllerPrefabs.Initialize(model, viewPrefabs);
        }

        private bool IsCheckGameObjectExist()
        {
            if (simplyView is null)
                throw new System.ArgumentNullException("View is empty!");

            if (controller is null)
                throw new System.ArgumentNullException("Controller is empty!");

            return true;
        }


    }
}
