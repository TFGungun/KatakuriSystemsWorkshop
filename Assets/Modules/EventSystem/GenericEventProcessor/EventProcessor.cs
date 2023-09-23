using System;
using System.Collections.Generic;

namespace Katakuri.Modules.Event
{
    /// <summary>
    /// Wrapper for Event Monitoring which includes reading for event, filtering it, and executing a certain action.
    /// This class only serve as base class for its generic class <see cref= "EventProcessor{T}"/>.
    /// </summary>
    public abstract class EventProcessor
    {
        protected Dictionary<Type, ParamEvent> ParamDictionary;

        public EventProcessor()
        {
            this.ParamDictionary = new Dictionary<Type, ParamEvent>();
        }

        public void AddEventParam<EventType>(EventType args) where EventType : ParamEvent
        {
            var type = typeof(EventType);
            if(ParamDictionary.ContainsKey(type))
            {
                ParamDictionary[type] = args;
            } else
            {
                ParamDictionary.Add(type, args);
            }
        }

        public EventType GetEventParam<EventType>() where EventType : ParamEvent
        {
            var type = typeof(EventType);
            ParamEvent tempParam = default;

            if(ParamDictionary.TryGetValue(type, out tempParam))
            {
                EventType param = tempParam as EventType;
                if(param != null)
                {
                    return param;
                }
            }

            return null;
        }

        public void RemoveEventParam<EventType>() where EventType : ParamEvent
        {
            var type = typeof(EventType);
            if(ParamDictionary.ContainsKey(type))
            {
                ParamDictionary.Remove(type);
            }
        }

        public abstract void TriggerProcessor();
        protected abstract bool CheckFilter();
        protected abstract void ExecuteAction();

    }

    /// <summary>
    /// Wrapper for processing event that is bound to a <paramref name = "ProcessTarget"/>.
    /// This class should be instantiated and initialized as a way to monitor each event.
    /// It should be removed and destroyed by calling <see cref = "DestroyProcessor"/>
    /// </summary>
    /// <typeparam name="ProcessTarget">Can be any class that can be acted upon by <see cref="EventActionBase"/>.</typeparam>
    public abstract class EventProcessor<ProcessTarget> : EventProcessor
    {
        public ProcessTarget Target;
        protected EventTriggerBehaviourBase Trigger;
        protected EventFilterBase[] FilterList;
        protected EventActionBase<ProcessTarget> Action;

        public EventProcessor(ProcessTarget target) : base()
        {
            this.Target = target;
        }

        /// <summary>
        /// Initializes the Trigger, Filters, and Action of this Event Processor
        /// </summary>
        protected void InitializeProcessor()
        {
            this.Trigger = GetTargetTriggerBehaviour();
            this.Trigger.InitializeTrigger(this);

            EventFilterBase[] targetFilter = GetTargetFilterList();
            this.FilterList = new EventFilterBase[targetFilter.Length];

            for (int i = 0; i < this.FilterList.Length; i++)
            {
                FilterList[i] = targetFilter[i];
                FilterList[i].InitializeFilter(this);
            }

            this.Action = GetTargetAction();
        }

        /// <summary>
        /// Retrieves the Trigger Behaviour from <paramref name = "ProcessTarget"/>
        /// </summary>
        /// <returns></returns>
        protected abstract EventTriggerBehaviourBase GetTargetTriggerBehaviour();

        /// <summary>
        /// Retrieves the Filter List from <paramref name = "ProcessTarget"/>
        /// </summary>
        /// <returns></returns>
        protected abstract EventFilterBase[] GetTargetFilterList();

        /// <summary>
        /// Retrieves the Action from <paramref name = "ProcessTarget"/>
        /// </summary>
        /// <returns></returns>
        protected abstract EventActionBase<ProcessTarget> GetTargetAction();

        public void DestroyProcessor()
        {
            this.Trigger.DestroyTrigger();
            for (int i = 0; i < this.FilterList.Length; i++)
            {
                FilterList[i].DestroyFilter();
            }
        }
        
        public override void TriggerProcessor()
        {
            if(CheckFilter())
            {
                ExecuteAction();
            }
        }

        protected override bool CheckFilter()
        {
            bool isAllTrue = true;
            foreach(EventFilterBase filter in FilterList)
            {
                isAllTrue = filter.IsTrue();
                if(!isAllTrue)
                {
                    break;
                }
            }

            return isAllTrue;
        }

        protected override void ExecuteAction()
        {
            Action.ExecuteAction(this, Target);
        }
    }

}
