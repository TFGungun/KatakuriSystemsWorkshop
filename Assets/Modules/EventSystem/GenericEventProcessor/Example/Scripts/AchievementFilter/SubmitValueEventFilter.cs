using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.Modules.Event.Test
{
    [CreateAssetMenu(menuName = "Katakuri/GenericEventProcessor/Test/SubmitValueEventFilter")]
    public class SubmitValueEventFilter : EventFilterParam<ExampleEvent.SubmitValueEvent>
    {
        public int ValueToCheck;
        public override void InitializeFilter(EventProcessor processor)
        {
            Processor = processor;
        }

        public override void DestroyFilter()
        {
        }

        public override bool IsTrue()
        {
            ExampleEvent.SubmitValueEvent args = GetParam();

            if(args != null)
            {
                return args.Value == ValueToCheck;
            }   

            return false;
        }
    }
}

