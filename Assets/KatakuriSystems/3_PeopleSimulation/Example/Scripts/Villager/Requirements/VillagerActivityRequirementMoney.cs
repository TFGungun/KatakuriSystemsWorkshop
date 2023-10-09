using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.PersonSimulation.Example1
{
    [CreateAssetMenu(menuName = "Katakuri/PersonSimulation/Example1/Requirement/Money")]
    public class VillagerActivityRequirementMoney : VillagerActivityRequirement
    {
        public int MinimumMoney = 0;
        public override bool CheckValidity(VillagerStatus status, VillagerSimulationContext context)
        {
            return status.Money > MinimumMoney;
        }
    }
}

