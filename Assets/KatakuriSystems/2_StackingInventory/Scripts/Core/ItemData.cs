using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.StackingInventory1
{
    [CreateAssetMenu(menuName = "Katakuri/StackingInventory1/ItemData")]
    public class ItemData : ScriptableObject
    {
        public int ItemID;
        public string ItemName;
        public string ItemDescription;
        public Sprite ItemSprite;
        public int MaxStack = 1;
        public ItemData ConvertResult;
    }

}

