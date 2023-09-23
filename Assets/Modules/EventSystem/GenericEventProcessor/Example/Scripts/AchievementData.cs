using UnityEngine;

namespace Katakuri.Modules.Event.Test
{
    [CreateAssetMenu(menuName = "Katakuri/GenericEventProcessor/Test/AchievementData")]
    public class AchievementData : ScriptableObject
    {
        [Header("Details")]
        public string Name;
        public int Progress;
        public int Target;

        public bool IsCompleted;

        public EventTriggerBase Trigger;
        public EventFilterBase[] FilterList;
        public EventActionBase<AchievementData> Action;

        public void AddProgress(int value)
        {
            Progress += value;
            if(Progress >= Target)
            {
                Progress = Target;
                IsCompleted = true;
            }
        }
    }

}
