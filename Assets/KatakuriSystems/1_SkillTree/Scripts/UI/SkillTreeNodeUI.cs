using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Katakuri.SystemsWorkshop.SkillTree1
{
    /// <summary>
    /// Represents a node inside a SkillTree, responsible for holding its own SkillTreeNodeData and the required Nodes to interact with it.
    /// It also holds its own unlocked state.
    /// </summary>
    public class SkillTreeNodeUI : MonoBehaviour
    {
        [SerializeField] private SkillTreeNodeData _nodeData; // The SkillTreeNodeData that this node is controlling
        public SkillTreeNodeData NodeData => _nodeData;
        [SerializeField] private SkillTreeNodeUI[] _requiredNodeList; // The required nodes for this node to be interactable
        public SkillTreeNodeUI[] RequiredNodeList => _requiredNodeList;

        public bool IsRootNode; // Root node does not require any node to be interactable
        public bool IsUnlocked;
        public bool IsUnlockable;

        [Header("UI")]
        [SerializeField] private Button _nodeButton;
        [SerializeField] private Image _nodeStateImage; // Changes according to the node's unlocked state

        [Header("State Colors")]
        [SerializeField] private Color lockedColor = Color.gray;
        [SerializeField] private Color unlockedColor = Color.green;
        
        /// <summary>
        /// Sets what should happen if this node is clicked, for example showing the node's data in the info panel.
        /// </summary>
        /// <param name="onClick"></param>
        public void SetOnClick(Action<SkillTreeNodeUI> onClick)
        {
            _nodeButton.onClick.RemoveAllListeners();
            _nodeButton.onClick.AddListener(() => onClick(this));
        }

        /// <summary>
        /// Sets the node's interactable and unlocked state.
        /// This method is called from outside.
        /// </summary>
        /// <param name="isInteractable">Whether the node is interactable</param>
        /// <param name="isUnlocked">Whether the node is unlocked</param>
        public void SetNode(bool isUnlockable, bool isUnlocked)
        {
            IsUnlockable = isUnlockable;
            IsUnlocked = isUnlocked;

            _nodeStateImage.color = isUnlocked ? unlockedColor : lockedColor;
        }        

        // Draw gizmos to visualize the needed node
        private void OnDrawGizmos() 
        {
            if(_requiredNodeList != null && _requiredNodeList.Length > 0)
            {
                Gizmos.color = Color.yellow;
                foreach(SkillTreeNodeUI requiredNode in _requiredNodeList)
                {
                    if(requiredNode != null)
                    {
                        Gizmos.DrawLine(transform.position, requiredNode.transform.position);
                    }
                }

            }
        }
    }
}
