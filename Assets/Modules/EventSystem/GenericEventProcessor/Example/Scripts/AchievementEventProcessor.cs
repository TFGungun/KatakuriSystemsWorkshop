using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.Modules.Event.Test
{
    public class AchievementEventProcessor : EventProcessor<AchievementData>
    {
        private AchievementManager _manager;
        public AchievementEventProcessor(AchievementData target) : base(target)
        {
            
            
        }

        public void InitializeAchievementProcessor(AchievementManager manager)
        {
            _manager = manager;
            InitializeProcessor();
        }

        protected override void ExecuteAction()
        {
            base.ExecuteAction();
            if(Target.IsCompleted)
            {
                _manager.CompleteAchievement(this);
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

