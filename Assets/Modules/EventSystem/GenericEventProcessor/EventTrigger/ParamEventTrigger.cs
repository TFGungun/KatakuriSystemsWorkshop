using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.Modules.Event
{
    /// <summary>
    /// Base EventTrigger class for triggers that listen to Event with Parameters (<see cref="ParamEvent"/>).
    /// Different implementation is needed as the arguments from these events need to be passed to <see cref="EventProcessor"/>.
    /// If the event does not need any extra-handling, this class can be extended by just subclassing this class by specifying the <paramref name="EventType"/>,
    /// with no further code.
    /// This class also needs a concrete implementation of <see cref="EventTriggerParamBehaviour"/> of the same <see cref="ParamEvent"/>.
    /// </summary>
    /// <typeparam name="EventType"></typeparam>
    /// <typeparam name="Behaviour">Concrete Behaviour Implementation of <see cref="ParamEvent"/></typeparam>
    public abstract class ParamEventTrigger<EventType, Behaviour> : EventTriggerBase
        where EventType : ParamEvent
        where Behaviour : ParamEventTriggerBehaviour<EventType>, new()
    {
        public override EventTriggerBehaviourBase GetTriggerBehaviour()
        {
            return new Behaviour{};
        }
    }

    /// <summary>
    /// Base EventTriggerBehaviour class to be supplied for <see cref="EventTriggerParam"/>.
    /// Every <see cref="ParamEvent"/> that needs to be implemented, will also have to extend this class as a concrete implementation.
    /// This class will listen to the <paramref name="EventType"/> and passing the corresponding arguments to the <see cref="EventProcessor"/>.
    /// Please ensure that the listened event is unsubscribed when the trigger is destroyed so there is no dangling listener.
    /// </summary>
    /// <typeparam name="EventType"></typeparam>
    public abstract class ParamEventTriggerBehaviour<EventType> : EventTriggerBehaviourBase
        where EventType : ParamEvent
    {
        public EventType TriggerParam;

        public override void InitializeTrigger(EventProcessor processor)
        {
            base.InitializeTrigger(processor);
            EventManager.AddListener<EventType>(OnEventTriggered);
        }

        public override void DestroyTrigger()
        {
            EventManager.RemoveListener<EventType>(OnEventTriggered);
        }

        protected virtual void OnEventTriggered(EventType args)
        {
            TriggerParam = args;
            Processor.AddEventParam<EventType>(args);
            Processor.TriggerProcessor();
        } 
    }
}

