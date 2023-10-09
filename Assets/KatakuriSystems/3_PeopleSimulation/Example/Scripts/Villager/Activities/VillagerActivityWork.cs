using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.PersonSimulation.Example1
{
    [CreateAssetMenu(menuName = "Katakuri/PersonSimulation/Example1/Activity/Work")]
    public class VillagerActivityWork : VillagerActivity
    {
        public override ActivityBehaviour<VillagerStatus> GetActivityBehaviour()
        {
            return new VillagerActivityWorkBehaviour();
        }
    }

    public class VillagerActivityWorkBehaviour : ActivityBehaviour<VillagerStatus>
    {
        public float currentTime;
        public VillagerActivityWorkBehaviour()
        {
            currentTime = 0f;
        }
        public override void Update(float tick, VillagerStatus status)
        {
            currentTime += tick;
            if(currentTime > 1f)
            {
                currentTime -= 1f;
                status.Money += 1;
            }
        }
    }
}

