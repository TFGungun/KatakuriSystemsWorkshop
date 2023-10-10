using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Katakuri.Modules.DragDrop;
using Katakuri.SystemsWorkshop.StackingInventory1.Example;

namespace Katakuri.SystemsWorkshop.StackingInventory1.Example2
{
    public class InventoryDisplayUI : MonoBehaviour
    {
        [SerializeField] private StackingInventory _inventory;
        [SerializeField] private List<InventorySlotUI> _availableSlotList;
        [SerializeField] private ItemDatabase _itemDatabase;

        [SerializeField] private InventorySlotUI _convertSlot;
        [SerializeField] private InventorySlotUI _resultSlot;

        [SerializeField] private Button _convertButton;

        private void OnEnable() 
        {
            DragDropUIEvent.ItemDroppedOnSlotEvent += OnItemDroppedOnSlot;
            DragDropUIEvent.ItemDroppedOffSlotEvent += OnItemDroppedOffSlot;
        }

        private void OnDisable() 
        {
            DragDropUIEvent.ItemDroppedOnSlotEvent -= OnItemDroppedOnSlot;
            DragDropUIEvent.ItemDroppedOffSlotEvent -= OnItemDroppedOffSlot;
        }

        private void Start() 
        {
            InitializeInventoryDisplay();
            _convertSlot.FillSlot(null, null);
            _resultSlot.FillSlot(null, null);
        }

        private void InitializeInventoryDisplay()
        {
            _inventory.GetMaxItemStack = _itemDatabase.GetItemMaxStack;

            for (int i = 0; i < _availableSlotList.Count; i++)
            {
                _availableSlotList[i].SetInventoryDisplay(this);
                if(i < _inventory.InventoryContent.Count)
                {
                    Sprite itemSprite = _itemDatabase.GetItemSprite(_inventory.InventoryContent[i].ItemID);
                    _availableSlotList[i].FillSlot(_inventory.InventoryContent[i], itemSprite);
                } else
                {
                    _availableSlotList[i].FillSlot(null, null);
                }
            }
        }

        private void OnItemDroppedOnSlot(DragDropItemUI item, DragDropSlotUI slot)
        {
            InventoryItemUI inventoryItem = item.GetComponent<InventoryItemUI>();
            if(inventoryItem == null) return;

            InventorySlotUI inventorySlot = slot.GetComponent<InventorySlotUI>();
            if(inventorySlot == null) return;

            FillSlot(inventoryItem, inventorySlot);

        }

        private void OnItemDroppedOffSlot(DragDropItemUI item)
        {
            InventoryItemUI inventoryItem = item.GetComponent<InventoryItemUI>();
            if(inventoryItem == null) return;

            StackingInventory.ItemStack itemStack = inventoryItem.ItemStack;
            _inventory.RemoveItemStack(itemStack);

            inventoryItem.Slot.FillSlot(null, null);
        }

        public void FillSlot(InventoryItemUI item, InventorySlotUI slot)
        {
            if(slot.Item.ItemStack == null)
            {
                SwapSlot(item.Slot, slot);
            } else if(slot.Item.ItemStack.ItemID != item.ItemStack.ItemID)
            {
                SwapSlot(item.Slot, slot);
            } else if(slot.Item.ItemStack.ItemID == item.ItemStack.ItemID)
            {
                int maxStack = _itemDatabase.GetItemMaxStack(item.ItemStack.ItemID);
                if(slot.Item.ItemStack.ItemAmount >= maxStack)
                {
                    SwapSlot(item.Slot, slot);
                } else
                {
                    int itemAmount = item.ItemStack.ItemAmount;

                    slot.Item.ItemStack.ItemAmount += itemAmount;

                    if(slot.Item.ItemStack.ItemAmount > maxStack)
                    {
                        itemAmount = slot.Item.ItemStack.ItemAmount - maxStack;
                        slot.Item.ItemStack.ItemAmount = maxStack;
                    } else
                    {
                        itemAmount = 0;
                    }

                    Sprite sprite = _itemDatabase.GetItemSprite(item.ItemStack.ItemID);
                    if(itemAmount > 0)
                    {
                        item.ItemStack.ItemAmount = itemAmount;


                        item.Slot.FillSlot(item.ItemStack, sprite);
                        slot.FillSlot(slot.Item.ItemStack, sprite);
                    } else
                    {
                        _inventory.RemoveItemStack(item.ItemStack);
                        item.Slot.FillSlot(null, null);
                        slot.FillSlot(slot.Item.ItemStack, sprite);
                    }
                }
            }
            
        }

        public void SwapSlot(InventorySlotUI oldSlot, InventorySlotUI newSlot)
        {
            StackingInventory.ItemStack oldStack = oldSlot.Item.ItemStack;
            StackingInventory.ItemStack newStack = newSlot.Item.ItemStack;
            
            Sprite oldItemSprite = oldStack != null ? _itemDatabase.GetItemSprite(oldStack.ItemID) : null;
            Sprite newItemSprite = newStack != null ? _itemDatabase.GetItemSprite(newStack.ItemID) : null;

            oldSlot.FillSlot(newStack, newItemSprite);
            newSlot.FillSlot(oldStack, oldItemSprite);
        }

        public void OnFillConvertSlot(InventorySlotUI slot)
        {
            _convertButton.interactable = slot.Item.ItemStack != null;
        }

        public void ConvertItem()
        {
            if(_convertSlot.Item.ItemStack != null)
            {
                ItemData itemData = _itemDatabase.GetItemData(_convertSlot.Item.ItemStack.ItemID);
                StackingInventory.ItemStack newItemStack = new StackingInventory.ItemStack(itemData.ConvertResult.ItemID, _convertSlot.Item.ItemStack.ItemAmount);

                _resultSlot.FillSlot(newItemStack, itemData.ConvertResult.ItemSprite);
                _convertSlot.FillSlot(null, null);

            }
        }

        public void SplitItem(InventoryItemUI item)
        {
            if(item.ItemStack == null) return;
            if(item.ItemStack.ItemAmount <= 1) return;

            InventorySlotUI availableSlot = GetAvailableSlot();
            if(availableSlot == null) return;

            int splitAmount = item.ItemStack.ItemAmount / 2;
            int remainAmount = item.ItemStack.ItemAmount - splitAmount;

            item.ItemStack.ItemAmount = remainAmount;

            StackingInventory.ItemStack splitItemStack = new StackingInventory.ItemStack(item.ItemStack.ItemID, splitAmount);

            Sprite sprite = _itemDatabase.GetItemSprite(item.ItemStack.ItemID);

            item.Slot.FillSlot(item.ItemStack, sprite);
            availableSlot.FillSlot(splitItemStack, sprite);
        }

        private InventorySlotUI GetAvailableSlot()
        {
            int index = -1;
            for (int i = 0; i < _availableSlotList.Count; i++)
            {
                if(_availableSlotList[i].Item.ItemStack == null)
                {
                    index = i;
                    break;
                }
            }

            if(index != -1) return _availableSlotList[index];
            else return null;
        }
    }

}
