using Assets.Inventory.Scripts.Controller;
using Assets.Inventory.Scripts.Model;
using UnityEngine;

namespace Inventory
{
    public class Bootstrap : MonoBehaviour
    {
        [Header("Helper attributes")]
        [SerializeField] private Transform parentViewTransform;
        [Header("Simply View component")]
        [SerializeField] private View simplyView;
        [Header("Controller")]
        [SerializeField] private Controller controller;
        [Header("Model")]
        [SerializeField] private Model model;


        private void Awake()
        {
            CheckFileExist();

            var viewPrefabs = Instantiate(simplyView, parentViewTransform);
            viewPrefabs.transform.SetParent(parentViewTransform);
            model = new SimplyModel(viewPrefabs);
            controller = new PointerClickController(model, viewPrefabs);
        }

        private void CheckFileExist()
        {
            if (simplyView is null)
                throw new System.ArgumentNullException("View is empty!");
        }


    }
}
