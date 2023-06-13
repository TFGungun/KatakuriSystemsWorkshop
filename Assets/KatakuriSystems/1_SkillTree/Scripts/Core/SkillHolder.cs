using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.SkillTree1
{ 
    /// <summary>
    /// Holds the player's Skills. This object can be freely represented in any way.
    /// </summary>
    public class SkillHolder : MonoBehaviour
    {
        public HashSet<int> AcquiredSkillList;
        public Dictionary<int, int> SkillLevelDictionary;


        private void Awake() 
        {
            AcquiredSkillList = new HashSet<int>();
            SkillLevelDictionary = new Dictionary<int, int>();
        }

        public void AcquireSkill(int id)
        {
            AcquiredSkillList.Add(id);
            if(!SkillLevelDictionary.ContainsKey(id))
            {
                SkillLevelDictionary.Add(id, 1);
            }
        }

        public void UpgradeSkill(int id, int level)
        {
            if(SkillLevelDictionary.ContainsKey(id))
            {
                SkillLevelDictionary[id] = level;
            }
        }
        
    }
    
}


