using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.SkillTree1
{
    /// <summary>
    /// Stores the basic information of a Skill Tree Node
    /// </summary>
    public abstract class SkillTreeNodeData : ScriptableObject
    {
        [Header("Information")]
        public string NodeKey; // For saving and identification purpose
        public string NodeName;
        [TextArea(3, 5)]
        public string NodeDescription;
        public int RequiredSP;
        
        [Header("Skill")]
        public SkillData Skill;

        /// <summary>
        /// Called when the player spends currency on this type of node.
        /// Override this method to create different types of nodes.
        /// </summary>
        /// <param name="skillHolder"></param>
        public abstract void ExecuteNodeEffect(SkillHolder skillHolder);
    }
}
