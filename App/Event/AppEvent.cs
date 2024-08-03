

using UnityEngine;

namespace App.Event
{
    public class AppEvent<T>
    {
        public EventType Type { get; private set; }
        public T Data { get; private set; }

        public AppEvent(EventType type, T data = default)
        {
            Type = type;
            Data = data;
        }
        
        
    }   
    
}

