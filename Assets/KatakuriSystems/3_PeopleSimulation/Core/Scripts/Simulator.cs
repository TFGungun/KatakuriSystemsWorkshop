using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.PersonSimulation
{
    public abstract class Simulator<U, V, W> : MonoBehaviour
        where U : Person<V, W>
        where V : Status
        where W : SimulationContext
    {
        public List<PersonBehaviour<V, W>> PersonBehaviours = new List<PersonBehaviour<V, W>>();
        public W Context;

        public void RegisterPeople(List<U> people)
        {
            foreach(U person in people)
            {
                RegisterPerson(person);
            }
        }

        protected virtual void RegisterPerson(U person)
        {
            PersonBehaviour<V, W> behaviour = person.GetPersonBehaviour();
            PersonBehaviours.Add(behaviour);
        }

        protected virtual void UpdateBehaviours(float tick)
        {
            foreach(PersonBehaviour<V, W> behaviour in PersonBehaviours)
            {
                behaviour.Update(tick, Context);
            }
        }


    }
}

