using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Katakuri.Modules.DragDrop
{
    /// <summary>
    /// This class defines the behaviour of Draggable Icon for Drag Drop UI module.
    /// This object will be instantiated when <see cref="DragDropItemUI"/> starts dragging.
    /// </summary>
    public class DragDropIconUI : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup canvasGroup;
        public CanvasGroup CanvasGroup => canvasGroup;
        protected DragDropItemUI dragDropItem;
        public DragDropItemUI DragDropItem => dragDropItem;

        public virtual void SetDragDropItem(DragDropItemUI dragDropItem)
        {
            this.dragDropItem = dragDropItem;
        }
    }
}
