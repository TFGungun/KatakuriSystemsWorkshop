using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.StackingInventory1.Example1
{
    public class InventoryPanelUI : MonoBehaviour
    {
        [SerializeField] private StackingInventory _inventory;
        [SerializeField] private ItemDatabase _itemDatabase;

        [Header("UI")]
        [SerializeField] private ItemDisplayUI _itemDisplayPrefab;
        [SerializeField] private List<ItemDisplayUI> _instantiatedItemDisplay;
        [SerializeField] private RectTransform _itemDisplayParent;

        private void OnEnable() 
        {
            _inventory.OnInventoryUpdated += UpdateInventoryPanel;
        }

        private void OnDestroy() 
        {
            _inventory.OnInventoryUpdated -= UpdateInventoryPanel;
        }

        private void Awake() 
        {
            _instantiatedItemDisplay = new List<ItemDisplayUI>();
            _inventory.GetMaxItemStack = _itemDatabase.GetItemMaxStack;
        }

        private void UpdateInventoryPanel()
        {
            if(_instantiatedItemDisplay == null) _instantiatedItemDisplay = new List<ItemDisplayUI>();
            if(_instantiatedItemDisplay.Count > 0)
            {
                foreach(ItemDisplayUI itemDisplay in _instantiatedItemDisplay)
                {
                    Destroy(itemDisplay.gameObject);
                }

                _instantiatedItemDisplay.Clear();
            }

            foreach(StackingInventory.ItemStack itemStack in _inventory.InventoryContent)
            {
                ItemDisplayUI itemDisplay = Instantiate(_itemDisplayPrefab, _itemDisplayParent);
                itemDisplay.SetItemDisplay(_itemDatabase.GetItemSprite(itemStack.ItemID), itemStack.ItemAmount, () => OnClickItemDisplay(itemStack));
                _instantiatedItemDisplay.Add(itemDisplay);
            }

            void OnClickItemDisplay(StackingInventory.ItemStack itemStack)
            {
                _inventory.RemoveItem(itemStack.ItemID, 1);
            }
        }
    }

}
