using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Katakuri.SystemsWorkshop.SkillTree1
{
    public class SkillDisplayUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private SkillHolder _skillHolder;
        [SerializeField] private SkillDatabase _skillDatabase;
        [SerializeField] private SkillTreeUI _skillTreeUI;

        [Header("UI")]
        [SerializeField] private RectTransform _skillDisplayContainer;
        [SerializeField] private TMP_Text _skillDisplayPrefab;
        private List<TMP_Text> _instantiatedSkillList = new List<TMP_Text>();

        private void OnEnable() 
        {
            _skillTreeUI.OnUpdateSkillTree += OnUpdateSkillTree;    
        }

        private void OnDisable() 
        {
            _skillTreeUI.OnUpdateSkillTree -= OnUpdateSkillTree;    
        }

        private void OnUpdateSkillTree()
        {
            ClearInstantiatedSkill();

            foreach(int skillID in _skillHolder.AcquiredSkillList)
            {
                SkillData skillData = _skillDatabase.GetSkillData(skillID);
                if(skillData != null)
                {
                    int skillLevel = _skillHolder.SkillLevelDictionary[skillID];
                    
                    TMP_Text skillDisplay = Instantiate(_skillDisplayPrefab, _skillDisplayContainer);
                    skillDisplay.text = string.Format($"{skillData.SkillName} Lv. {skillLevel}");
                    skillDisplay.gameObject.SetActive(true);

                    _instantiatedSkillList.Add(skillDisplay);
                }
            }

            void ClearInstantiatedSkill()
            {
                foreach(TMP_Text instantiatedSkill in _instantiatedSkillList)
                {
                    Destroy(instantiatedSkill.gameObject);
                }

                _instantiatedSkillList.Clear();
            }
        }
    }

}
