using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.SkillTree1.Nodes
{
    [CreateAssetMenu(menuName = "Katakuri/SkillTree1/Nodes/AcquireSkillNode")]
    public class AcquireSkillNodeData : SkillTreeNodeData
    {
        public override void ExecuteNodeEffect(SkillHolder skillHolder)
        {
            skillHolder.AcquireSkill(Skill.SkillID);
        }
    }

}
