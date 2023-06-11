using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.SkillTree1.Nodes
{
    [CreateAssetMenu(menuName = "Katakuri/SkillTree1/Nodes/UpgradeSkillNode")]
    public class UpgradeSkillNodeData : SkillTreeNodeData
    {
        [Header("Upgrade Skill")]
        [SerializeField] private int upgradeToLevel;
        public override void ExecuteNodeEffect(SkillHolder skillHolder)
        {
            skillHolder.UpgradeSkill(Skill.SkillID, upgradeToLevel);
        }
    }

}
