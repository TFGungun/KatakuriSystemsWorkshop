using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Katakuri.SystemsWorkshop.StackingInventory.Example;

namespace Katakuri.SystemsWorkshop.StackingInventory.Example1
{
    public class LootPanelUI : MonoBehaviour
    {
        [SerializeField] private ItemInventory _inventory;
        [SerializeField] private ItemDatabase _itemDatabase;

        [Header("UI")]
        [SerializeField] private ItemDisplayUI _itemDisplayPrefab;
        [SerializeField] private List<ItemDisplayUI> _instantiatedItemDisplay;
        [SerializeField] private RectTransform _itemDisplayParent;

        [SerializeField] private List<StackingInventory.ItemStack> _currentLootList;

        private void Awake() 
        {
            _instantiatedItemDisplay = new List<ItemDisplayUI>();
        }

        public void ShowLoot(List<StackingInventory.ItemStack> itemStackList)
        {
            _currentLootList = itemStackList;
            UpdateLootPanel();
        }

        private void UpdateLootPanel()
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

            foreach(StackingInventory.ItemStack itemStack in _currentLootList)
            {
                ItemDisplayUI itemDisplay = Instantiate(_itemDisplayPrefab, _itemDisplayParent);
                itemDisplay.SetItemDisplay(_itemDatabase.GetItemSprite(itemStack.ItemID), itemStack.ItemAmount, () => OnClickItemDisplay(itemStack));
                _instantiatedItemDisplay.Add(itemDisplay);
            }

            void OnClickItemDisplay(StackingInventory.ItemStack itemStack)
            {
                _currentLootList.Remove(itemStack);
                _inventory.Content.AddItem(itemStack.ItemID, itemStack.ItemAmount);
                UpdateLootPanel();
            }
        }
    }

}
