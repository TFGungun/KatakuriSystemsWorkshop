using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Katakuri.Modules.DragDrop
{
	/// <summary>
    /// This class defines the behaviour of Drag Drop Slot for Drag Drop UI module.
    /// Object with this component has the capability of detecting a "Drop" Input Action to see if any <see cref="DragDropItemUI"/> is dragged.
    /// </summary>
	public class DragDropSlotUI : MonoBehaviour, IDropHandler
	{
		public int Identifier;
        protected DragDropItemUI currentItem;
        [SerializeField] protected UnityEvent OnDetectDrop;
		
		public virtual void OnDrop(PointerEventData data)
		{
			DragDropItemUI droppedItem = GetDroppedItem<DragDropItemUI>(data);
            OnDetectDrop?.Invoke();
        }

		public virtual void FillSlot(DragDropItemUI item)
		{
            currentItem = item;
        }

		public virtual void UnfillSlot(DragDropItemUI item)
		{
			if(currentItem == item)
			{
                currentItem = null;
            }
		}

        protected T GetDroppedItem<T>(PointerEventData data) where T : DragDropItemUI
		{
			var originalObj = data.pointerDrag;
			if (originalObj == null)
				return null;
			
			var drag = originalObj.GetComponent<T>();
			if (drag == null)
				return null;

			return drag;
		}

	}

}
