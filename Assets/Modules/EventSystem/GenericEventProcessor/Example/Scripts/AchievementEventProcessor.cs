using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.Modules.Event.Test
{
    public class AchievementEventProcessor : EventProcessor<AchievementData>
    {
        private AchievementMonitor _monitor;
        public AchievementEventProcessor(AchievementData target) : base(target)
        {
            
            
        }

        public void InitializeAchievementProcessor(AchievementMonitor monitor)
        {
            _monitor = monitor;
            InitializeProcessor();
        }

        protected override void ExecuteAction()
        {
            base.ExecuteAction();
            _monitor.UpdateAchievement(this);
            if(Target.IsCompleted)
            {
                _monitor.CompleteAchievement(this);
            }
        }

        protected override EventTriggerBehaviourBase GetTargetTriggerBehaviour()
        {
            return Target.Trigger.GetTriggerBehaviour();
        }

        protected override EventFilterBase[] GetTargetFilterList()
        {
            return Target.FilterList;
        }

        protected override EventActionBase<AchievementData> GetTargetAction()
        {
            return Target.Action;
        }
    }
}

