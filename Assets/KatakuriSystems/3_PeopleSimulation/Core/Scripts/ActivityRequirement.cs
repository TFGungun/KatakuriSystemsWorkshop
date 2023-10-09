using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.PersonSimulation
{
    public abstract class ActivityRequirement<U, V> : ScriptableObject
        where U : Status
        where V : SimulationContext
    {
        public abstract bool CheckValidity(U status, V context);
    }
}

