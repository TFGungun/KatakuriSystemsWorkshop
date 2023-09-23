using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.Modules.Event.Test
{
    public class ExampleEvent
    {
        public enum NonParamEvent { 
            ClickButtonEvent = 0
            };
        public class ClickButtonEvent : Event.NonParamEvent{}
        public class SubmitValueEvent : Event.ParamEvent
        {
            public int Value;
            public SubmitValueEvent(int value)
            {
                Value = value;
            }
        }
    }
}

