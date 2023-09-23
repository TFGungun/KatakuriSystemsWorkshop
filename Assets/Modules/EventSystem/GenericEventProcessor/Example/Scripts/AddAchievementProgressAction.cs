using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.Modules.Event.Test
{   
    [CreateAssetMenu(menuName = "Katakuri/GenericEventProcessor/Test/AddAchievementProgressAction")]
    public class AddAchievementProgressAction : EventActionBase<AchievementData>
    {
        public int ProgressValue = 1;

        public override void ExecuteAction(EventProcessor processor, AchievementData target)
        {
            target.AddProgress(ProgressValue);
        }
    }
}

