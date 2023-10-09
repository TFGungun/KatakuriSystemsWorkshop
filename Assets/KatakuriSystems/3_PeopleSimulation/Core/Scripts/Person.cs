using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.PersonSimulation
{
    public abstract class Person<U, V> : ScriptableObject
        where U : Status
        where V : SimulationContext
    {
        public U Status;
        public List<Activity<U, V>> Activities;

        public PersonBehaviour<U, V> GetPersonBehaviour()
        {
            return new PersonBehaviour<U, V>(Status, Activities);
        }
    }

    public abstract class PersonBehaviour
    {

    }

    public class PersonBehaviour<U, V> : PersonBehaviour
        where U : Status
        where V : SimulationContext
    {
        public U Status;
        public List<Activity<U, V>> Activities;
        public ActivityBehaviour<U> CurrentActivity;

        public PersonBehaviour(U status, List<Activity<U, V>> activities)
        {
            Status = status;
            Activities = activities;
        }

        public void Update(float tick, V context)
        {
            if(CurrentActivity != null) CurrentActivity.Update(tick, Status);

            if(TryGetValidActivity(out Activity<U, V> validActivity, context))
            {
                ActivityBehaviour<U> newActivity = validActivity.GetActivityBehaviour();
                if(CurrentActivity != null && CurrentActivity.GetType() == newActivity.GetType()) return;
                CurrentActivity = newActivity;
            }
        }

        public bool TryGetValidActivity(out Activity<U, V> validActivity, V context)
        {
            validActivity = Activities.Find((activity) => activity.CheckValidity(Status, context));
            return validActivity != null;
        }
    }
}

