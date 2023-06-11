using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.SkillTree1
{
    public class SkillDatabase : MonoBehaviour
    {
        [SerializeField] private List<SkillData> _availableSkillData;
        public List<SkillData> AvailableSkillData => _availableSkillData;

        public SkillData GetSkillData(int ID)
        {
            return _availableSkillData.Find((skillData) => skillData.SkillID == ID);
        }

        public string GetSkillDescription(int ID)
        {
            SkillData skillData = GetSkillData(ID);
            if(skillData != null)
            {
                return skillData.SkillDescription;
            } else
            {
                return string.Empty;
            }
        }
    }

}
