using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.Modules.Event.Test
{
    [CreateAssetMenu(menuName = "Katakuri/GenericEventProcessor/Test/ExampleEventWrapper")]
    public class ExampleEventWrapper : EventWrapper<ExampleEvent.NonParamEvent, ExampleEventResolver>
    {
    }

    public class ExampleEventResolver : EventResolveBehaviour<ExampleEvent.NonParamEvent>
    {
        public override void AttachListener()
        {
            AddListener(RegisteredDelegate);
        }

        public override void RemoveListener()
        {
            RemoveListener(RegisteredDelegate);
        }

        private void AddListener(EventManager.NonParamEventDelegate del)
        {
            switch(EventToListen)
            {
                case ExampleEvent.NonParamEvent.ClickButtonEvent:
                    EventManager.AddListener<ExampleEvent.ClickButtonEvent>(del);
                    break;
            }

        }

        private void RemoveListener(EventManager.NonParamEventDelegate del)
        {
            switch(EventToListen)
            {
                case ExampleEvent.NonParamEvent.ClickButtonEvent:
                    EventManager.RemoveListener<ExampleEvent.ClickButtonEvent>(del);
                    break;
            }

        }
    }
}

