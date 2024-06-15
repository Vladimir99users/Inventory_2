using Assets.Inventory.Scripts.Helpers.Cells;
using Assets.Inventory.Scripts.Helpers.Items;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.Inventory.Scripts.View
{
    public class UpDownView : View
    {
        [SerializeField] private Transform generalContentTransform;
        [SerializeField] private Transform fastHandContentTransform;
        [SerializeField] private TextMeshProUGUI errorText;
        [SerializeField] private Transform tmpGameObject;

        [SerializeField] private List<Cell> generalCells;
        private ICollection<ItemPrefabs> generalItems;

        [SerializeField] private List<Cell> fastHandCells;
        private ICollection<ItemPrefabs> fastHandItems;
        public override void Initialize(ICollection<Cell> fastHandCells, ICollection<Cell> generalCells)
        {
            this.generalCells = new List<Cell>(generalCells);
            this.fastHandCells = new List<Cell>(fastHandCells);
            DisplayInventory(this.fastHandCells, fastHandContentTransform);
            DisplayInventory(this.generalCells, generalContentTransform);
            tmpGameObject.SetAsLastSibling();
        }

        private void DisplayInventory(ICollection<Cell> cells, Transform parent)
        {
            foreach (var cell in cells)
            {
                cell.transform.SetParent(parent);
                cell.gameObject.transform.localScale = Vector3.one;
            }
        }

        public override void UpdateVisial()
        {
            UpdateGeneralInventory(generalCells);
            UpdateFastHandInventory(fastHandCells);
        }
        public override void UpdateGeneralInventory(ICollection<Cell> cells)
        {
            foreach (var cell in cells)
            {
                if (cell.ItemPrefabs is null)
                    continue;

                var visibilityCell = generalCells.FirstOrDefault(x => x == cell);
                visibilityCell.ItemPrefabs.transform.SetParent(visibilityCell.transform);
                visibilityCell.ItemPrefabs.gameObject.transform.position = visibilityCell.transform.position;
                visibilityCell.ItemPrefabs.gameObject.transform.localScale = Vector3.one;
            }
        }

        public override void UpdateFastHandInventory(ICollection<Cell> cells)
        {
            foreach (var cell in cells)
            {
                if (cell.ItemPrefabs is null)
                    continue;

                var visibilityCell = fastHandCells.FirstOrDefault(x => x == cell);
                visibilityCell.ItemPrefabs.transform.SetParent(visibilityCell.transform);
                visibilityCell.ItemPrefabs.gameObject.transform.position = visibilityCell.transform.position;
                visibilityCell.ItemPrefabs.gameObject.transform.localScale = Vector3.one;
            }
        }

        public override void DisplayText(string str, int time = 1)
        {
            base.DisplayText(str, time);
            errorText.text = str;
            StartCoroutine(HideText(time));
        }

        private IEnumerator HideText(int time)
        {
            errorText.gameObject.SetActive(true);
            yield return new WaitForSeconds(time);
            errorText.gameObject.SetActive(false);
        }

        public override void UpdatePositionCell(Cell currentClickCell)
        {
            currentClickCell.ItemPrefabs.transform.SetParent(tmpGameObject);
        }
    }
}