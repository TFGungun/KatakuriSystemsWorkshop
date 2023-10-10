using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.StackingInventory.Example
{
    public class ItemInventory : MonoBehaviour
    {
        [SerializeField] private StackingInventory _content;
        public StackingInventory Content => _content;
    }
}

