using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.StackingInventory.Example1
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyDropData _enemyDropData;
        private System.Action<EnemyController, List<StackingInventory.ItemStack>> _onDrop;

        public void SetOnDrop(System.Action<EnemyController, List<StackingInventory.ItemStack>> OnDrop)
        {
            _onDrop = OnDrop;
        }

        /// <summary>
        /// Called from button to kill enemy
        /// </summary>
        public void SlayEnemy()
        {
            List<StackingInventory.ItemStack> enemyDropList = new List<StackingInventory.ItemStack>();
            foreach(EnemyDropData.DropData dropData in _enemyDropData.DropList)
            {
                float rand = Random.value;
                if(rand > dropData.Probability)
                {
                    StackingInventory.ItemStack enemyDrop = new StackingInventory.ItemStack(dropData.Item.ItemID, Random.Range(dropData.MinAmount, dropData.MaxAmount + 1));
                    enemyDropList.Add(enemyDrop);
                }
            }
            _onDrop?.Invoke(this, enemyDropList);
        }
    }

}
