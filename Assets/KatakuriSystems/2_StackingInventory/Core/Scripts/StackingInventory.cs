using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.StackingInventory1
{
    [System.Serializable]
    public class StackingInventory
    {
        [System.Serializable]
        public class ItemStack
        {
            public int ItemID;
            public int ItemAmount;

            public ItemStack(int itemID, int itemAmount)
            {
                ItemID = itemID;
                ItemAmount = itemAmount;
            }
        }

        [SerializeField] private List<ItemStack> _stack;
        public List<ItemStack> Stack => _stack;
        public Func<int, int> GetMaxItemStack; // Function to get the max amount of stack of an item, should return -1 if item doesn't exist.
        public event Action OnStackUpdated;
        /// <summary>
        /// Adds an item to the inventory. If the item is already inside, add it to the stack with highest amount.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="amount"></param>
        public void AddItem(int ID, int amount = 1)
        {
            int stackCount = _stack.Count((stack) => stack.ItemID == ID);

            if(stackCount == 0)
            {
                // No stack inside the inventory yet
                AddNewItemEntry(ID, amount);
            } else
            {
                // Multiple stacks exist in the inventory
                var itemStackArr = _stack.Where((stack) => stack.ItemID == ID).OrderByDescending((stack) => stack.ItemAmount).ToArray();
                AddAmountToAvailableStack(itemStackArr, ID, amount);
            }

            OnStackUpdated?.Invoke();

            // Adds a new Item Stack entry to inventory
            void AddNewItemEntry(int ID, int amount)
            {
                int maxStack = GetMaxItemStack(ID);
                while(amount > 0)
                {
                    int entryAmount;

                    if(amount >= maxStack)
                    {
                        entryAmount = maxStack;
                    } else
                    {
                        entryAmount = amount;
                    }
                    
                    ItemStack entry = new ItemStack(ID, entryAmount);
                    _stack.Add(entry);

                    amount -= entryAmount;
                }
            }

            // Adds amount to the available stack in the inventory, if amount remains, add new item stack entry
            void AddAmountToAvailableStack(ItemStack[] itemStackArr, int ID, int amount)
            {
                int maxStack = GetMaxItemStack(ID);
                int i = 0;
                while(i < itemStackArr.Length && amount > 0)
                {
                    if(itemStackArr[i].ItemAmount + amount <= maxStack)
                    {
                        itemStackArr[i].ItemAmount += amount;
                        amount = 0;
                    } else
                    {
                        amount -= maxStack - itemStackArr[i].ItemAmount;
                        itemStackArr[i].ItemAmount = maxStack;
                    }
                    i += 1;
                }

                if(amount > 0)
                {
                    AddNewItemEntry(ID, amount);
                }
            }
        }

        public void RemoveItem(int ID, int amount = 1)
        {
            int totalAvailable = _stack.Where((stack) => stack.ItemID == ID).Sum((stack) => stack.ItemAmount);

            if(totalAvailable >= amount)
            {
                var itemStackArr = _stack.Where((stack) => stack.ItemID == ID).OrderByDescending((stack) => stack.ItemAmount).ToArray();
                RemoveAmountFromAvailableStack(itemStackArr, ID, amount);
                OnStackUpdated?.Invoke();
            }

            void RemoveAmountFromAvailableStack(ItemStack[] itemStackArr, int ID, int amount)
            {
                int i = itemStackArr.Length - 1;
                while(i >= 0 && amount > 0)
                {
                    if(itemStackArr[i].ItemAmount > amount)
                    {
                        itemStackArr[i].ItemAmount -= amount;
                        amount = 0;
                    } else
                    {
                        amount -= itemStackArr[i].ItemAmount;
                        itemStackArr[i].ItemAmount = 0;
                        RemoveItemEntry(itemStackArr[i]);
                    }
                    i--;
                }
            }

            void RemoveItemEntry(ItemStack itemStack)
            {
                _stack.Remove(itemStack);
            }
        }

        public void RemoveItemStack(ItemStack itemStack)
        {
            _stack.Remove(itemStack);
        }

        public void RemoveAmountFromItemStack(ItemStack itemStack, int amount)
        {
            if(itemStack.ItemAmount > amount) return;

            if(itemStack.ItemAmount < amount)
            {
                itemStack.ItemAmount -= amount;
            } else
            {
                RemoveItemStack(itemStack);
            }
            OnStackUpdated?.Invoke();

        }
    }

}
