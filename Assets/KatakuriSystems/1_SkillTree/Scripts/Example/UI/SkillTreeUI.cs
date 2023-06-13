using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Katakuri.SystemsWorkshop.SkillTree1
{
    /// <summary>
    /// The UI that controls player's interaction with the game's Skill Trees.
    /// This UI can be represented in any way based on your game.
    /// </summary>
    public class SkillTreeUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private SkillHolder _skillHolder;
        [SerializeField] private SkillTreeSaveData _treeSaveData;
        [SerializeField] private SkillDatabase _skillDatabase;
        [SerializeField] private SkillTree _skillTree;
        [SerializeField] private SkillPointHolder _skillPointHolder;
        
        [Header("Info Panel UI")]
        [SerializeField] private TMP_Text _nodeNameText;
        [SerializeField] private TMP_Text _nodeDescriptionText;
        [SerializeField] private TMP_Text _requiredCurrencyText;
        [SerializeField] private TMP_Text _skillDescriptionTitleText;
        [SerializeField] private TMP_Text _skillDescriptionText;
        [SerializeField] private Button _unlockNodeButton;

        private SkillTreeNode _currentActiveNode;

        public event Action OnUpdateSkillTree;

        private void Awake() 
        {
            _unlockNodeButton.onClick.RemoveAllListeners();
            _unlockNodeButton.onClick.AddListener(OnClickUnlock);
        }

        public void SetCurrentActiveTree(SkillTree skillTree)
        {
            if(_skillTree != null)
            {
                _skillTree.gameObject.SetActive(false);
            }

            _skillTree = skillTree;
            _skillTree.gameObject.SetActive(true);

            InitializeSkilTree();
        }

        public void InitializeSkilTree()
        {
            _skillTree.CheckNodeUnlocked = IsNodeUnlocked;
            _skillTree.OnClickNode = OnClickNode;
            _skillTree.RefreshSkillTree();
            OnClickNode(null);
        }

        private bool IsNodeUnlocked(SkillTreeNodeData nodeData)
        {
            // Access Save System to check if the node is unlocked
            return _treeSaveData.IsNodeUnlocked(nodeData.NodeKey);
        }

        private void OnClickNode(SkillTreeNode node)
        {
            _currentActiveNode = node;

            _nodeNameText.text = _currentActiveNode != null ? _currentActiveNode.NodeData.NodeName : string.Empty;
            _nodeDescriptionText.text = _currentActiveNode != null ? _currentActiveNode.NodeData.NodeDescription : string.Empty;
            _requiredCurrencyText.text = _currentActiveNode != null ? _currentActiveNode.NodeData.RequiredSP.ToString() : string.Empty;

            _skillDescriptionTitleText.gameObject.SetActive(_currentActiveNode != null);
            _skillDescriptionText.text = _currentActiveNode != null ? _skillDatabase.GetSkillDescription(_currentActiveNode.NodeData.Skill.SkillID) : string.Empty;

            _unlockNodeButton.interactable = _currentActiveNode != null 
                && !_currentActiveNode.IsUnlocked 
                && _currentActiveNode.IsUnlockable
                && _skillPointHolder.SkillPoint >= _currentActiveNode.NodeData.RequiredSP; // Only spendable when the node is not unlocked
        }

        private void OnClickUnlock()
        {
            if(_currentActiveNode != null)
            {
                _skillPointHolder.SkillPoint -= _currentActiveNode.NodeData.RequiredSP;
                _currentActiveNode.NodeData.ExecuteNodeEffect(_skillHolder);
                _currentActiveNode.IsUnlocked = true;
                _treeSaveData.SetUnlockNode(_currentActiveNode.NodeData.NodeKey, true);
                _skillTree.RefreshSkillTree();
                OnClickNode(_currentActiveNode);

                OnUpdateSkillTree?.Invoke();
            }
        }


    }

}
