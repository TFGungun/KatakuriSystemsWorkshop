using UnityEngine;

namespace Katakuri.Modules.Event
{
    public abstract class EventActionBase{}

    /// <summary>
    /// Base class for defining an Event Action.
    /// Action in achievement will determine how the ProcessTarget will progress.
    /// Every ProcessTarget needs to implement their own EventActionBase method because
    /// every ProcessTarget has a different way of progressing.
    /// </summary>
    public abstract class EventActionBase<ProcessTarget> : ScriptableObject
    {
        public virtual void InitializeAction()
        {
        }
        
        public abstract void ExecuteAction(EventProcessor processor, ProcessTarget target);
    }
    
    /// <summary>
    /// Base AchievementAction class for actions that needs to process arguments from <see cref="ParamEvent"/>.
    /// The arguments will be passed to the <see cref="ProcessTarget"/> and this class can take it as needed.
    /// </summary>
    public abstract class EventActionParam<EventType, ProcessTarget> : EventActionBase<ProcessTarget>
        where EventType : ParamEvent
    {
        public EventType GetParam(EventProcessor processor)
        {
            return processor.GetEventParam<EventType>();
        }
    }

}
