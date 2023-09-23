using UnityEngine;

namespace Katakuri.Modules.Event
{
    /// <summary>
    /// Base class for defining an Event Filter.
    /// Filters in an EventProcesor will define what kinds of condition suffice the Event to be further processed.
    /// </summary>
    public abstract class EventFilterBase : ScriptableObject
    {
        public EventProcessor Processor;

        public virtual void InitializeFilter(EventProcessor processor)
        {
            this.Processor = processor;
        }
        public virtual void DestroyFilter(){}
        public abstract bool IsTrue();
        
    }

    /// <summary>
    /// Base EventFilter class for filters that needs to decide based on the arguments for a <see cref="ParamEvent"/>.
    /// The arguments will be passed to the <see cref="EventProcessor"/> and this class can take it as needed.
    /// </summary>
    public abstract class EventFilterParam<EventType> : EventFilterBase
        where EventType : ParamEvent
    {
        public EventType GetParam()
        {
            return Processor.GetEventParam<EventType>();
        }
    }

}
