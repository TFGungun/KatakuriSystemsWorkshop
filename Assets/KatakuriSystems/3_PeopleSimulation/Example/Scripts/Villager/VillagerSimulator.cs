using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.SystemsWorkshop.PersonSimulation.Example1
{
    public class VillagerSimulator : Simulator<Villager, VillagerStatus, VillagerSimulationContext>
    {
        public List<Villager> Villagers;
        private void Start()
        {
            RegisterPeople(Villagers);
        }

        private void Update() 
        {
            UpdateBehaviours(Time.deltaTime);
        }
    }
}

