using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.Modules.Event.Test
{
    public class ExampleScript : MonoBehaviour
    {
        [SerializeField] private AchievementManager _achievementManager;
        [SerializeField] private List<AchievementData> _achievementToMonitor;
        public bool RegisterAchievementOnStart = true;

        private void Start() 
        {
            if(RegisterAchievementOnStart) RegisterAllAchievement();
        }

        [ContextMenu("Register All Achievement")]
        public void RegisterAllAchievement()
        {
            foreach(AchievementData achievementData in _achievementToMonitor)
            {
                _achievementManager.RegisterAchievement(achievementData);
            }
        }
    }
}

