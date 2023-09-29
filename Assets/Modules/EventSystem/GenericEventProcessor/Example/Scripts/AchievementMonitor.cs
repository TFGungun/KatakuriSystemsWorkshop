using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Katakuri.Modules.Event.Test
{
    public class AchievementMonitor : MonoBehaviour
    {
        [SerializeField] private AchievementData _achievementToMonitor;
        private AchievementEventProcessor _monitoredProcessor;

        [SerializeField] private TMP_Text _achievementNumber;
        [SerializeField] private TMP_Text _achievementName;
        [SerializeField] private TMP_Text _achievementDescription;
        [SerializeField] private TMP_Text _achievementProgress;

        private void Start() 
        {
            RegisterAchievement();
        }

        public void RegisterAchievement()
        {
            if(_achievementToMonitor.IsCompleted) return;
            _monitoredProcessor = new AchievementEventProcessor(_achievementToMonitor);
            _monitoredProcessor.InitializeAchievementProcessor(this);

            _achievementNumber.text = $"Achievement #{_achievementToMonitor.ID}";
            _achievementName.text = _achievementToMonitor.Name;
            _achievementDescription.text = _achievementToMonitor.Description;
            _achievementProgress.text = $"{_achievementToMonitor.Progress}/{_achievementToMonitor.Target}";
        }

        public void UpdateAchievement(AchievementEventProcessor processor)
        {
            _achievementProgress.text = $"{_achievementToMonitor.Progress}/{_achievementToMonitor.Target}";
        }

        public void CompleteAchievement(AchievementEventProcessor processor)
        {
            processor.DestroyProcessor();
            _achievementProgress.text = "Complete";
        }

        
    }
}

