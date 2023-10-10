using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Katakuri.Modules.DragDrop;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace Katakuri.SystemsWorkshop.StackingInventory1.Example2
{
    public class InventoryItemUI : DragDropItemUI, IPointerClickHandler
    {
        private StackingInventory.ItemStack _itemStack;
        public StackingInventory.ItemStack ItemStack => _itemStack;

        [SerializeField] private Image _itemRenderer;

        [SerializeField] private InventorySlotUI _slot;
        public InventorySlotUI Slot => _slot;

        private InventoryDisplayUI _inventoryDisplayUI;

        public void SetInventoryDisplay(InventoryDisplayUI inventoryDisplayUI)
        {
            _inventoryDisplayUI = inventoryDisplayUI;
        }


        public void SetItemStack(StackingInventory.ItemStack itemStack, Sprite itemSprite)
        {
            _itemStack = itemStack;
            _itemRenderer.sprite = itemSprite;
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            if(!IsDraggable) return;
            base.OnBeginDrag(eventData);
            if(_slot != null)
            {
                DragDropUIEvent.OnItemDraggedOffSlot(this, _slot);
            }

            this.draggingIcon[eventData.pointerId].GetComponent<InventoryItemIconUI>().SetInformation(_itemRenderer.sprite);
            OnStartDrag?.Invoke();
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            if(!IsDraggable) return;
            base.OnEndDrag(eventData);
            OnFinishDrag?.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.button == PointerEventData.InputButton.Right)
            {
                if(_inventoryDisplayUI != null) _inventoryDisplayUI.SplitItem(this);
            }
        }




    }

}
