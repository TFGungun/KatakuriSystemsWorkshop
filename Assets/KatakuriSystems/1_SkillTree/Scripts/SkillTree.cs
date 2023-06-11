using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.SkillTree1
{
    /// <summary>
    /// Represents a skill tree that is already filled with Nodes.
    /// This object is intended to only be responsible for containing the nodes and showing their respective interactable & unlocked states.
    /// </summary>
    public class SkillTree : MonoBehaviour
    {
        [SerializeField] private SkillTreeNodeUI[] _treeNodeList;
        public SkillTreeNodeUI[] TreeNodeList => _treeNodeList;

        public Func<SkillTreeNodeData, bool> CheckNodeUnlocked; // Function to check if a node has been unlocked (usually in SaveData)
        public Action<SkillTreeNodeUI> OnClickNode; // Action to execute when a node is clicked

        /// <summary>
        /// Refreshes the nodes' interactable and unlocked states in the skill tree.
        /// </summary>
        public void RefreshSkillTree()
        {
            foreach(SkillTreeNodeUI node in _treeNodeList)
            {
                node.SetNode(
                    isUnlockable: node.IsRootNode || CheckNodeFulfilRequirements(node),
                    isUnlocked: IsNodeUnlocked(node.NodeData)
                    );
                node.SetOnClick(OnClickNode);
            }
        }

        /// <summary>
        /// Checks and returns if a node is interactable based on the unlocked states of its required nodes.
        /// </summary>
        /// <param name="node">The SkillTreeNodeUI that we want to check.</param>
        /// <returns>Whether the node has fulfilled its required nodes.</returns>
        private bool CheckNodeFulfilRequirements(SkillTreeNodeUI node)
        {
            bool fulfilRequirements = true;

            for (int i = 0; i < node.RequiredNodeList.Length; i++)
            {
                fulfilRequirements = node.RequiredNodeList[i].IsUnlocked;
                if(!fulfilRequirements) break;
            }

            return fulfilRequirements;
        }

        /// <summary>
        /// Checks and returns if a node has been unlocked from a save data or outside data container.
        /// </summary>
        /// <param name="nodeData">The SkillTreeNodeData containing the node's information we want to check</param>
        /// <returns>Whether the node has been unlocked previously.</returns>
        private bool IsNodeUnlocked(SkillTreeNodeData nodeData)
        {
            // Access Save System to check if the node is unlocked
            return CheckNodeUnlocked(nodeData);
        }
    }

}
