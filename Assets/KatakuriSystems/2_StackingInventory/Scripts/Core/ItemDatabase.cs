using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Katakuri.SystemsWorkshop.StackingInventory1
{
    public class ItemDatabase : MonoBehaviour
    {
        [SerializeField] private List<ItemData> _availableItemData;
        
        public ItemData GetItemData(int ID)
        {
            return _availableItemData.Find((itemData) => itemData.ItemID == ID);
        }

        /// <summary>
        /// Search and return the max stack of an Item with its ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>The max stack of the item, if found; otherwise, -1.</returns>
        public int GetItemMaxStack(int ID)
        {
            ItemData itemData = GetItemData(ID);
            if(itemData != null)
            {
                return itemData.MaxStack;
            } else
            {
                return -1;
            }
        }

        public Sprite GetItemSprite(int ID)
        {
            ItemData itemData = GetItemData(ID);
            if(itemData != null)
            {
                return itemData.ItemSprite;
            } else
            {
                return null;
            }
        }
    }

}
