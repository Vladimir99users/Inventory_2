using Assets.Inventory.Scripts.Helpers.Cells;
using System.Collections;
using System.Collections.Generic;
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

        [SerializeField] private IEnumerable<Cell> generalCells;
        [SerializeField] private IEnumerable<Cell> fastHandCells;
        public override void Initialize(IEnumerable<Cell> fastHandCells, IEnumerable<Cell> generalCells)
        {
            this.generalCells = generalCells;
            this.fastHandCells = fastHandCells;
            DisplayInventory(this.fastHandCells, fastHandContentTransform);
            DisplayInventory(this.generalCells, generalContentTransform);
            tmpGameObject.SetAsLastSibling();
        }

        private void DisplayInventory(IEnumerable<Cell> cells, Transform parent)
        {
            foreach (var cell in cells)
            {
                cell.transform.SetParent(parent);
                cell.gameObject.transform.localScale = Vector3.one;
            }
        }

        public override void UpdateVisual()
        {
            UpdateGeneralInventory();
            UpdateFastHandInventory();
        }
        public override void UpdateGeneralInventory()
        {
            foreach (var cell in generalCells)
            {
                if (cell.ItemPrefabs is null)
                    continue;
                TransformCell(cell);
            }
        }
        public override void UpdateFastHandInventory()
        {
            foreach (var cell in fastHandCells)
            {
                if (cell.ItemPrefabs is null)
                    continue;
                TransformCell(cell);
            }
        }
        private void TransformCell(Cell cell)
        {
            cell.ItemPrefabs.transform.SetParent(cell.transform);
            cell.ItemPrefabs.gameObject.transform.position = cell.transform.position;
            cell.ItemPrefabs.gameObject.transform.localScale = Vector3.one;
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