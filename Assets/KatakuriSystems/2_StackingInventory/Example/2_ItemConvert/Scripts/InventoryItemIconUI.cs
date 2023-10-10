using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Katakuri.Modules.DragDrop;
using UnityEngine.UI;

namespace Katakuri.SystemsWorkshop.StackingInventory.Example2
{
    public class InventoryItemIconUI : DragDropIconUI
    {
        [SerializeField] private Image _itemRenderer;

        public void SetInformation(Sprite itemSprite)
        {
            _itemRenderer.sprite = itemSprite;
        }


    }

}
