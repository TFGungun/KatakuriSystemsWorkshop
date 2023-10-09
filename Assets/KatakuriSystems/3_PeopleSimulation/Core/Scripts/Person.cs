using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.PersonSimulation
{
    /// <summary>
    /// Represents a Scriptable Object containing the data of a Person
    /// </summary>
    /// <typeparam name="TStatus"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public abstract class Person<TStatus, TContext> : ScriptableObject
        where TStatus : Status
        where TContext : SimulationContext
    {
        public TStatus Status;
        public List<Activity<TStatus, TContext>> Activities;

        public PersonBehaviour<TStatus, TContext> GetPersonBehaviour()
        {
            return new PersonBehaviour<TStatus, TContext>(Status, Activities);
        }
    }

    /// <summary>
    /// Represents the Person as a simulation instance in the world
    /// </summary>
    /// <typeparam name="TStatus"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public class PersonBehaviour<TStatus, TContext>
        where TStatus : Status
        where TContext : SimulationContext
    {
        private TStatus _status;
        private List<Activity<TStatus, TContext>> _activities;
        public ActivityBehaviour<TStatus> CurrentActivity;

        public PersonBehaviour(TStatus status, List<Activity<TStatus, TContext>> activities)
        {
            _status = status;
            _activities = activities;
        }

        public void Update(float tick, TContext context)
        {
            if(CurrentActivity != null) CurrentActivity.Update(tick, _status);

            if(TryGetValidActivity(out Activity<TStatus, TContext> validActivity, context))
            {
                ActivityBehaviour<TStatus> newActivity = validActivity.GetActivityBehaviour();
                if(CurrentActivity != null && CurrentActivity.GetType() == newActivity.GetType()) return; // If the activities are the same type, do not transition
                CurrentActivity = newActivity;
            }
        }

        public bool TryGetValidActivity(out Activity<TStatus, TContext> validActivity, TContext context)
        {
            validActivity = _activities.Find((activity) => activity.CheckValidity(_status, context));
            return validActivity != null;
        }
    }
}

