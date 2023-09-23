using System;
using System.Collections.Generic;
using UnityEngine;

namespace Katakuri.Modules.Event
{
    public class NonParamEvent{}
    public class ParamEvent{}
    public class EventManager : MonoBehaviour
    {
        public delegate void NonParamEventDelegate();
        public delegate void ParamEventDelegate<T>(T param);
        private static Dictionary<Type, Delegate> DelegateList = new Dictionary<Type, Delegate>();

        /// <summary>
        /// Assign/add delegate function into the event dictionary.
        /// </summary>
        /// <typeparam name="T">Event Type to listen to</typeparam>
        /// <param name="eventDelegate">Method to call for this event</param>
        public static void AddListener<T>(ParamEventDelegate<T> eventDelegate) where T : ParamEvent
        {
            Delegate tempDelegate;
            if(DelegateList.TryGetValue(typeof(T), out tempDelegate))
            {
                ParamEventDelegate<T> castedDelegate = tempDelegate as ParamEventDelegate<T>;
                DelegateList[typeof(T)] = castedDelegate += eventDelegate;
            } else
            {
                DelegateList.Add(typeof(T), eventDelegate);
            }
        }

        public static void AddListener<T>(NonParamEventDelegate eventDelegate) where T : NonParamEvent
        {
            Delegate tempDelegate;
            if(DelegateList.TryGetValue(typeof(T), out tempDelegate))
            {
                NonParamEventDelegate castedDelegate = tempDelegate as NonParamEventDelegate;
                DelegateList[typeof(T)] = castedDelegate += eventDelegate;
            } else
            {
                DelegateList.Add(typeof(T), eventDelegate);
            }
        }


        /// <summary>
        /// Remove a delegate function from the event dictionary.
        /// </summary>
        /// <typeparam name="T">Event Type to remove from</typeparam>
        /// <param name="eventDelegate">Method to remove from this event</param>
        public static void RemoveListener<T>(ParamEventDelegate<T> eventDelegate) where T : ParamEvent
        {
            Delegate tempDelegate;
            if(DelegateList.TryGetValue(typeof(T), out tempDelegate))
            {
                ParamEventDelegate<T> castedDelegate = tempDelegate as ParamEventDelegate<T>;

                castedDelegate -= eventDelegate;

                if(castedDelegate != null)
                {
                    DelegateList[typeof(T)] = castedDelegate;
                } else
                {
                    DelegateList.Remove(typeof(T));
                }
            }
        }

        public static void RemoveListener<T>(NonParamEventDelegate eventDelegate) where T : NonParamEvent
        {
            Delegate tempDelegate;
            if(DelegateList.TryGetValue(typeof(T), out tempDelegate))
            {
                NonParamEventDelegate castedDelegate = tempDelegate as NonParamEventDelegate;

                castedDelegate -= eventDelegate;

                if(castedDelegate != null)
                {
                    DelegateList[typeof(T)] = castedDelegate;
                } else
                {
                    DelegateList.Remove(typeof(T));
                }
            }
        }
        
        
        /// <summary>
        /// Trigger all delegate function associated to an event with given parameters
        /// </summary>
        /// <typeparam name="T">Event Type to trigger</typeparam>
        /// <param name="param">Parameters to call this event with</param>
        public static void TriggerEvent<T>(T param) where T : ParamEvent
        {
            Delegate tempDelegate;
            if(DelegateList.TryGetValue(typeof(T), out tempDelegate))
            {
                ParamEventDelegate<T> castedDelegate = tempDelegate as ParamEventDelegate<T>;
                castedDelegate.Invoke(param);
            }
        }

        public static void TriggerEvent<T>() where T : NonParamEvent
        {
            Delegate tempDelegate;
            if(DelegateList.TryGetValue(typeof(T), out tempDelegate))
            {
                NonParamEventDelegate castedDelegate = tempDelegate as NonParamEventDelegate;
                castedDelegate.Invoke();
            }
        }
    }
}

