using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.StackingInventory1.Example1
{
    [CreateAssetMenu(menuName = "Katakuri/StackingInventory1/Example1/EnemyDropData")]
    public class EnemyDropData : ScriptableObject
    {
        [System.Serializable]
        public class DropData
        {
            public ItemData Item;
            public float Probability;
            public int MinAmount;
            public int MaxAmount;
        }

        public List<DropData> DropList;
    }

}
