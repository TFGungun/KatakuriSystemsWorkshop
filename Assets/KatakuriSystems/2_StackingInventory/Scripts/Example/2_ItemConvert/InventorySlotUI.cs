using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Katakuri.Modules.DragDrop;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;

namespace Katakuri.SystemsWorkshop.StackingInventory1.Example2
{
    public class InventorySlotUI : DragDropSlotUI
    {
        [SerializeField] private InventoryItemUI _item;
        public InventoryItemUI Item => _item;
        [SerializeField] private TMP_Text _itemAmountContainer;

        [SerializeField] private UnityEvent<InventorySlotUI> onFillSlot;

        private InventoryDisplayUI _inventoryDisplayUI;

        public void SetInventoryDisplay(InventoryDisplayUI inventoryDisplayUI)
        {
            _inventoryDisplayUI = inventoryDisplayUI;
            _item.SetInventoryDisplay(inventoryDisplayUI);
        }


        public void FillSlot(StackingInventory.ItemStack itemStack, Sprite itemSprite)
        {
            if(itemStack != null)
            {
                _itemAmountContainer.text = itemStack.ItemAmount.ToString();
            }
            _item.SetItemStack(itemStack, itemSprite);
            _item.gameObject.SetActive(itemStack != null);
            _itemAmountContainer.gameObject.SetActive(itemStack != null);
            onFillSlot?.Invoke(this);
        }

        public override void OnDrop(PointerEventData data)
        {
            var previousItem = GetDroppedItem<InventoryItemUI>(data);
            OnDetectDrop?.Invoke();
        }
    }

}
