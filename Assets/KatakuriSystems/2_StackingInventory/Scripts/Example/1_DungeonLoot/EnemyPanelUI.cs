using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.StackingInventory1.Example1
{
    public class EnemyPanelUI : MonoBehaviour
    {
        [SerializeField] private LootPanelUI _lootPanel;
        [SerializeField] private List<EnemyController> _randomEnemyList;
        [SerializeField] private int _maxEnemy = 3;

        [Header("UI")]
        [SerializeField] private List<EnemyController> _instantiatedEnemy;
        [SerializeField] private RectTransform _enemyParent;

        private void Awake() 
        {
            _instantiatedEnemy = new List<EnemyController>();
        }

        private void Start() 
        {
            FillEnemyPanel();
        }

        public void FillEnemyPanel()
        {
            int remain = _maxEnemy - _instantiatedEnemy.Count;
            if(remain > 0)
            {
                for (int i = 0; i < remain; i++)
                {
                    EnemyController enemy = Instantiate(_randomEnemyList[Random.Range(0, _randomEnemyList.Count)], _enemyParent);
                    enemy.SetOnDrop(OnSlayEnemy);
                    _instantiatedEnemy.Add(enemy);  
                }
                    
            }
        }

        public void OnSlayEnemy(EnemyController enemy, List<StackingInventory.ItemStack> dropList)
        {
            _lootPanel.ShowLoot(dropList);

            _instantiatedEnemy.Remove(enemy);
            Destroy(enemy.gameObject);
            FillEnemyPanel();
        }

    }
}
