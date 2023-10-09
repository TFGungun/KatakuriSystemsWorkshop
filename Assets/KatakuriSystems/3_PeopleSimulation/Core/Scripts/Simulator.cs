using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.PersonSimulation
{
    /// <summary>
    /// Represent a simulator where all the Person are running
    /// </summary>
    /// <typeparam name="TPerson"></typeparam>
    /// <typeparam name="TStatus"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public abstract class Simulator<TPerson, TStatus, TContext> : MonoBehaviour
        where TPerson : Person<TStatus, TContext>
        where TStatus : Status
        where TContext : SimulationContext
    {
        private List<PersonBehaviour<TStatus, TContext>> _personBehaviours = new List<PersonBehaviour<TStatus, TContext>>();
        public TContext Context;

        public void RegisterPeople(List<TPerson> people)
        {
            foreach(TPerson person in people)
            {
                RegisterPerson(person);
            }
        }

        protected virtual void RegisterPerson(TPerson person)
        {
            PersonBehaviour<TStatus, TContext> behaviour = person.GetPersonBehaviour();
            _personBehaviours.Add(behaviour);
        }

        protected virtual void UpdateBehaviours(float tick)
        {
            foreach(PersonBehaviour<TStatus, TContext> behaviour in _personBehaviours)
            {
                behaviour.Update(tick, Context);
            }
        }


    }
}

