using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.Modules.Event.Test
{
    public class AchievementManager : MonoBehaviour
    {
        private List<AchievementEventProcessor> _monitoredEventProcessorList;

        private void Awake() 
        {
            _monitoredEventProcessorList = new List<AchievementEventProcessor>();
        }

        public void RegisterAchievement(AchievementData data)
        {
            var processor = new AchievementEventProcessor(data);
            _monitoredEventProcessorList.Add(processor);
            processor.InitializeAchievementProcessor(this);
        }

        public void CompleteAchievement(AchievementEventProcessor processor)
        {
            processor.DestroyProcessor();
            _monitoredEventProcessorList.Remove(processor);
        }

        [Header("Debug")]
        public AchievementData DebugData;
        
        [ContextMenu("Register Debug Data")]
        public void RegisterDebugData()
        {
            if(DebugData == null) return;
            RegisterAchievement(DebugData);
        }
    }
}

