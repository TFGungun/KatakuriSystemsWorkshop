using UnityEngine;

namespace Katakuri.Modules.Event
{
    /// <summary>
    /// Base class for defining an Event Trigger.
    /// Trigger is the primary way for an Event Processor to get notified for the things that are relevant.
    /// Every Event Trigger should be able to be used by different Event Processors.
    /// Therefore we delegate the trigger handling to <see cref="EventTriggerBehaviourBase"/>.
    /// </summary>
    public abstract class EventTriggerBase : ScriptableObject
    {
        public abstract EventTriggerBehaviourBase GetTriggerBehaviour();
    }
    
    /// <summary>
    /// Base class to create the trigger handling behaviour of an Event Trigger.
    /// Trigger will mainly be handled by listening to event and notifying it to <see cref="EventProcessor"/>.
    /// </summary>
    public abstract class EventTriggerBehaviourBase
    {
        public EventProcessor Processor;

        public virtual void InitializeTrigger(EventProcessor processor)
        {
            this.Processor = processor;
        }

        public abstract void DestroyTrigger();
    }
}
