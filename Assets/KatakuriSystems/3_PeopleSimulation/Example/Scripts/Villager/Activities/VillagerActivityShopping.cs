using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.PersonSimulation.Example1
{
    [CreateAssetMenu(menuName = "Katakuri/PersonSimulation/Example1/Activity/Shopping")]
    public class VillagerActivityShopping : VillagerActivity
    {
        public override ActivityBehaviour<VillagerStatus> GetActivityBehaviour()
        {
            return new VillagerActivityShoppingBehaviour();
        }
    }

    public class VillagerActivityShoppingBehaviour : ActivityBehaviour<VillagerStatus>
    {
        public VillagerActivityShoppingBehaviour()
        {
        }
        public override void Update(float tick, VillagerStatus status)
        {
            status.Money -= 5;
        }
    }
}

