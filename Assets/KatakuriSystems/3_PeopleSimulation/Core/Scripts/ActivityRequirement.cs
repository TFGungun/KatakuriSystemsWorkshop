using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.PersonSimulation
{
    /// <summary>
    /// Defines a requirement for an activity to be valid
    /// </summary>
    /// <typeparam name="TStatus"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public abstract class ActivityRequirement<TStatus, TContext> : ScriptableObject
        where TStatus : Status
        where TContext : SimulationContext
    {
        public abstract bool CheckValidity(TStatus status, TContext context);
    }
}

