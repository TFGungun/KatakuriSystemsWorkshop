using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Katakuri.SystemsWorkshop.SkillTree1
{
    public class SkillTreeTabUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private SkillTreeUI _skillTreeUI;
        [SerializeField] private SkillTree _defaultSkillTree;

        [Header("UI")]
        [SerializeField] private SkillTreeTab[] _tabList;

        private void Awake()
        {
            foreach(SkillTreeTab tab in _tabList)
            {
                InitializeTab(tab);
            }
        }

        private void InitializeTab(SkillTreeTab tab)
        {
            tab.TabButton.onClick.RemoveAllListeners();
            tab.TabButton.onClick.AddListener(() => SwitchSkillTree(tab.SkillTree));
        }

        private void Start() 
        {
            if(_defaultSkillTree != null)
            {
                SwitchSkillTree(_defaultSkillTree);
            }    
        }

        private void SwitchSkillTree(SkillTree skillTree)
        {
            _skillTreeUI.SetCurrentActiveTree(skillTree);
        }


        [System.Serializable]
        public class SkillTreeTab
        {
            public Button TabButton;
            public SkillTree SkillTree;
        }
    }

}
