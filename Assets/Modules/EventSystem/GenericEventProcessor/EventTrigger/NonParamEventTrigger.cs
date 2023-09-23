using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.Modules.Event
{
    /// <summary>
    /// Concrete Implementation of <see cref="EventTriggerBase"/> that listens to <see cref="NonParamEvent"/>.
    /// If the event that we need falls into this type of event, it is advised to listen to it by using this trigger
    /// alongside <see cref="EventObject"/> that implements the corresponding event.
    /// </summary>
    [CreateAssetMenu(menuName = "Katakuri/GenericEventProcessor/Non Param Trigger")]
    public class NonParamEventTrigger : EventTriggerBase
    {
        public EventWrapper EventToListen;

        public override EventTriggerBehaviourBase GetTriggerBehaviour()
        {
            return new NonParamEventTriggerBehaviour(EventToListen);
        }
    }

    /// <summary>
    /// Concrete Implementation of <see cref="EventTriggerBehaviourBase"/> to be supplied to <see cref="NonParamEventTrigger"/>.
    /// This class will work with the listened <see cref="EventObject"/>.
    /// Please ensure that the listened event is unsubscribe when the trigger is destroyed so there is no dangling listener.
    /// </summary>
    public class NonParamEventTriggerBehaviour : EventTriggerBehaviourBase
    {
        public EventResolveBehaviour EventResolveBehaviour;

        public NonParamEventTriggerBehaviour(EventWrapper eventToListen)
        {
            EventResolveBehaviour = eventToListen.GetEventObjectBehaviour(OnEventTriggered);
        }


        public override void InitializeTrigger(EventProcessor processor)
        {
            base.InitializeTrigger(processor);
            EventResolveBehaviour.AttachListener();
        }

        public override void DestroyTrigger()
        {
            EventResolveBehaviour.RemoveListener();
        }

        private void OnEventTriggered()
        {
            Processor.TriggerProcessor();
        }
    }
}

