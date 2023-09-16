using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.Modules.DragDrop
{
    public class DragDropUIEvent
    {
        public static event Action<DragDropItemUI, DragDropSlotUI> ItemDroppedOnSlotEvent;
        public static event Action<DragDropItemUI> ItemDroppedOffSlotEvent;
        public static event Action<DragDropItemUI, DragDropSlotUI> ItemDraggedOffSlotEvent;

        public static void OnItemDroppedOnSlot(DragDropItemUI item, DragDropSlotUI slot)
        {
            ItemDroppedOnSlotEvent?.Invoke(item, slot);
        }

        public static void OnItemDroppedOffSlot(DragDropItemUI item)
        {
            ItemDroppedOffSlotEvent?.Invoke(item);
        }

        public static void OnItemDraggedOffSlot(DragDropItemUI item, DragDropSlotUI slot)
        {
            ItemDraggedOffSlotEvent?.Invoke(item, slot);
        }
    }

}
