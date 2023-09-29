using UnityEngine;

namespace Katakuri.Modules.Event.Test
{
    [CreateAssetMenu(menuName = "Katakuri/GenericEventProcessor/Test/AchievementData")]
    public class AchievementData : ScriptableObject
    {
        [Header("Details")]
        public int ID;
        public string Name;
        [TextArea(3, 5)]
        public string Description;
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
