using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.PersonSimulation
{
    public abstract class Activity<U, V> : ScriptableObject 
        where U : Status
        where V : SimulationContext
    {
        public List<ActivityRequirement<U, V>> Requirements;

        public bool CheckValidity(U status, V context)
        {
            return Requirements.TrueForAll((requirement) => requirement.CheckValidity(status, context));
        }

        public abstract ActivityBehaviour<U> GetActivityBehaviour();
    }

    public abstract class ActivityBehaviour<T>
        where T : Status
    {
        public abstract void Update(float tick, T status);
    }
}

