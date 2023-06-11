using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.SkillTree1
{
    /// <summary>
    /// Stores the saved unlocked state for Skill Trees in the game.
    /// This object can be represented in any way.
    /// </summary>
    public class SkillTreeSaveData : MonoBehaviour
    {
        [System.Serializable]
        public class NodeSaveData
        {
            public string NodeKey;
            public bool IsUnlocked;
        }

        [SerializeField] private List<NodeSaveData> savedNodeList;

        public bool IsNodeUnlocked(string nodeKey)
        {
            return savedNodeList.Exists((saveData) => saveData.NodeKey.Equals(nodeKey) && saveData.IsUnlocked);
        }

        public void SetUnlockNode(string nodeKey, bool isUnlocked)
        {
            NodeSaveData saveData = savedNodeList.Find((data) => data.NodeKey.Equals(nodeKey));
            if(saveData != null)
            {
                saveData.IsUnlocked = isUnlocked;
            } else
            {
                saveData = new NodeSaveData();
                saveData.NodeKey = nodeKey;
                saveData.IsUnlocked = isUnlocked;
                savedNodeList.Add(saveData);
            }
        }

        #region Debug
        [Header("Debug")]
        [SerializeField] private SkillTreeNodeData debugNodeData;
        [SerializeField] private bool debugIsUnlocked;

        [ContextMenu("Insert Debug Node to List")]
        private void InsertDebugNodeToList()
        {
            if(debugNodeData == null) return;

            NodeSaveData saveData = savedNodeList.Find((saveData) => saveData.NodeKey.Equals(debugNodeData.NodeKey));
            if(saveData != null)
            {
                saveData.IsUnlocked = debugIsUnlocked;
            } else
            {
                saveData = new NodeSaveData();
                saveData.NodeKey = debugNodeData.NodeKey;
                saveData.IsUnlocked = debugIsUnlocked;
                savedNodeList.Add(saveData);
            }
        }

        #endregion
    }

}
