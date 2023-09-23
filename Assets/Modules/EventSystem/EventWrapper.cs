using UnityEngine;

namespace Katakuri.Modules.Event
{
    /// <summary>
    /// Base abstract class to wrap <see cref= "Katakuri.Modules.Event.NonParamEvent"/> listening.
    /// This class itself is not usable and is only made to be extended by the generic class <see cref= "EventObject{T,K}"/>.
    /// Please see the generic class for more information.
    /// </summary>
    public abstract class EventWrapper : ScriptableObject
    {
        public abstract EventResolveBehaviour GetEventObjectBehaviour(EventManager.NonParamEventDelegate del);
    }

    /// <summary>
    /// Scriptable Object Wrapper for <see cref= "Katakuri.Modules.Event.NonParamEvent"/>.
    /// This object will instantiate a behaviour to be attached to a GameObject to listen to the events.
    /// The base class expects <typeparamref name="EventType"/> to be an enum in the <see cref = "Katakuri.Modules.Event"/> namespace.
    /// Every implementation of this class has to implement a <typeparamref name="ResolveBehaviour"/> of the same <typeparamref name="EventType"/>.
    /// </summary>
    /// <typeparam name="T">Enum in the <see cref = "Katakuri.Modules.Event"/> namespace.</typeparam>
    public abstract class EventWrapper<EventType, ResolveBehaviour> : EventWrapper where EventType : struct
        where ResolveBehaviour : EventResolveBehaviour<EventType>, new()
    {
        public EventType EventToListen;

        public override EventResolveBehaviour GetEventObjectBehaviour(EventManager.NonParamEventDelegate del)
        {
            return new ResolveBehaviour{EventToListen = EventToListen, RegisteredDelegate = del};
        }
    }

    public abstract class EventResolveBehaviour
    {
        public EventManager.NonParamEventDelegate RegisteredDelegate;
        public abstract void AttachListener();
        public abstract void RemoveListener();
    }

    /// <summary>
    /// Behaviour resolver for  for <see cref= "Nix.Event.NonParamEvent"/>.
    /// This object will instantiate a behaviour to be attached to a GameObject to listen to the events.
    /// The base class expects <typeparamref name="T"/> to be an enum in the <see cref = "Nix.Event"/> namespace.
    /// </summary>
    /// <typeparam name="T">Enum in the <see cref = "Nix.Event"/> namespace.</typeparam>
    public abstract class EventResolveBehaviour<EventType> : EventResolveBehaviour where EventType : struct
    {
        public EventType EventToListen;

    }
}
