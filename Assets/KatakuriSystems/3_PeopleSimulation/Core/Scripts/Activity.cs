using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.PersonSimulation
{
    /// <summary>
    /// Represents an activity which can be executed by a Person
    /// </summary>
    /// <typeparam name="TStatus"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public abstract class Activity<TStatus, TContext> : ScriptableObject 
        where TStatus : Status
        where TContext : SimulationContext
    {
        public List<ActivityRequirement<TStatus, TContext>> Requirements;

        public bool CheckValidity(TStatus status, TContext context)
        {
            return Requirements.TrueForAll((requirement) => requirement.CheckValidity(status, context));
        }

        public abstract ActivityBehaviour<TStatus> GetActivityBehaviour();
    }

    /// <summary>
    /// Represents the Activity as a simulation instance in the world
    /// </summary>
    /// <typeparam name="TStatus"></typeparam>
    public abstract class ActivityBehaviour<TStatus>
        where TStatus : Status
    {
        public abstract void Update(float tick, TStatus status);
    }
}

