using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Katakuri.SystemsWorkshop.SkillTree1
{
    public class SkillPointUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private SkillPointHolder _skillPointHolder;
        [SerializeField] private SkillTreeUI _skillTreeUI;

        [Header("UI")]
        [SerializeField] private TMP_Text _skillPointText;

        private void OnEnable() 
        {
            _skillTreeUI.OnUpdateSkillTree += OnUpdateSkillTree;    
        }

        private void OnDisable() 
        {
            _skillTreeUI.OnUpdateSkillTree -= OnUpdateSkillTree;    
        }

        // Start is called before the first frame update
        void Start()
        {
            UpdateSkillPoint();
        }

        private void OnUpdateSkillTree()
        {
            UpdateSkillPoint();
        }

        private void UpdateSkillPoint()
        {
            _skillPointText.text = string.Format($"Skill Point : {_skillPointHolder.SkillPoint.ToString()}");
        }

        [ContextMenu("Add 1 Skill Point")]
        public void Add1SkillPoint()
        {
            _skillPointHolder.SkillPoint += 1;
            UpdateSkillPoint();
        }
    }

}
