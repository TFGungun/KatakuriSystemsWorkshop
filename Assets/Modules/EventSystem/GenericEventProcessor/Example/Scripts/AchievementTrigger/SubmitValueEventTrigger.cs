using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.Modules.Event.Test
{

    [CreateAssetMenu(menuName = "Katakuri/GenericEventProcessor/Test/SubmitValueEventTrigger")]
    public class SubmitValueEventTrigger : ParamEventTrigger<ExampleEvent.SubmitValueEvent, SubmitValueEventTriggerBehaviour>
    {
    }

    public class SubmitValueEventTriggerBehaviour : ParamEventTriggerBehaviour<ExampleEvent.SubmitValueEvent>
    {
    }
}

