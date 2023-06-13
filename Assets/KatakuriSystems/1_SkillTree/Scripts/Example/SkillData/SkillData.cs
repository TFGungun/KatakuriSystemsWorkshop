using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.SkillTree1
{
    [CreateAssetMenu(menuName = "Katakuri/SkillTree1/Skill")]
    public class SkillData : ScriptableObject
    {
        public int SkillID;
        public string SkillName;
        [TextArea(3, 5)]
        public string SkillDescription;
    }


}