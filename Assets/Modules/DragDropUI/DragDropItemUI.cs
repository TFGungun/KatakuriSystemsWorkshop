using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Katakuri.Modules.DragDrop
{
	/// <summary>
    /// This class defines the behaviour of Draggable Item for Drag Drop UI module.
    /// Object with this component has the capability of instantiating a <see cref="DragDropIconUI"/> and moving them around.
    /// </summary>
	public class DragDropItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		public int Identifier;
        public bool IsDraggable = true;
		[SerializeField] protected Canvas canvas;
		[SerializeField] protected RectTransform canvasRectTransform;
        [SerializeField] protected DragDropIconUI draggableIconTemplate;
	    protected Dictionary<int,DragDropIconUI> draggingIcon = new Dictionary<int, DragDropIconUI>();

		[SerializeField] protected UnityEvent OnStartDrag;
        [SerializeField] protected UnityEvent OnFinishDrag;

        protected DragDropSlotUI currentSlot;

        protected int currentPointerID;
		private readonly List<RaycastResult> raycastResult = new List<RaycastResult>();
		protected virtual void Awake() 
		{
            currentSlot = null;
        }

        protected virtual void Start()
        {
			this.canvas = FindInParents<Canvas>(gameObject);
			this.canvasRectTransform = this.canvas.GetComponent<RectTransform>();
            currentPointerID = -1;
        }

		public virtual void OnBeginDrag(PointerEventData eventData)
		{
            if (canvas == null) return;
            if(!IsDraggable) return;
            // if (currentSlot != null)
            // {
            //     currentSlot.UnfillSlot(this);
            //     currentSlot = null;
            // }

            currentPointerID = eventData.pointerId;
            this.draggingIcon[eventData.pointerId] = Instantiate(draggableIconTemplate, canvas.transform);
            this.draggingIcon[eventData.pointerId].SetDragDropItem(this);
            this.draggingIcon[eventData.pointerId].transform.SetAsLastSibling();
            
            this.draggingIcon[eventData.pointerId].CanvasGroup.enabled = true;
            this.draggingIcon[eventData.pointerId].CanvasGroup.blocksRaycasts = false;

            this.draggingIcon[eventData.pointerId].gameObject.SetActive(true);

            SetDraggedPosition(eventData);
        }

		public void OnDrag(PointerEventData eventData)
		{
            if(!IsDraggable) return;
            SetDraggedPosition(eventData);
		}

		private void SetDraggedPosition(PointerEventData eventData)
		{			
			Vector3 globalMousePos;
			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
			{
				this.draggingIcon[eventData.pointerId].transform.position = globalMousePos;
			}
		}

		public virtual void OnEndDrag(PointerEventData eventData)
		{
            if(!IsDraggable) return;
            if (draggingIcon[eventData.pointerId] != null)
			{
                Destroy(draggingIcon[eventData.pointerId].gameObject);
                draggingIcon[eventData.pointerId] = null;
            	currentPointerID = -1;
            }

            var droppedSlot = GetDroppableSlot(eventData);
            if(droppedSlot != null)
            {
                DragDropUIEvent.OnItemDroppedOnSlot(this, droppedSlot);
            } else
            {
                DragDropUIEvent.OnItemDroppedOffSlot(this);
            }

            // var droppedSlot = GetDroppableSlot<DragDropSlotUI>(eventData);

            // if(droppedSlot != null)
            // {
            //     currentSlot = droppedSlot;
            //     droppedSlot.FillSlot(this);
            // }

            // this.draggingIcon[eventData.pointerId].GetComponent<CanvasGroup>().enabled = false;
            // this.draggingIcon[eventData.pointerId].GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

		static public T FindInParents<T>(GameObject go) where T : Component
		{
			if (go == null) return null;
			var comp = go.GetComponent<T>();

			if (comp != null)
				return comp;
			
			var t = go.transform.parent;
			while (t != null && comp == null)
			{
				comp = t.gameObject.GetComponent<T>();
				t = t.parent;
			}
			return comp;
		}

        protected DragDropSlotUI GetDroppableSlot(PointerEventData data)
        {
            EventSystem.current.RaycastAll(data, raycastResult);

            DragDropSlotUI raycastedSlot = null;

            for (int i = 0; i < raycastResult.Count; i++)
            {
                raycastedSlot = raycastResult[i].gameObject.GetComponent<DragDropSlotUI>();
                if (raycastedSlot != null)
                {
                    break;
                }
            }

			return raycastedSlot;
        }

		protected T GetDroppableSlot<T>(PointerEventData data) where T : DragDropSlotUI
		{
            EventSystem.current.RaycastAll(data, raycastResult);

            T raycastedSlot = null;

            for (int i = 0; i < raycastResult.Count; i++)
            {
                raycastedSlot = raycastResult[i].gameObject.GetComponent<T>();
                if (raycastedSlot != null)
                {
                    break;
                }
            }

			return raycastedSlot;
		}
	}

}
